using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Text
{
	/// <summary>
	/// <see cref="System.Text.StringBuilder" /> extension methods.
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Determines whether the string builder contains the text.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="text">The text.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified text]; otherwise, <c>false</c>.
		/// </returns>
		public static bool Contains(this StringBuilder builder, string text)
		{
			return builder.ToString().IndexOf(text) > -1;
		}

		/// <summary>
		/// Determines whether the string builder contains the text applying a <see cref="System.StringComparison"/>.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="text">The text.</param>
		/// <param name="comparissonType">Type of the comparisson.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified text]; otherwise, <c>false</c>.
		/// </returns>
		public static bool Contains(this StringBuilder builder, string text, StringComparison comparissonType)
		{
			return builder.ToString().IndexOf(text, comparissonType) > -1;
		}


		/// <summary>
		/// Replaces the case insensitive.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns>A new replaced instance.</returns>
		public static StringBuilder ReplaceCaseInsensitive(this StringBuilder builder, string oldValue, string newValue)
		{
			return new StringBuilder(builder.ToString().ReplaceCaseInsensitive(oldValue, newValue));
		}
	}
}
