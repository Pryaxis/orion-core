using System;
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
		/// Gets the player's defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets the player's dyes <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray Dyes { get; }

		/// <summary>
		/// Gets the player's equips <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray Equips { get; }

		/// <summary>
		/// Gets or sets the player's health.
		/// </summary>
		int Health { get; set; }

		/// <summary>
		/// Gets the player's inventory <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray Inventory { get; }

		/// <summary>
		/// Gets or sets the player's mana.
		/// </summary>
		int Mana { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum health.
		/// </summary>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum mana.
		/// </summary>
		int MaxMana { get; set; }

		/// <summary>
		/// Gets the player's miscellaneous dyes <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray MiscDyes { get; }

		/// <summary>
		/// Gets the player's miscellaneous equips <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray MiscEquips { get; }

		/// <summary>
		/// Gets the player's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the player's piggy bank <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray PiggyBank { get; }

		/// <summary>
		/// Gets or sets the player's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the player's safe <see cref="IItemArray"/> instance.
		/// </summary>
		IItemArray Safe { get; }

		/// <summary>
		/// Gets the player's selected <see cref="IItem"/> instance.
		/// </summary>
		IItem SelectedItem { get; }

		/// <summary>
		/// Gets or sets the player's trash <see cref="IItem"/> instance.
		/// </summary>
		IItem TrashItem { get; set; }

		/// <summary>
		/// Gets or sets the player's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria player instance.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }
	}
}
