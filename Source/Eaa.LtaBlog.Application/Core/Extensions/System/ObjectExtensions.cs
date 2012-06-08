using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Linq.Expressions;
//using Newtonsoft.Json;

namespace System
{
	public static class ObjectExtensions
	{
		public static T GetAttribute<T>(this object obj) where T: Attribute
		{
			if(obj == null)
				return null;

			var attrs = obj.GetType().GetCustomAttributes(typeof(T), true);
			if (attrs.Length > 0)
				return (T)attrs[0];
			return null;
		}

		public static T GetAttribute<T>(this object obj, string propertyName) where T : Attribute
		{
			if(!obj.HasProperty(propertyName))
				return null;

			var property = obj.GetType().GetProperty(propertyName);

			if (!property.IsDefined(typeof(T), true))
				return null;

			var attrs = property.GetCustomAttributes(typeof(T), true);

			if (attrs.Length > 0)
				return (T)attrs[0];
			return null;
		}

		public static bool In(this object obj, params object[] values)
		{
			return In<object>(obj, values);
		}
		public static bool In<T>(this T obj, params T[] values)
		{
			foreach (T value in values)
			{
				if (obj.Equals(value))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Applies the format to the current object and returns the formatted value
		/// </summary>
		/// <param name="s"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public static string ToFormattedString(this object s, string format)
		{
			return string.Format(format, s);
		}

		public static void CopyTo<T>(this T fromRecord, T toRecord)
		{
			foreach (PropertyInfo field in typeof(T).GetProperties())
			{
				if(field.CanWrite)
					field.SetValue(toRecord, field.GetValue(fromRecord, null), null);
			}
		}

		/// <summary>
		/// Indicates whether the specified attribute type is defined for the current object 
		/// instance.
		/// </summary>
		public static bool HasCustomAttribute(this object o, Type attributeToFind)
		{
			if (typeof(Attribute).IsAssignableFrom(attributeToFind) == false)
				throw new ArgumentException("attributeToFind");

			object[] attributes = o.GetType().GetCustomAttributes(attributeToFind, true);

			return (attributes != null && attributes.Length > 0);
		}

		/// <summary>
		/// Get the represent value of the object in System.String type.
		/// </summary>
		/// <param name="obj">Extension object</param>
		/// <returns>string</returns>
		public static string ToValue(this object obj)
		{
			if (obj == null)
			{
				return "null";
			}
			else if (obj is DateTime)
			{
				return ((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss");
			}
			else if (obj is ValueType || obj is string)
			{
				if (obj is char)
				{
					return "'" + obj.ToString() + "'";
				}
				else if (obj is string)
				{
					return "\"" + obj.ToString() + "\"";
				}
				else
				{
					return obj.ToString();
				}
			}
			else if (obj is System.Collections.DictionaryEntry) // Hashtable
			{
				return ((System.Collections.DictionaryEntry)obj).Key.ToValue() + "=" + ((System.Collections.DictionaryEntry)obj).Value.ToValue();
			}
			else if (obj is IEnumerable)
			{
				StringBuilder sb = new StringBuilder();

				foreach (var item in (IEnumerable)obj)
				{
					if (obj is Hashtable)
					{
						DictionaryEntry ht = (DictionaryEntry)item;

						sb.Append(
							"[" +
							ht.Key.ToValue() +
							"]=[" +
							ht.Value.ToValue() +
							"],");
					}
					else if (obj is NameValueCollection)
					{
						sb.Append(
							"\"" + item.ToString() + "\"" +
							"=" +
							"\"" + ((NameValueCollection)obj)[item.ToString()] + "\"" + ",");
					}
					else
					{
						sb.Append(item.ToValue() + ",");
					}
				}

				sb.Length = sb.Length - 1;

				return sb.ToString();
				//return "IEnumerable { ... }";

			}
			else
			{
				return "{ " + obj.ToString() + "}";
			}
		}

		/// <summary>
		/// Get the all the values inside object in VERY detail.  Useful for debugging purpose.
		/// </summary>
		/// <param name="o">object</param>
		/// <param name="prefix">an object id or name</param>
		/// <returns>string</returns>
		public static string ToString(this object obj, string prefix)
		{
			return obj.ToString(prefix, 0);
		}

		/// <summary>
		/// Get the all the values inside object in VERY detail. Useful for debugging purpose.
		/// </summary>
		private static string ToString(this object obj, string prefix, int depth)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (obj == null || obj is ValueType || obj is string)
			{
				sb.Append(prefix);
				sb.Append(" = ");
				sb.Append(obj.ToValue());
				sb.AppendLine();
			}
			else if (obj is IEnumerable)
			{
				foreach (var item in (IEnumerable)obj)
				{
					if (obj is Hashtable)
					{
						DictionaryEntry ht = (DictionaryEntry)item;

						sb.AppendLine(
							prefix +
							"[" +
							ht.Key.ToValue() +
							"] = " +
							ht.Value.ToValue());
					}
					else if (obj is NameValueCollection)
					{
						sb.AppendLine(
							prefix +
							"[\"" + item.ToString() + "\"]" +
							" = " +
							"\"" + ((NameValueCollection)obj)[item.ToString()] + "\"");
					}
					else
					{
						sb.Append(item.ToValue());
					}
				}
			}
			else
			{
				MemberInfo[] members = obj.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);

				foreach (MemberInfo mi in members)
				{
					FieldInfo fi = mi as FieldInfo;
					PropertyInfo pi = mi as PropertyInfo;
					if (fi != null || pi != null)
					{
						sb.Append(prefix + "." + mi.Name + " = ");

						Type t = (fi != null) ? fi.FieldType : pi.PropertyType;

						if (t.IsValueType || t == typeof(string))
						{
							if (fi != null)
							{
								sb.Append(fi.GetValue(obj).ToValue());
							}
							else
							{
								sb.Append(pi.GetValue(obj, null).ToValue());
							}
						}
						else
						{
							if (typeof(IEnumerable).IsAssignableFrom(t))
							{
								sb.Append(pi.GetValue(obj, null).ToValue());
							}
							else
							{
								sb.AppendLine("{");
								sb.AppendLine(pi.GetValue(obj, null).ToString(prefix + "." + pi.Name, depth + 1));
								sb.Append("}");
							}
						}

						sb.AppendLine();
					}
				}
			}

			if (depth > 0)
			{
				StringBuilder sb1 = new StringBuilder();
				foreach (string line in sb.ToString().Split(Environment.NewLine))
				{
					if (!String.IsNullOrEmpty(line))
					{
						sb1.AppendLine(line.PrependWithTabs(1));
					}
				}

				return sb1.ToString().Trim();
			}
			else
			{
				return sb.ToString().Trim();
			}
		}

		/// <summary>
		/// Creates an Enumerable containing this item as it's only memeber.
		/// </summary>
		public static IEnumerable<T> ToSingleItemEnumerable<T>(this T objectToTranslateToEnumerable)
		{
			yield return objectToTranslateToEnumerable;
		}

		/// <summary>
		/// Creates a List containing this item as it's only memeber.
		/// </summary>
		public static IList<T> ToSingleItemList<T>(this T objectToTranslateToEnumerable)
		{
			return new List<T> { objectToTranslateToEnumerable };
		}

		/// <summary>
		/// Creates an Array containing this item as it's only memeber.
		/// </summary>
		public static T[] ToSingleItemArray<T>(this T objectToTranslateToEnumerable)
		{
			return new T[] { objectToTranslateToEnumerable };
		}


		/// <summary>
		/// Creates a deep copy of a object.
		/// 
		/// Note: object must be serializable.
		/// 
		/// Note: this will not handle an object with events. 
		/// However a work around is described here
		/// http://www.lhotka.net/WeBlog/CommentView.aspx?guid=776f44e8-aaec-4845-b649-e0d840e6de2c
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objectToCopy"></param>
		/// <returns></returns>
		public static T SerializedDeepCopy<T>(this T objectToCopy)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();

			binaryFormatter.Serialize(memoryStream, objectToCopy);
			memoryStream.Seek(0, SeekOrigin.Begin);

			return (T)binaryFormatter.Deserialize(memoryStream);
		}


		private class TargetProperty
		{
			public object Target { get; set; }
			public PropertyInfo Property { get; set; }

			public bool IsValid { get { return Target != null && Property != null; } }
		}

		private static TargetProperty GetTargetProperty(object source, string propertyName)
		{
			if (!propertyName.Contains("."))
				return new TargetProperty { Target = source, Property = source.GetType().GetProperty(propertyName) };

			string[] propertyPath = propertyName.Split('.');

			var targetProperty = new TargetProperty();

			targetProperty.Target = source;
			targetProperty.Property = source.GetType().GetProperty(propertyPath[0]);

			for (int propertyIndex = 1; propertyIndex < propertyPath.Length; propertyIndex++)
			{
				propertyName = propertyPath[propertyIndex];
				if (!propertyName.IsNullOrEmpty())
				{
					targetProperty.Target = targetProperty.Property.GetValue(targetProperty.Target, null);
					targetProperty.Property = targetProperty.Target.GetType().GetProperty(propertyName);
				}
			}

			return targetProperty;
		}


		public static bool HasProperty(this object source, string propertyName)
		{
			return GetTargetProperty(source, propertyName).Property != null;
		}

		public static object GetPropertyValue(this object source, string propertyName)
		{
			var targetProperty = GetTargetProperty(source, propertyName);
			if (targetProperty.IsValid)
			{
				return targetProperty.Property.GetValue(targetProperty.Target, null);
			}
			return null;
		}

		public static bool SetPropertyValue(this object source, string propertyName, object value)
		{
			var targetProperty = GetTargetProperty(source, propertyName);
			if(targetProperty.IsValid)
			{
				targetProperty.Property.SetValue(targetProperty.Target, value, null);
				return true;
			}
			return false;
		}

		public static IDictionary<string, TValue> ToDictionary<TValue>(this object source)
		{
			IDictionary<string, TValue> dic = new Dictionary<string, TValue>();

			foreach (var property in source.GetType().GetProperties())
			{
				dic.Add(property.Name, (TValue)property.GetValue(source, null));
			}
			return dic;
		}

		public static IDictionary<string, object> ToDictionary(this object source)
		{
			return ToDictionary<object>(source);
		}


		//public static string ToJSON(this object source)
		//{
		//    return JavaScriptConvert.SerializeObject(source);
		//}
	}
}
