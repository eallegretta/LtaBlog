using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace System
{
	public static class TypeExtensions
	{

		public static T GetCustomAttribute<T>(this ICustomAttributeProvider type) where T : class
		{
			return type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
		}

		public static bool IsNullable(this Type theType)
		{
			return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
		}

		public static bool Implements<INTERFACE_TYPE>(this Type type)
		{
			return ImplementsAny(type, typeof(INTERFACE_TYPE));
		}

		public static bool ImplementsAny(this Type type, params Type[] interfaceTypes)
		{
			return type.GetInterfaces().Where(t => IsOfType(t, interfaceTypes)).Any();
		}
		public static bool ImplementsAll(this Type type, params Type[] interfaceTypes)
		{
			Type[] interfaces = type.GetInterfaces();

			foreach (Type interfaceType in interfaceTypes)
			{
				if (!interfaces.Where(t => IsOfType(t, interfaceType)).Any())
					return false;
			}
			return true;
		}

		public static bool IsOfType<OTHER_TYPE>(this Type type)
		{
			return IsOfType(type, typeof(OTHER_TYPE));
		}

		public static bool IsOfType(this Type type, params Type[] otherTypes)
		{
			return IsOfType(type, false, otherTypes);
		}

		public static bool IsOfType(this Type type, bool includeNullables, params Type[] otherTypes)
		{
			if (includeNullables && type.IsNullable())
				type = Nullable.GetUnderlyingType(type);

			foreach (var otherType in otherTypes)
			{
				if(otherType.IsAssignableFrom(type))
					return true;
			}

			return false;
		}
	}
}
