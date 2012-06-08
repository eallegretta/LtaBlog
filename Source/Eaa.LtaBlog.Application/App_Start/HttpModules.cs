using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Modules;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.HttpModules), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class HttpModules
	{
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(ContainerModule));

			// No need to add more modules here since all the modules are registered using the IOC container
		}
	}
}