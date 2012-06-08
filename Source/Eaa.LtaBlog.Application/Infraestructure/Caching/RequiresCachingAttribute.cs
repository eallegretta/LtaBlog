using System;
using Eaa.LtaBlog.Application.Properties;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// Attribute that determines if a class requires caching.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public class RequiresCachingAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequiresCachingAttribute"/> class.
		/// </summary>
		public RequiresCachingAttribute()
		{
			Timeout = Settings.Default.CacheAbsoluteExpirationMinutes;
			CacheAllMethods = false;
			CacheKeyProvider = Activator.CreateInstance<BasicCacheKeyProvider>();
			CacheItemSerializer = Activator.CreateInstance<JsonCacheItemSerializer>();
		}

		public RequiresCachingAttribute(Type cacheKeyProvider, Type cacheItemSerializer)
		{
			CacheKeyProvider =  Activator.CreateInstance(cacheKeyProvider) as ICacheKeyProvider;
			CacheItemSerializer = Activator.CreateInstance(cacheItemSerializer) as ICacheItemSerializer;
		}


		public ICacheKeyProvider CacheKeyProvider
		{
			get;
			set;
		}

		public ICacheItemSerializer CacheItemSerializer
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the timeout.
		/// </summary>
		/// <value>The timeout.</value>
		public int Timeout { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to cache all methods.
		/// </summary>
		/// <value><c>true</c> if cache all methods; otherwise, <c>false</c>.</value>
		public bool CacheAllMethods { get; set; }
	}
}
