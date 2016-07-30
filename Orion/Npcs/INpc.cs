using System;
using Microsoft.Xna.Framework;

namespace Orion.Npcs
{
	/// <summary>
	/// Provides a wrapper around a Terraria NPC instance.
	/// </summary>
	public interface INpc
	{
		/// <summary>
		/// Gets the NPC's damage.
		/// </summary>
		int Damage { get; }

		/// <summary>
		/// Gets the NPC's defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets or sets the NPC's health.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> was negative.</exception>
		int Health { get; set; }

		/// <summary>
		/// Gets or sets the NPC's maximum health.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> was negative.</exception>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets the NPC's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the NPC's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the NPC's <see cref="NpcType"/> instance.
		/// </summary>
		NpcType Type { get; }

		/// <summary>
		/// Gets or sets the NPC's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria NPC instance.
		/// </summary>
		Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Kills the NPC.
		/// </summary>
		void Kill();

		/// <summary>
		/// Sets the NPC's defaults using a <see cref="NpcType"/> instance.
		/// </summary>
		/// <param name="type">The <see cref="NpcType"/> instance.</param>
		void SetDefaults(NpcType type);
	}
}
