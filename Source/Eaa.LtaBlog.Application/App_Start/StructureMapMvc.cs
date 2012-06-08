using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using Eaa.LtaBlog.Application.DependencyResolution;
using Eaa.LtaBlog.Application.RavenDb;
using Eaa.LtaBlog.Application.DependencyResolution.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.StructureMapMvc), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	public static class StructureMapMvc
	{
		public static void Start()
		{
			var container = IoC.Initialize();
			ControllerBuilder.Current.SetControllerFactory(new IoCControllerFactory());
			//DependencyResolver.SetResolver(new SmDependencyResolver(container));
		}
	}
}