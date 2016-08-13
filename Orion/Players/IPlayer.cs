using Microsoft.Xna.Framework;
using Orion.Items;

namespace Orion.Players
{
	/// <summary>
	/// Provides a wrapper around a Terraria player instance.
	/// </summary>
	public interface IPlayer
	{
		/// <summary>
		/// Gets or sets the player's defense.
		/// </summary>
		int Defense { get; set; }

		/// <summary>
		/// Gets the player's dyes.
		/// </summary>
		/// <remarks>
		/// This array corresponds to the <see cref="Equips"/> array.
		/// </remarks>
		IItemArray Dyes { get; }

		/// <summary>
		/// Gets the player's equips.
		/// </summary>
		IItemArray Equips { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the player has PvP enabled.
		/// </summary>
		bool HasPvpEnabled { get; set; }

		/// <summary>
		/// Gets or sets the player's health.
		/// </summary>
		int Health { get; set; }

		/// <summary>
		/// Gets or sets the player's height in pixels.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Gets the player's inventory.
		/// </summary>
		IItemArray Inventory { get; }

		/// <summary>
		/// Gets or sets the player's magic crit bonus.
		/// </summary>
		int MagicCritBonus { get; set; }

		/// <summary>
		/// Gets or sets the player's magic damage multiplier.
		/// </summary>
		float MagicDamageMultiplier { get; set; }

		/// <summary>
		/// Gets or sets the player's mana.
		/// </summary>
		int Mana { get; set; }

		/// <summary>
		/// Gets or sets the player's mana cost multiplier.
		/// </summary>
		float ManaCostMultiplier { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum health.
		/// </summary>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum mana.
		/// </summary>
		int MaxMana { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum number of minions.
		/// </summary>
		int MaxMinions { get; set; }

		/// <summary>
		/// Gets or sets the player's melee crit bonus.
		/// </summary>
		int MeleeCritBonus { get; set; }

		/// <summary>
		/// Gets or sets the player's melee damage multiplier.
		/// </summary>
		float MeleeDamageMultiplier { get; set; }

		/// <summary>
		/// Gets or sets the player's minion damage multiplier.
		/// </summary>
		float MinionDamageMultiplier { get; set; }

		/// <summary>
		/// Gets the player's miscellaneous dyes.
		/// </summary>
		/// <remarks>
		/// This array corresponds to the <see cref="MiscEquips"/> array.
		/// </remarks>
		IItemArray MiscDyes { get; }

		/// <summary>
		/// Gets the player's miscellaneous equips.
		/// </summary>
		IItemArray MiscEquips { get; }

		/// <summary>
		/// Gets or sets the player's movement speed.
		/// </summary>
		float MovementSpeed { get; set; }

		/// <summary>
		/// Gets or sets the player's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets the player's piggy bank.
		/// </summary>
		IItemArray PiggyBank { get; }

		/// <summary>
		/// Gets or sets the player's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the player's ranged crit bonus.
		/// </summary>
		int RangedCritBonus { get; set; }

		/// <summary>
		/// Gets or sets the player's ranged damage multiplier.
		/// </summary>
		float RangedDamageMultiplier { get; set; }

		/// <summary>
		/// Gets the player's safe.
		/// </summary>
		IItemArray Safe { get; }

		/// <summary>
		/// Gets the player's selected item.
		/// </summary>
		IItem SelectedItem { get; }

		/// <summary>
		/// Gets or sets the player's thrown crit bonus.
		/// </summary>
		int ThrownCritBonus { get; set; }

		/// <summary>
		/// Gets or sets the player's thrown damage multiplier.
		/// </summary>
		float ThrownDamageMultiplier { get; set; }

		/// <summary>
		/// Gets or sets the player's trash item.
		/// </summary>
		IItem TrashItem { get; set; }

		/// <summary>
		/// Gets or sets the player's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the player's width in pixels.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria player instance.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }
	}
}
