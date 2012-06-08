using StructureMap;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using StructureMap.Interceptors;

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

							RegisterInterceptors(x);

							x.Scan(scan =>
									{
										scan.TheCallingAssembly();
										scan.LookForRegistries();
									});
						});

			return ObjectFactory.Container;
		}

		private static void RegisterInterceptors(IInitializationExpression exp)
		{
			var interceptorTypes = from t in typeof(IoC).Assembly.GetTypes()
								   where typeof(TypeInterceptor).IsAssignableFrom(t)
								   select t;

			foreach (var type in interceptorTypes)
			{
				exp.RegisterInterceptor(Activator.CreateInstance(type) as TypeInterceptor);
			}
		}
	}
}