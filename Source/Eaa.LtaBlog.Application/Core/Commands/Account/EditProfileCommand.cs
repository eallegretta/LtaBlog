using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using Eaa.LtaBlog.Application.Core.Queries;
using Eaa.LtaBlog.Application.Core.Entities;

namespace Eaa.LtaBlog.Application.Core.Commands.Account
{
	public class EditProfileCommand: Command
	{
		[Required]
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[Required]
		[Email]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Validate Password")]
		[Compare("Password")]
		public string ValidatePassword { get; set; }

		[Required]
		public string Firstname { get; set; }

		[Required]
		public string Lastname { get; set; }


		protected override bool Validate()
		{
			bool isValid = base.Validate();
			
			if (isValid)
			{
				var existingUser = Session.GetUserByEmail(Email);

				if (existingUser == null)
				{
					throw new Exception("The user you are trying to update does not exist");
				}

				if (existingUser != null && existingUser.Id != Id)
				{
					AddValidationResult("The email you are trying to set already exists", "Email");
					return false;
				}
			}

			return isValid;
		}

		public override void Execute()
		{
			if (!IsValid)
				return;

			var user = Session.Load<User>(Id);

			user.Email = Email;
			user.Firstname = Firstname;
			user.Lastname = Lastname;

			if (!string.IsNullOrWhiteSpace(Password))
				user.SetPassword(Password);

			Session.Store(user);
		}
	}
}