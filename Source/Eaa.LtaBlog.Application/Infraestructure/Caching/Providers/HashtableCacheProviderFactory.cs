using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching.Providers
{
	public class HashtableCacheProviderFactory: ICacheProviderFactory
	{
		private static HashtableCacheProvider _provider = new HashtableCacheProvider();

		#region ICacheProviderFactory Members

		public ICacheProvider GetCacheProvider()
		{
			return _provider;
		}

		#endregion
	}
}
