using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eaa.LtaBlog.Application.Models;
using Eaa.LtaBlog.Application.Core.Queries;
using System.Web.Security;
using Eaa.LtaBlog.Application.Core.Entities.Security;
using Eaa.LtaBlog.Application.Core.Entities;
using Eaa.LtaBlog.Application.Core.Commands.Account;
using AutoMapper;

namespace Eaa.LtaBlog.Application.Controllers
{
    public class AccountController : LtaController
    {
        public ActionResult Login(string returnUrl)
        {
			return View(new LoginModel { ReturnUrl = returnUrl });
        }

		[HttpPost]
		public ActionResult Login(LoginModel input)
		{
			var user = RavenSession.GetUserByEmail(input.Email);

			if (ModelState.IsValid && (user == null || !user.ValidatePassword(input.Password)))
			{
				ModelState.AddModelError("UserNotExistOrPasswordNotMatch",
				"Email and / or password is incorrect.");
			}

			if (ModelState.IsValid)
			{
				FormsAuthentication.SetAuthCookie(input.Email, true);

				if (!string.IsNullOrWhiteSpace(input.ReturnUrl))
					return Redirect(input.ReturnUrl);

				return RedirectToRoute(new { controller = "Posts", action = "Index" });

			}

			return View(new LoginModel { Email = input.Email, ReturnUrl = input.ReturnUrl });
		}

		[Authorize]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return Redirect(Request.UrlReferrer.ToString());
		}

		public ActionResult Register()
		{
			return View(new ProfileModel());
		}

		[HttpPost]
		public ActionResult Register(ProfileModel input)
		{
			if (ModelState.IsValid)
			{
				var registerUserCommand = Mapper.Map<RegisterUserCommand>(input);
				CommandProcessor.Process(registerUserCommand);

				if (registerUserCommand.IsValid)
				{
					return Login(Mapper.Map<LoginModel>(input));
				}
			}

			input.Password = string.Empty;

			return View(input);
		}

		[Authorize]
		public ActionResult EditProfile()
		{
			var profileModel = Mapper.Map<ProfileModel>(LoggedUser);

			return View(profileModel);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(ProfileModel input)
		{
			if (LoggedUser.Id != input.Id)
				throw new Exception("Your are not allowed to modify somebodies' else profile");

			if (ModelState.IsValid)
			{
				var editProfileCommand = Mapper.Map<EditProfileCommand>(input);

				CommandProcessor.Process(editProfileCommand);

				if (editProfileCommand.IsValid)
				{
					input.Password = input.ValidatePassword = string.Empty;

					Notify("Your profile has been succesfully updated", NotifyType.Success, NotifyPosition.Top);

					return View(input);
				}
			}

			input.Password = input.ValidatePassword = string.Empty;

			return View(input);

		}


		[ChildActionOnly]
		public ActionResult CurrentUser()
		{
			if (LoggedUser == null)
			{
				if (IsLoginRoute())
					return new EmptyResult { };

				return PartialView("UserNotLogged", new LoginModel { ReturnUrl = Request.Url.ToString() });
			}
			else
			{
				return PartialView("UserLogged", LoggedUser);
			}
		}

		private bool IsLoginRoute()
		{
			string controllerName = Request.RequestContext.RouteData.Values["controller"].ToString();
			string actionName = Request.RequestContext.RouteData.Values["action"].ToString();

			return string.Equals("Account", controllerName, StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals("Login", actionName, StringComparison.InvariantCultureIgnoreCase);
		}

    }
}
