using Microsoft.Xna.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a wrapper around a Terraria NPC.
	/// </summary>
	public interface INpc
	{
		/// <summary>
		/// Gets or sets the NPC's HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets or sets the NPC's maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets the NPC's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the NPC's position.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the NPC's type ID.
		/// </summary>
		int Type { get; }

		/// <summary>
		/// Gets or sets the NPC's velocity.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria NPC.
		/// </summary>
		Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		void Kill();
	}
}
