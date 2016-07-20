using System;
using Microsoft.Xna.Framework;

namespace Orion.Interfaces
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
		/// Gets or sets the player's HP.
		/// </summary>
		int HP { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum HP.
		/// </summary>
		int MaxHP { get; set; }

		/// <summary>
		/// Gets or sets the player's maximum MP.
		/// </summary>
		int MaxMP { get; set; }

		/// <summary>
		/// Gets or sets the player's MP.
		/// </summary>
		int MP { get; set; }

		/// <summary>
		/// Gets the player's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets the player's position.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets or sets the player's velocity.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria player.
		/// </summary>
		Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Gets the player's inventory item at the specified index.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The item at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		IItem GetInventory(int index);

		/// <summary>
		/// Gets the player's selected item.
		/// </summary>
		/// <returns>The selected item.</returns>
		IItem GetSelectedItem();

		/// <summary>
		/// Sets the player's inventory item at the specified index.
		/// </summary>
		/// <param name="index">The index to modify.</param>
		/// <param name="item">The item to set.</param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		void SetInventory(int index, IItem item);
	}
}
