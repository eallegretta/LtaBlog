using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Interceptors;
using Eaa.LtaBlog.Application.Infraestructure.Caching;
using Castle.DynamicProxy;
using System.Runtime.Caching;

namespace Eaa.LtaBlog.Application.DependencyResolution.Interceptors
{
	public class CachingInterceptor: TypeInterceptor, IInterceptor
	{
		private const string HASHED_DATA_TYPE_FORMAT = "DataType_{0}";

		private static ProxyGenerator _dynamicProxy = new ProxyGenerator();

		#region TypeInterceptor Members

		public bool MatchesType(Type type)
		{
			var requiresCachingAttr = type.GetCustomAttributes(typeof(RequiresCachingAttribute), true).FirstOrDefault() as RequiresCachingAttribute;

			if (requiresCachingAttr != null)
			{
				return true;
			}

			return false;
		}

		#endregion

		#region InstanceInterceptor Members

		public object Process(object target, StructureMap.IContext context)
		{
			return _dynamicProxy.CreateInterfaceProxyWithTarget(target.GetType().GetInterfaces().First(), target, this);
		}

		#endregion

		/// <summary>
		/// Caches the invocation.
		/// </summary>
		/// <param name="invocation">The invocation.</param>
		/// <param name="cacheKeyProvider">The cache key provider.</param>
		/// <param name="cacheItemSerializer">The cache item serializer.</param>
		/// <param name="cacheStorageMode">The cache storage mode.</param>
		private void CacheInvocation(IInvocation invocation, ICacheKeyProvider cacheKeyProvider, ICacheItemSerializer cacheItemSerializer)
		{
			string hash = cacheKeyProvider.GetCacheKeyForMethod(invocation.InvocationTarget,
				invocation.MethodInvocationTarget, invocation.Arguments);

			string hashedObjectDataType = string.Format(HASHED_DATA_TYPE_FORMAT, hash);

			var cacheProvider = CacheProviderFactory.Default.GetCacheProvider();

			Type type = cacheProvider[hashedObjectDataType] as Type;
			object data = null;
			if (type != null && cacheProvider[hash] != null)
				data = cacheItemSerializer.Deserialize(cacheProvider[hash].ToString(), type, 
					invocation.InvocationTarget, invocation.Method, invocation.Arguments);

			if (data == null)
			{
				invocation.Proceed();
				data = invocation.ReturnValue;
				if (data != null)
				{
					cacheProvider.Add(hashedObjectDataType, invocation.Method.ReturnType);
					cacheProvider.Add(hash, cacheItemSerializer.Serialize(data, invocation.InvocationTarget,
						invocation.Method, invocation.Arguments));
				}
			}
			else
				invocation.ReturnValue = data;
		}


		#region IInterceptor Members

		public void Intercept(IInvocation invocation)
		{
			var requiresCachingAttr = invocation.TargetType.GetCustomAttribute<RequiresCachingAttribute>();

			if (requiresCachingAttr == null)
			{
				invocation.Proceed();
				return;
			}

			var cacheOutputAttr = invocation.MethodInvocationTarget.GetCustomAttribute<CacheOutputAttribute>();
			if (cacheOutputAttr != null)
			{
				CacheInvocation(invocation, cacheOutputAttr.CacheKeyProvider, cacheOutputAttr.CacheItemSerializer);
			}
			else if (requiresCachingAttr.CacheAllMethods)
				CacheInvocation(invocation, new BasicCacheKeyProvider(), new JsonCacheItemSerializer());
			else
				invocation.Proceed();
		}

		#endregion
	}
}