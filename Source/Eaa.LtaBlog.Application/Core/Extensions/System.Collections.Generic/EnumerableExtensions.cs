using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Collections.Generic
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Duplicate<T>(this IEnumerable<T> en)
		{
			return Duplicate<T>(en, 1);
		}

		public static IEnumerable<T> Duplicate<T>(this IEnumerable<T> en, int times)
		{
			IList<T> list = new List<T>();
			for (int count = 0; count <= times; count++)
			{
				foreach (var item in en)
				{
					list.Add(item);
				}
			}
			return list;
		}

	}
}
