using System;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// Specifies a Caching absolute time item expiration policy.
	/// </summary>
	public class AbsoluteTime : ICacheItemExpiration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AbsoluteTime"/> class.
		/// </summary>
		/// <param name="absoluteTime">The absolute time.</param>
		public AbsoluteTime(DateTime absoluteTime)
		{
			this.ExpirationTime = absoluteTime;
		}
		/// <summary>
		/// Gets or sets the expiration time.
		/// </summary>
		/// <value>The expiration time.</value>
		public DateTime ExpirationTime { get; set; }
	}

}
