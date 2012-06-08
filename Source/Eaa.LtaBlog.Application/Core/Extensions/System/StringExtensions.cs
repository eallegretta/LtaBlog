using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace System
{
	public static class StringExtensions
	{
		public static string Uncapitalize(this string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return text;

			if (text.Length == 1)
				return text[0].ToString().ToLowerInvariant();

			return text[0].ToString().ToLowerInvariant() + text.Substring(1);
		}

		public static string[] TrimAll(this string[] source)
		{
			string[] output = new string[source.Length];
			for (int index = 0; index < output.Length; index++)
			{
				string value = source[index];
				if (value != null)
					output[index] = value.Trim();
			}
			return output;
		}

		public static string[] ChangeCasingAll(this string[] source, bool makeLowercase)
		{
			string[] output = new string[source.Length];
			for (int index = 0; index < output.Length; index++)
			{
				string value = source[index];
				if (value != null)
					output[index] = makeLowercase ? value.ToLowerInvariant() : value.ToUpperInvariant();
			}
			return output;
		}


		/// <summary>
		/// Determines whether the string contains the text applying a <see cref="System.StringComparison"/>.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="text">The text.</param>
		/// <param name="comparissonType">Type of the comparisson.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified text]; otherwise, <c>false</c>.
		/// </returns>
		public static bool Contains(this string src, string text, StringComparison comparissonType)
		{
			return src.ToString().IndexOf(text, comparissonType) > -1;
		}


		/// <summary>
		/// Performs a case insensitive string replacement.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns>A new replaced instance.</returns>
		public static string ReplaceCaseInsensitive(this string src, string oldValue, string newValue)
		{
			string original = src;
			string pattern = oldValue;
			string replacement = newValue;

			int count, position0, position1;
			count = position0 = position1 = 0;
			string upperString = original.ToUpper();
			string upperPattern = pattern.ToUpper();
			int inc = (original.Length / pattern.Length) *
					  (replacement.Length - pattern.Length);
			char[] chars = new char[original.Length + Math.Max(0, inc)];
			while ((position1 = upperString.IndexOf(upperPattern,
											  position0)) != -1)
			{
				for (int i = position0; i < position1; ++i)
					chars[count++] = original[i];
				for (int i = 0; i < replacement.Length; ++i)
					chars[count++] = replacement[i];
				position0 = position1 + pattern.Length;
			}
			if (position0 == 0) return original;
			for (int i = position0; i < original.Length; ++i)
				chars[count++] = original[i];
			return new string(chars, 0, count);
		}

		// TODO:
		//  - Add the following methods: 
		//     -> IsStrongPassword (http://regexlib.com/REDetails.aspx?regexp_id=2062)
		/// <summary>
		/// Capitalizes the current string object.
		/// </summary>
		public static string Capitalize(this string s)
		{
			return string.IsNullOrEmpty(s) ? s : s.Substring(0, 1).ToUpper() + (s.Length > 1 ? s.Substring(1) : string.Empty);
		}

		/// <summary>
		/// Returns an empty string if the current string object is null.
		/// </summary>
		public static string DefaultIfNull(this string s)
		{
			return s != null ? s : string.Empty;
		}

		/// <summary>
		/// Returns the specified default value if the current string object is null.
		/// </summary>
		public static string DefaultIfNull(this string s, string defaultValue)
		{
			return s != null ? s : (defaultValue ?? string.Empty);
		}

		/// <summary>
		/// Returns the specified default value if the current string object is null 
		/// or empty.
		/// </summary>
		public static string DefaultIfNullOrEmpty(this string s, string defaultValue)
		{
			return string.IsNullOrEmpty(s) ? s : defaultValue;
		}

		/// <summary>
		/// Indicates whether the current string object is a valid emailaddresss.
		/// </summary>
		public static bool IsEmailAddress(this string s)
		{
			return (new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")).IsMatch(s);
		}

		/// <summary>
		/// Indicates whether the specified comma-separated value list contains the current 
		/// string object.
		/// </summary>
		public static bool IsIn(this string s, string value)
		{
			return s.IsIn(value, ",");
		}

		/// <summary>
		/// Indicates whether the specified value list contains the current string object.
		/// </summary>
		public static bool IsIn(this string s, string value, string separator)
		{
			if (string.IsNullOrEmpty(value))
				return string.IsNullOrEmpty(s);

			return s.IsIn(value.Split(separator.ToCharArray()));
		}

		/// <summary>
		/// Indicates whether the specified value list contains the current string object.
		/// </summary>
		public static bool IsIn(this string s, string[] value)
		{
			if (value == null || value.Length == 0)
				return string.IsNullOrEmpty(s);

			foreach (string v in value)
			{
				if (s.Equals(v.Trim()))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Indicates whether the regular expression finds a match in the current string 
		/// object for the specified pattern.
		/// </summary>
		public static bool IsMatch(this string s, string pattern)
		{
			return (new Regex(pattern)).IsMatch(s);
		}

		/// <summary>
		/// Indicates whether the current string object is null or an empty string.
		/// </summary>
		public static bool IsNullOrEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}

		/// <summary>
		/// Determines whether the string is null, empty or only contains white spaces.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns>
		/// 	<c>true</c> if the string is null, empty or only contains white spaces; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrWhiteSpace(this string s)
		{
			if (s == null)
				return true;

			if (s.Length == 0)
				return true;

			if (s.Trim().Length == 0)
				return true;

			return false;
		}

		/// <summary>
		/// Retrieves a substring (left-side) of the specified length from the current 
		/// string object.
		/// </summary>
		public static string Left(this string s, int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("length");

			if (length == 0 || string.IsNullOrEmpty(s))
				return string.Empty;

			return length > s.Length ? s : s.Substring(0, length);
		}

		/// <summary>
		/// Returns the current string with leading spaces.
		/// </summary>
		public static string Prepend(this string s, int count)
		{
			return Prepend(s, count, " ");
		}

		/// <summary>
		/// Returns the current string with the leading string.
		/// </summary>
		public static string Prepend(this string s, int count, string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentNullException("indentationString");

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < count; i++)
				sb.Append(value);
			sb.Append(s);

			return sb.ToString();
		}

		/// <summary>
		/// Returns the current string with leading HTML blank.
		/// </summary>
		public static string PrependWithNbsp(this string s, int count)
		{
			return Prepend(s, count, "&nbsp;");
		}

		/// <summary>
		/// Returns the current string with leading tabs.
		/// </summary>
		public static string PrependWithTabs(this string s, int count)
		{
			return Prepend(s, count, "\t");
		}

		/// <summary>
		/// Replaces variables (${PropertyName}) witin the current string object with
		/// the property values of the specified object.
		/// </summary>
		public static string Replace(this string s, object properties)
		{
			if (properties == null)
				return s;

			string rs = s;
			foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(properties))
				rs = rs.Replace("${" + pd.Name + "}", pd.GetValue(properties).ToString());

			return rs;
		}

		/// <summary>
		/// Replaces new line with BR sign.
		/// </summary>
		public static string ReplaceNewLineWithBR(this string s)
		{
			return s.Replace(Environment.NewLine, "<br />");
		}

		/// <summary>
		/// Retrieves a substring (right-side) of the specified length from the current 
		/// string object.
		/// </summary>
		public static string Right(this string s, int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("length");

			if (length == 0 || string.IsNullOrEmpty(s))
				return string.Empty;

			return length > s.Length ? s : s.Substring(s.Length - length, length);
		}

		/// <summary>

		/// Returns a string array containing the substrings from the current string 

		/// object that are delimited by the given separator.

		/// </summary>

		public static string[] Split(this string s, string separator)
		{
			if (string.IsNullOrEmpty(s))
				return new string[] { string.Empty };

			return s.Split(separator.ToCharArray());
		}

		/// <summary>

		/// Returns a Dictionary instance created from the current string 

		/// object if it contains a format like "firstkey=value1|second=Val2|...".

		/// </summary>

		public static Dictionary<string, string> ToDictionary(this string s)
		{
			return ToDictionary(s, "|");
		}

		/// <summary>

		/// Returns a Dictionary instance created from the current string 

		/// object if it contains a format like 

		/// "firstkey=value1[separator]second=Val2[separator]...".

		/// </summary>

		public static Dictionary<string, string> ToDictionary(this string s, string separator)
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();

			NameValueCollection collection = ToNameValueCollection(s, separator);
			foreach (string key in collection.AllKeys)
				dic.Add(key, collection[key]);

			return dic;
		}

		/// <summary>
		/// Applies the format to the current string and returns the formatted value
		/// </summary>
		/// <param name="s"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public static string ToFormat(this string s, string format)
		{
			return string.Format(format, s);
		}


		public static string ToBase64(this string s)
		{
			var encData = new byte[s.Length];
			encData = Encoding.UTF8.GetBytes(s);
			return Convert.ToBase64String(encData);
		}

		public static string FromBase64(this string s)
		{
			UTF8Encoding encoder = new UTF8Encoding();
			Decoder utf8Decode = encoder.GetDecoder();

			var encData = Convert.FromBase64String(s);
			int charCount = utf8Decode.GetCharCount(encData, 0, encData.Length);
			var decodedChars = new char[charCount];
			utf8Decode.GetChars(encData, 0, encData.Length, decodedChars, 0);
			return new String(decodedChars);
		}

		/// <summary>

		/// Converts the current string object into an bool object.

		/// </summary>

		public static bool ToBoolean(this string s)
		{
			return Boolean.Parse(s);
		}

		/// <summary>

		/// Converts the current string object into an bool object or returns 

		/// the defaultValue.

		/// </summary>

		public static bool ToBoolean(this string s, bool defaultValue)
		{
			bool value;

			return Boolean.TryParse(s, out value) ? value : defaultValue;
		}


		/// <summary>

		/// Converts the current string object into an byte object.

		/// </summary>

		public static byte ToByte(this string s)
		{
			return Byte.Parse(s);
		}

		/// <summary>

		/// Converts the current string object into an byte object or returns 

		/// the defaultValue.

		/// </summary>

		public static byte ToByte(this string s, byte defaultValue)
		{
			byte value;

			return Byte.TryParse(s, out value) ? value : defaultValue;
		}


		/// <summary>

		/// Converts the current string object into an integer object.

		/// </summary>

		public static int ToInt32(this string s)
		{
			return Int32.Parse(s);
		}

		/// <summary>

		/// Converts the current string object into an integer object or returns 

		/// the defaultValue.

		/// </summary>

		public static int ToInt32(this string s, int defaultValue)
		{
			int value;

			return Int32.TryParse(s, out value) ? value : defaultValue;
		}

		public static int? ToInt32Nullable(this string s)
		{
			return ToInt32Nullable(s, null);
		}

		public static int? ToInt32Nullable(this string s, int? defaultValue)
		{
			int value;

			return Int32.TryParse(s, out value) ? value : defaultValue;
		}


		public static float ToFloat(this string s, float defaultValue)
		{
			float value;

			return float.TryParse(s, out value) ? value : defaultValue;
		}

		public static float? ToFloatNullable(this string s)
		{
			return ToFloatNullable(s, null);
		}

		public static float? ToFloatNullable(this string s, float? defaultValue)
		{
			float value;

			return float.TryParse(s, out value) ? value : defaultValue;
		}

		public static double? ToDoubleNullable(this string s)
		{
			return ToDoubleNullable(s, null);
		}

		public static double? ToDoubleNullable(this string s, double? defaultValue)
		{
			double value;

			return double.TryParse(s, out value) ? value : defaultValue;
		}


		public static decimal? ToDecimalNullable(this string s)
		{
			return ToDecimalNullable(s, null);
		}

		public static decimal? ToDecimalNullable(this string s, decimal? defaultValue)
		{
			decimal value;

			return decimal.TryParse(s, out value) ? value : defaultValue;
		}

		/// <summary>

		/// Returns a List(string) instance from the current |-separated string.

		/// </summary>

		public static List<string> ToList(this string s)
		{
			return ToList(s, "|");
		}

		/// <summary>

		/// Returns a List(string) instance from the current string.

		/// </summary>

		public static List<string> ToList(this string s, string separator)
		{
			List<string> list = new List<string>();

			foreach (string e in s.Split(separator.ToCharArray()))
				list.Add(e.Trim());

			return list;
		}

		/// <summary>

		/// Returns a MD5 representation of the current string object.

		/// </summary>

		public static string ToMD5(this string s)
		{
			byte[] bytes = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(s));

			StringBuilder sb = new StringBuilder();
			foreach (byte b in bytes)
				sb.Append(b.ToString("x2").ToLower());

			return sb.ToString();
		}

		/// <summary>
		/// Returns a SHA256 representation of the current string object
		/// </summary>
		public static string ToSHA256(this string s)
		{
			using (var sha = SHA256.Create())
			{
				var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(s));
				return Convert.ToBase64String(computedHash);
			}
		}

		/// <summary>

		/// Returns a NameValueCollection instance created from the current string 

		/// object if it contains a format like "firstkey=value1|second=Val2|...".

		/// </summary>

		public static NameValueCollection ToNameValueCollection(this string s)
		{
			return ToNameValueCollection(s, "|");
		}

		/// <summary>

		/// Returns a NameValueCollection instance created from the current string 

		/// object if it contains a format like 

		/// "firstkey=value1[separator]second=Val2[separator]...".

		/// </summary>

		public static NameValueCollection ToNameValueCollection(this string s, string separator)
		{
			if (string.IsNullOrEmpty(separator))
				throw new ArgumentNullException("separator");

			NameValueCollection collection = new NameValueCollection();

			string[] nameValuePairs = s.Split(separator.ToCharArray());

			foreach (string nvs in nameValuePairs)
			{
				string[] nvp = nvs.Split("=".ToCharArray());

				string name = nvp[0].Trim();
				string value = nvp.Length > 1 ? nvp[1].Trim() : string.Empty;

				if (name.Length > 0)
					collection.Add(name, value);
			}

			return collection;
		}

		/// <summary>

		/// Removes the specified string value from the current string object at the

		/// beginning and the end.

		/// </summary>

		public static string Trim(this string s, string trimString)
		{
			return s.Trim(trimString.ToCharArray());
		}

		/// <summary>

		/// Encode string into JavaScript string format that is encode \ and ' characters.

		/// </summary>

		public static string ToJsString(this string s)
		{
			if (s == null)
			{
				return "";
			}

			return s.Replace("'", "\\'").Replace("\r", "").Replace("\n", "\\r\\n");
		}

		/// <summary>

		/// Encode string into JavaScript string format that is encode \ and specified quote characters.

		/// </summary>

		public static string ToJsString(this string s, char quote)
		{
			if (s == null)
			{
				return "";
			}

			if (quote != '"' && quote != '\'')
			{
				throw new ArgumentOutOfRangeException("quote is limited to ' or \"");
			}

			return s.Replace(quote.ToString(), String.Concat("\\", quote.ToString())).Replace("\r", "").Replace("\n", "\\r\\n");
		}

		/// <summary>

		/// Repeat string n times.

		/// </summary>

		public static string Repeat(this string s, int n)
		{
			if (s == null || n == 0)
			{
				return "";
			}

			if (n < 0)
			{
				throw new ArgumentOutOfRangeException("The n must be equal or larger then 0.");
			}

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < n; i++)
			{
				sb.Append(s);
			}

			return sb.ToString();
		}

		public static T To<T>(this string s)
		{
			return (T)Convert.ChangeType(s, typeof(T));
		}
	}
}