using StructureMap;
using System;
using System.Web.Mvc;

namespace Eaa.LtaBlog.Application.DependencyResolution
{
	public static class IoC
	{
		public static IContainer Container
		{
			get
			{
				return ObjectFactory.Container;
			}
		}

		public static IContainer Initialize()
		{
			return Initialize(null);
		}

		public static IContainer Initialize(Action<IInitializationExpression> expression)
		{
			ObjectFactory.Initialize(x =>
						{
							if (expression != null)
								expression(x);

							x.Scan(scan =>
									{
										scan.TheCallingAssembly();
										scan.LookForRegistries();
									});
						});

			return ObjectFactory.Container;
		}
	}
}