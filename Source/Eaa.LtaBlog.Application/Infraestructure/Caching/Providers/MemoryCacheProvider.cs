using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using Eaa.LtaBlog.Application.Properties;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching.Providers
{
	public class MemoryCacheProvider: ICacheProvider
	{
		#region ICacheProvider Members

		/// <summary>
		/// Gets the cache.
		/// </summary>
		/// <value>The cache.</value>
		private MemoryCache Cache
		{
			get
			{
				return MemoryCache.Default;
			}
		}


		/// <summary>
		/// Adds new CacheItem to cache. If another item already exists with the same key, that item is removed before
		/// the new item is added. If any failure occurs during this process, the cache will not contain the item being added.
		/// Items added with this method will be not expire, and will have a Normal <see cref="CacheItemPriority"/> priority.
		/// </summary>
		/// <param name="key">Identifier for this CacheItem</param>
		/// <param name="value">Value to be stored in cache. May be null.</param>
		/// <exception cref="ArgumentNullException">Provided key is null</exception>
		/// <exception cref="ArgumentException">Provided key is an empty string</exception>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the CacheItems.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public void Add(string key, object value)
		{
			Cache.Add(key, value, DateTimeOffset.Now.AddMinutes(Settings.Default.CacheAbsoluteExpirationMinutes));
		}

		/// <summary>
		/// Adds new CacheItem to cache. If another item already exists with the same key, that item is removed before
		/// the new item is added. If any failure occurs during this process, the cache will not contain the item being added.
		/// </summary>
		/// <param name="key">Identifier for this CacheItem</param>
		/// <param name="value">Value to be stored in cache. May be null.</param>
		/// <param name="scavengingPriority">Specifies the new item's scavenging priority.
		/// See <see cref="CacheItemPriority"/> for more information.</param>
		/// <param name="refreshAction">Object provided to allow the cache to refresh a cache item that has been expired. May be null.</param>
		/// <param name="expirations">Param array specifying the expiration policies to be applied to this item. May be null or omitted.</param>
		/// <exception cref="ArgumentNullException">Provided key is null</exception>
		/// <exception cref="ArgumentException">Provided key is an empty string</exception>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the CacheItems.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
		{
			System.Runtime.Caching.CacheItemPriority cacheItemPriority = System.Runtime.Caching.CacheItemPriority.Default;

			switch (scavengingPriority)
			{
				case CacheItemPriority.Normal:
					cacheItemPriority = System.Runtime.Caching.CacheItemPriority.Default;
					break;
				case CacheItemPriority.None:
				case CacheItemPriority.Low:
				case CacheItemPriority.NotRemovable:
					cacheItemPriority = System.Runtime.Caching.CacheItemPriority.NotRemovable;
					break;
			}

			DateTime absoluteExpiration = DateTime.Now.AddMinutes(Settings.Default.CacheAbsoluteExpirationMinutes);
			TimeSpan slidingExpiration = TimeSpan.Zero;

			if (expirations != null)
			{
				foreach (var expiration in expirations)
				{
					if (expiration.GetType().IsOfType<AbsoluteTime>())
					{
						absoluteExpiration = ((AbsoluteTime)expiration).ExpirationTime;
					}
					else if (expiration.GetType().IsOfType<NeverExpired>())
					{
						absoluteExpiration = DateTime.MaxValue;
					}
					else if (expiration.GetType().IsOfType<SlidingTime>())
					{
						slidingExpiration = ((SlidingTime)expiration).SlidingExpiration;
					}
				}
			}

			Cache.Add(key, value, new CacheItemPolicy
			{
				AbsoluteExpiration = absoluteExpiration,
				SlidingExpiration = slidingExpiration,
				Priority = cacheItemPriority,
				RemovedCallback = args =>
				{
					CacheItemRemovedReason removalReason = CacheItemRemovedReason.Unknown;
					switch (args.RemovedReason)
					{
						case System.Runtime.Caching.CacheEntryRemovedReason.ChangeMonitorChanged:
							removalReason = CacheItemRemovedReason.DependencyChanged;
							break;
						case System.Runtime.Caching.CacheEntryRemovedReason.Expired:
							removalReason = CacheItemRemovedReason.Expired;
							break;
						case System.Runtime.Caching.CacheEntryRemovedReason.Evicted:
							removalReason = CacheItemRemovedReason.Scavenged;
							break;
					}
					if (refreshAction != null)
						refreshAction.Refresh(args.CacheItem.Key, args.CacheItem.Value, removalReason);
				}
				
			});
		}

		/// <summary>
		/// Returns true if key refers to item current stored in cache
		/// </summary>
		/// <param name="key">Key of item to check for</param>
		/// <returns>
		/// True if item referenced by key is in the cache
		/// </returns>
		public bool Contains(string key)
		{
			return Cache[key] != null;
		}

		/// <summary>
		/// Returns the number of items currently in the cache.
		/// </summary>
		/// <value></value>
		public int Count
		{
			get { return (int)Cache.GetCount(); }
		}

		/// <summary>
		/// Removes all items from the cache. If an error occurs during the removal, the cache is left unchanged.
		/// </summary>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the CacheItems.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public void Flush()
		{
			foreach(var key in Cache.GetValues(null).Keys)
			{
				Cache.Remove(key);
			}
		}

		/// <summary>
		/// Returns the value associated with the given key.
		/// </summary>
		/// <param name="key">Key of item to return from cache.</param>
		/// <returns>Value stored in cache</returns>
		/// <exception cref="ArgumentNullException">Provided key is null</exception>
		/// <exception cref="ArgumentException">Provided key is an empty string</exception>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the CacheItems.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public object Get(string key)
		{
			return Cache[key];
		}

		/// <summary>
		/// Removes the given item from the cache. If no item exists with that key, this method does nothing.
		/// </summary>
		/// <param name="key">Key of item to remove from cache.</param>
		/// <exception cref="ArgumentNullException">Provided key is null</exception>
		/// <exception cref="ArgumentException">Provided key is an empty string</exception>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the CacheItems.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public void Remove(string key)
		{
			Cache.Remove(key);
		}

		/// <summary>
		/// Returns the item identified by the provided key
		/// </summary>
		/// <value></value>
		/// <exception cref="ArgumentNullException">Provided key is null</exception>
		/// <exception cref="ArgumentException">Provided key is an empty string</exception>
		/// <remarks>The CacheManager can be configured to use different storage mechanisms in which to store the cache items.
		/// Each of these storage mechanisms can throw exceptions particular to their own implementations.</remarks>
		public object this[string key]
		{
			get { return Cache[key]; }
		}

		#endregion
	}
}
