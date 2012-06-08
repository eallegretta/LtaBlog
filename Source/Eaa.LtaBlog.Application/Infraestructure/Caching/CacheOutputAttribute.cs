using System;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// Attribute that indicates if the output of a method should be cached
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class CacheOutputAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheOutputAttribute"/> class.
		/// </summary>
		public CacheOutputAttribute()
			: this(typeof(BasicCacheKeyProvider), typeof(JsonCacheItemSerializer))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheOutputAttribute"/> class.
		/// </summary>
		/// <param name="cacheKeyProviderType">Type of the cache key provider.</param>
		public CacheOutputAttribute(Type cacheKeyProviderType)
			: this(cacheKeyProviderType, typeof(JsonCacheItemSerializer))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheOutputAttribute"/> class.
		/// </summary>
		/// <param name="cacheKeyProviderType">Type of the cache key provider.</param>
		/// <param name="cacheItemSerializerType">Type of the cache item serializer.</param>
		public CacheOutputAttribute(Type cacheKeyProviderType, Type cacheItemSerializerType)
		{
			CacheKeyProvider = Activator.CreateInstance(cacheKeyProviderType) as ICacheKeyProvider;
			CacheItemSerializer = Activator.CreateInstance(cacheItemSerializerType)	as ICacheItemSerializer;
		}

		/// <summary>
		/// Gets or sets the cache key provider.
		/// </summary>
		/// <value>The cache key provider.</value>
		public ICacheKeyProvider CacheKeyProvider { get; private set; }

		/// <summary>
		/// Gets or sets the cache item serializer.
		/// </summary>
		/// <value>The cache item serializer.</value>
		public ICacheItemSerializer CacheItemSerializer { get; private set; }
	}
}
