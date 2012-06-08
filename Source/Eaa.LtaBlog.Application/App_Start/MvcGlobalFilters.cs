using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eaa.LtaBlog.Application.RavenDb.Web;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.MvcGlobalFilters), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class MvcGlobalFilters
	{
		public static void Start()
		{
			RegisterGlobalFilters(GlobalFilters.Filters);
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new RavenDbGlobalActionFilter());
		}
	}
}