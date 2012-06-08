using System.Text;
using System.Reflection;
using System.Collections;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// A Basic Cache Key Provider. The key that generates is the concatenation of the Type FullName, method name and argument values
	/// <example>
	/// Verizon.CCSDashboard.BusinessLogic.DefaultMetricService.HelloWorld_hello_1
	/// </example>
	/// </summary>
	public class BasicCacheKeyProvider : ICacheKeyProvider
	{
		#region ICacheKeyProvider Members


		protected virtual string GetTargetName(object target, MethodInfo method, IList arguments)
		{
			return target.GetType().FullName;
		}

		protected virtual string GetMethodName(object target, MethodInfo method, IList arguments)
		{
			return method.Name;
		}

		protected virtual string GetArguments(object target, MethodInfo method, IList arguments)
		{
			StringBuilder hash = new StringBuilder();
			if (arguments != null && arguments.Count > 0)
			{
				hash.Append("_");
				foreach (object argument in arguments)
				{
					if (!(argument is string) && argument is IEnumerable)
					{
						var collection = argument as IEnumerable;
						var enumerator = collection.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (enumerator.Current != null)
							{
								hash.Append(enumerator.Current);
							}
							else
							{
								hash.Append("NULL");
							}
							hash.Append("_");
						}

					}
					else
					{
						if (argument == null)
							hash.Append("NULL");
						else
							hash.Append(argument.ToString());
						hash.Append("_");
					}
				}
			}
			return hash.ToString();
		}

		/// <summary>
		/// Gets the cache key for a method.
		/// </summary>
		/// <param name="target">The method instance target.</param>
		/// <param name="method">The method information.</param>
		/// <param name="arguments">The arguments.</param>
		/// <returns>The cache key.</returns>
		public string GetCacheKeyForMethod(object target, MethodInfo method, IList arguments)
		{
			StringBuilder hash = new StringBuilder();
			
			hash.Append(GetTargetName(target, method, arguments));
			hash.Append(".");
			hash.Append(GetMethodName(target, method, arguments));
			hash.Append(".");
			hash.Append(GetArguments(target, method, arguments));
			
			return hash.ToString();
		}

		#endregion
	}
}
