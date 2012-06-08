using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
	}
}