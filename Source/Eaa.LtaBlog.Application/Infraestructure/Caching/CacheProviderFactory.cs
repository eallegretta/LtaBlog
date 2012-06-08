using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eaa.LtaBlog.Application.Properties;


namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	public class CacheProviderFactory : ICacheProviderFactory
	{
		private static ICacheProviderFactory _defaultInstance = new CacheProviderFactory();

		public static ICacheProviderFactory Default { get { return _defaultInstance; } }

		public ICacheProvider GetCacheProvider()
		{
			return Activator.CreateInstance(Type.GetType(Settings.Default.CacheProvider)) as ICacheProvider;
		}
	}
}
