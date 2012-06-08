using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using Eaa.LtaBlog.Application.Modules;

namespace Eaa.LtaBlog.Application.DependencyResolution.Registries
{
	public class HttpModulesRegistry: Registry
	{
		public HttpModulesRegistry()
		{
			Scan(x =>
				{
					x.TheCallingAssembly();
					x.ExcludeType<ContainerModule>();
					x.AddAllTypesOf<IHttpModule>();
				});
		}
	}
}