namespace Orion.Interfaces
{
	/// <summary>
	/// Wraps a <see cref="Terraria.NPC"/>.
	/// </summary>
	public interface INpc : IEntity
	{
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

		/// <summary>
		/// Gets the wrapped <see cref="Terraria.NPC"/>.
		/// </summary>
		Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		void Kill();
	}
}
