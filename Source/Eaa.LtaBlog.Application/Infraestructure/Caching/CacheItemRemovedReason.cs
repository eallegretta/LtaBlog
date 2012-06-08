
namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// The reason that the cache item was removed.
	/// </summary>
	public enum CacheItemRemovedReason
	{
		/// <summary>
		/// The item has expired.
		/// </summary>
		Expired,

		/// <summary>
		/// The item was manually removed from the cache.
		/// </summary>
		Removed,

		/// <summary>
		/// The item was removed by the scavenger because it had a lower priority that any other item in the cache.
		/// </summary>
		Scavenged,

		/// <summary>
		/// Reserved. Do not use.
		/// </summary>
		Unknown = 9999,
		/// <summary>
		/// The item was removed from the cache because the cache dependency associated with it changed.
		/// </summary>
		DependencyChanged = 4,
	}

}
