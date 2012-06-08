using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using Eaa.LtaBlog.Application.Core.Commands;

namespace Eaa.LtaBlog.Application.DependencyResolution.Registries
{
	public class ServicesRegistry : Registry
	{
		public ServicesRegistry()
		{
			ForSingletonOf<ICommandProcessor>().Use<CommandProcessor>();
			
			Scan(x =>
			{
				x.TheCallingAssembly();

				x.IncludeNamespace("Eaa.LtaBlog.Application.Core.ServiceContracts");
				x.IncludeNamespace("Eaa.LtaBlog.Application.Services");

				x.WithDefaultConventions();
			});
		}
	}
}