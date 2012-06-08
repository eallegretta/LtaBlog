using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Eaa.LtaBlog.Application.App_Start.Razor), "Start")]
namespace Eaa.LtaBlog.Application.App_Start
{
	/// <summary>
	/// Allow razor to user _ViewName
	/// 
	/// _Views are used as a convention for partial views
	/// </summary>
	public static class Razor
	{
		public static void Start()
		{
			// Clears all previously registered view engines.
			ViewEngines.Engines.Clear();

			// Registers our Razor C# specific view engine.
			// This can also be registered using dependency injection through the new IDependencyResolver interface.
			var viewEngine = new RazorViewEngine();

			viewEngine.AreaPartialViewLocationFormats = new List<string>(
						viewEngine.AreaPartialViewLocationFormats.Concat(new string[] 
						{
							"~/Areas/{2}/Views/{1}/_{0}.cshtml", 
							"~/Areas/{2}/Views/{1}/_{0}.vbhtml", 
							"~/Areas/{2}/Views/Shared/_{0}.cshtml", 
							"~/Areas/{2}/Views/Shared/_{0}.vbhtml"
						})).ToArray();

			viewEngine.PartialViewLocationFormats = new List<string>(
						viewEngine.PartialViewLocationFormats.Concat(new string[] 
						{
							"~/Views/{1}/_{0}.cshtml", 
							"~/Views/{1}/_{0}.vbhtml", 
							"~/Views/Shared/_{0}.cshtml", 
							"~/Views/Shared/_{0}.vbhtml"
						})).ToArray();

			ViewEngines.Engines.Add(viewEngine);
		}
	}
}