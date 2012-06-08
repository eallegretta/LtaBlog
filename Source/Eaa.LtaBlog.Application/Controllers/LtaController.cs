using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Eaa.LtaBlog.Application.RavenDb;
using System.Web.Mvc;
using Eaa.LtaBlog.Application.Core.Commands;
using Eaa.LtaBlog.Application.Core.Entities;
using Eaa.LtaBlog.Application.Core.Entities.Security;
using Eaa.LtaBlog.Application.DependencyResolution;

namespace Eaa.LtaBlog.Application.Controllers
{
	public class LtaController : Controller
	{
		protected IDocumentSession RavenSession { get { return RavenDbStore.CurrentSession; } }

		protected ICommandProcessor CommandProcessor { get { return IoC.Container.GetInstance<ICommandProcessor>(); } } 

		public User LoggedUser
		{
			get
			{
				var principal = User as LtaPrincipal;
				if (principal == null)
					return null;

				return principal.Identity as User;
			}
		}

		public void Notify(string message, NotifyType type = NotifyType.Success, NotifyPosition position = NotifyPosition.TopRight)
		{
			TempData["Notify_Message"] = message;
			TempData["Notify_Type"] = type.ToString().Uncapitalize();
			TempData["Notify_Position"] = position.ToString().Uncapitalize();
		}
	}

	public enum NotifyPosition
	{
		Top,
		TopRight,
		TopLeft,
		TopCenter,
		Center,
		Bottom
	}

	public enum NotifyType
	{
		Success,
		Error
	}
}