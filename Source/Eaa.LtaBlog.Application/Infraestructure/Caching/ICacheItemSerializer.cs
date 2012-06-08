using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// Defines the contract for a custom cache item serialization.
	/// </summary>
	public interface ICacheItemSerializer
	{
		/// <summary>
		/// Serializes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="target">The target.</param>
		/// <param name="method">The method.</param>
		/// <param name="methodParameters">The method parameters.</param>
		/// <returns>A string serialized representation of the data</returns>
		string Serialize(object data, object target, MethodInfo method, IList methodParameters);

		/// <summary>
		/// Deserializes the specified serialized data.
		/// </summary>
		/// <param name="serializedData">The serialized data.</param>
		/// <param name="type">The data type of the serialized object.</param>
		/// <param name="target">The target.</param>
		/// <param name="method">The method.</param>
		/// <param name="methodParameters">The method parameters.</param>
		/// <returns>The data deserialized</returns>
		object Deserialize(string serializedData, Type type, object target, MethodInfo method,
			IList methodParameters);
	}
}
