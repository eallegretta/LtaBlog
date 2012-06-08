using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// The default implementation for the cache item serialization process.
	/// </summary>
	public class JsonCacheItemSerializer: ICacheItemSerializer
	{
		#region ICacheItemSerializer Members

		/// <summary>
		/// Serializes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="target">The target.</param>
		/// <param name="method">The method.</param>
		/// <param name="methodParameters">The method parameters.</param>
		/// <returns>
		/// A string serialized representation of the data
		/// </returns>
		public string Serialize(object data, object target, System.Reflection.MethodInfo method, IList methodParameters)
		{
			return JsonConvert.SerializeObject(data);
		}

		/// <summary>
		/// Deserializes the specified serialized data.
		/// </summary>
		/// <param name="serializedData">The serialized data.</param>
		/// <param name="type">The data type of the serialized object.</param>
		/// <param name="target">The target.</param>
		/// <param name="method">The method.</param>
		/// <param name="methodParameters">The method parameters.</param>
		/// <returns>The data deserialized</returns>
		public object Deserialize(string serializedData, Type type, object target, System.Reflection.MethodInfo method, IList methodParameters)
		{
			return JsonConvert.DeserializeObject(serializedData, type);
		}
		#endregion
	}
}
