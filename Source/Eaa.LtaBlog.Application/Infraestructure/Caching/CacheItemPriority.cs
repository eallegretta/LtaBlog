
namespace Eaa.LtaBlog.Application.Infraestructure.Caching
{
	/// <summary>
	/// The priority for a cache item
	/// </summary>
	public enum CacheItemPriority
	{
		/// <summary>
		/// Should never be seen in nature.
		/// </summary>
		None = 0,
		/// <summary>
		/// Low priority for scavenging.
		/// </summary>
		Low = 1,
		/// <summary>
		/// Normal priority for scavenging.
		/// </summary>
		Normal,
		/// <summary>
		/// High priority for scavenging.
		/// </summary>
		High,
		/// <summary>
		/// Non-removable priority for scavenging.
		/// </summary>
		NotRemovable
	}

}
