using System.Reflection;
using System.Collections;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// Defines the contract for all the cache key providers.
	/// </summary>
	public interface ICacheKeyProvider
	{
		/// <summary>
		/// Gets the cache key for a method.
		/// </summary>
		/// <param name="target">The method instance target.</param>
		/// <param name="method">The method information.</param>
		/// <param name="arguments">The arguments.</param>
		/// <returns>The cache key.</returns>
		string GetCacheKeyForMethod(object target, MethodInfo method, IList arguments);
	}
}
