namespace Orion.Interfaces
{
	/// <summary>
	/// Represents a Terraria NPC.
	/// </summary>
	public interface INpc : IEntity
	{
		/// <summary>
		/// Gets the HP value.
		/// </summary>
		int HP { get; }

		/// <summary>
		/// Gets the maximum HP value.
		/// </summary>
		int MaxHP { get; }

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		int Type { get; }
	}
}
