using Microsoft.Xna.Framework;

namespace Orion.Npcs
{
	/// <summary>
	/// Provides a wrapper around a Terraria NPC instance.
	/// </summary>
	public interface INpc
	{
		/// <summary>
		/// Gets or sets the NPC's damage.
		/// </summary>
		int Damage { get; set; }

		/// <summary>
		/// Gets or sets the NPC's defense.
		/// </summary>
		int Defense { get; set; }

		/// <summary>
		/// Gets or sets the NPC's health.
		/// </summary>
		int Health { get; set; }

		/// <summary>
		/// Gets or sets the NPC's height in pixels.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the NPC is a boss.
		/// </summary>
		bool IsBoss { get; set; }

		/// <summary>
		/// Gets or sets the NPC's maximum health.
		/// </summary>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets or sets the NPC's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the NPC's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the NPC's <see cref="NpcType"/>.
		/// </summary>
		NpcType Type { get; }

		/// <summary>
		/// Gets or sets the NPC's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the NPC's width in pixels.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria NPC instance.
		/// </summary>
		Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		void Kill();

		/// <summary>
		/// Sets the NPC's defaults using the specified <see cref="NpcType"/>.
		/// </summary>
		/// <param name="type">The <see cref="NpcType"/>.</param>
		void SetDefaults(NpcType type);
	}
}
