using System;
using Microsoft.Xna.Framework;

namespace Orion.Core
{
	/// <summary>
	/// Provides a wrapper around a Terraria player.
	/// </summary>
	public interface IPlayer
	{
		/// <summary>
		/// Gets the player's defense.
		/// </summary>
		int Defense { get; }

		/// <summary>
		/// Gets or sets the player's health.
		/// </summary>
		int Health { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum health.
		/// </summary>
		int MaxHealth { get; set; }

		/// <summary>
		/// Gets or sets the player's mana.
		/// </summary>
		int Mana { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum mana.
		/// </summary>
		int MaxMana { get; set; }

		/// <summary>
		/// Gets the player's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the player's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the player's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria player.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Gets the player's inventory <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The <see cref="IItem"/> at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem GetInventory(int index);

		/// <summary>
		/// Gets the player's selected <see cref="IItem"/>.
		/// </summary>
		/// <returns>The selected <see cref="IItem"/>.</returns>
		IItem GetSelectedItem();

		/// <summary>
		/// Sets the player's inventory <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to modify.</param>
		/// <param name="item">The <see cref="IItem"/> to set.</param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		void SetInventory(int index, IItem item);
	}
}
