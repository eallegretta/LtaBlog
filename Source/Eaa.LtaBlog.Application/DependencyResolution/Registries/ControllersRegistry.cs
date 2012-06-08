using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;

namespace Eaa.LtaBlog.Application.DependencyResolution.Registries
{
	public class ControllersRegistry : Registry
	{
		public ControllersRegistry()
		{
			SetAllProperties(sc => sc.OfType<IController>());

			Scan(x =>
			{
				x.TheCallingAssembly();
				x.Include(t => t.Namespace != null
							&& !t.IsAbstract
							&& !t.IsInterface
							&& typeof(IController).IsAssignableFrom(t));
			});
		}
	}
}