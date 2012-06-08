using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Eaa.LtaBlog.Application.DependencyResolution;

namespace Eaa.LtaBlog.Application.Modules
{
	/// <summary>
	/// An http module that initializes IOC injected Http Modules
	/// </summary>
	public class ContainerModule : IHttpModule
	{
		Lazy<IEnumerable<IHttpModule>> _modules
		  = new Lazy<IEnumerable<IHttpModule>>(RetrieveModules);

		private static IEnumerable<IHttpModule> RetrieveModules()
		{
			return IoC.Container.GetAllInstances<IHttpModule>();
		}

		public void Dispose()
		{
			var modules = _modules.Value;
			foreach (var module in modules)
			{
				var disposableModule = module as IDisposable;
				if (disposableModule != null)
				{
					disposableModule.Dispose();
				}
			}
		}

		public void Init(HttpApplication context)
		{
			var modules = _modules.Value;
			foreach (var module in modules)
			{
				module.Init(context);
			}
		}
	}
}