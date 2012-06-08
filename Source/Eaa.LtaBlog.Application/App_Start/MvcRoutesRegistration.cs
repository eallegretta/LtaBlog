using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.MvcRoutesRegistration), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class MvcRoutesRegistration
	{
		public static void Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Posts", action = "List", id = UrlParameter.Optional } // Parameter defaults
			);

		}
	}
}