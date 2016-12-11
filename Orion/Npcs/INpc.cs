using Orion.Entities;

namespace Orion.Npcs
{
	/// <summary>
	/// Provides a wrapper around a Terraria NPC instance.
	/// </summary>
	public interface INpc : IOrionEntity
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
		/// Gets or sets a value indicating whether the NPC is a boss.
		/// </summary>
		bool IsBoss { get; set; }

		/// <summary>
		/// Gets or sets the NPC's maximum health.
		/// </summary>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets the NPC's <see cref="NpcType"/>.
		/// </summary>
		NpcType Type { get; }

		/// <summary>
		/// Gets the wrapped Terraria NPC instance.
		/// </summary>
		Terraria.NPC WrappedNpc { get; }

		/// <summary>
		/// Sets the NPC's defaults using the specified <see cref="NpcType"/>.
		/// </summary>
		/// <param name="type">The <see cref="NpcType"/>.</param>
		void SetDefaults(NpcType type);
	}
}
