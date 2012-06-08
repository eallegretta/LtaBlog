using System;

namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// A sliding time cache item expiration policy.
	/// </summary>
	public class SlidingTime : ICacheItemExpiration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingTime"/> class.
		/// </summary>
		/// <param name="timespan">The timespan.</param>
		public SlidingTime(string timespan) :
			this(TimeSpan.Parse(timespan))
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SlidingTime"/> class.
		/// </summary>
		/// <param name="sildingExpiration">The silding expiration.</param>
		public SlidingTime(TimeSpan sildingExpiration)
		{
			this.SlidingExpiration = sildingExpiration;
		}
		/// <summary>
		/// Gets or sets the sliding expiration.
		/// </summary>
		/// <value>The sliding expiration.</value>
		public TimeSpan SlidingExpiration { get; set; }
	}

}
