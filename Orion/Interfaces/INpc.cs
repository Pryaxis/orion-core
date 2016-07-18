namespace Orion.Interfaces
{
	/// <summary>
	/// Encapsulates a Terraria NPC.
	/// </summary>
	public interface INpc : IEntity
	{
		/// <summary>
		/// Gets the backing Terraria NPC.
		/// </summary>
		new Terraria.NPC Backing { get; }

		/// <summary>
		/// Gets or sets the HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets or sets the maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets the type ID.
		/// </summary>
		int Type { get; }

		// TODO: complete
	}
}
