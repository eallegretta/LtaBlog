using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data
{
	/// <summary>
	/// <see cref="System.Data.IDataRecord"/> extension methods.
	/// </summary>
	public static class IDataRecordExtensions
	{

		/// <summary>
		/// Determines whether the specified data record has a column.
		/// </summary>
		/// <param name="dataRecord">The data record.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns>
		/// 	<c>true</c> if the specified data record has the column; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasColumn(this IDataRecord dataRecord, string columnName)
		{
			for (int i = 0; i < dataRecord.FieldCount; i++)
			{
				if (dataRecord.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the value from the data record.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataRecord">The data record.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns>The value casted to T</returns>
		public static T GetValue<T>(this IDataRecord dataRecord, string columnName)
		{
			object value = dataRecord[columnName];

			if (value == null || value == DBNull.Value)
				return default(T);

			Type type = typeof(T);

			if (typeof(T).IsNullable())
			{
				type = Nullable.GetUnderlyingType(type);
			}

			return (T)Convert.ChangeType(value, type);
		}
	}
}
