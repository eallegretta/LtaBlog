using System;
namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	public interface ICacheProviderFactory
	{
		ICacheProvider GetCacheProvider();
	}
}
