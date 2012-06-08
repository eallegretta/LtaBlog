using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching.Providers
{
	/// <summary>
	/// Basic Hashtable Cache Provider, DO NOT USE ON PRODUCTION CODE, ONLY FOR TESTING PURPOSES.
	/// </summary>
	public class HashtableCacheProvider: ICacheProvider
	{
		private static Hashtable _storage = new Hashtable();

		#region ICacheProvider Members

		public void Add(string key, object value)
		{
			_storage.Add(key, value);
		}

		public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
		{
			_storage.Add(key, value);
		}

		public bool Contains(string key)
		{
			return _storage.ContainsKey(key);
		}

		public int Count
		{
			get { return _storage.Count; }
		}

		public void Flush()
		{
			_storage.Clear();
		}

		public object Get(string key)
		{
			return _storage[key];
		}

		public void Remove(string key)
		{
			_storage.Remove(key);
		}

		public object this[string key]
		{
			get { return _storage[key]; }
		}

		#endregion
	}
}
