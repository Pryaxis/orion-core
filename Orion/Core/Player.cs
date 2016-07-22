using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria player.
	/// </summary>
	public class Player : IPlayer
	{
		private readonly IItem[] _inventory;

		/// <summary>
		/// Gets the player's defense.
		/// </summary>
		public int Defense => WrappedPlayer.statDefense;

		/// <summary>
		/// Gets or sets the player's HP.
		/// </summary>
		public int HP
		{
			get { return WrappedPlayer.statLife; }
			set { WrappedPlayer.statLife = value; }
		}

		/// <summary>
		/// Gets or sets the player's maximum HP.
		/// </summary>
		public int MaxHP
		{
			get { return WrappedPlayer.statLifeMax; }
			set { WrappedPlayer.statLifeMax = value; }
		}

		/// <summary>
		/// Gets or sets the player's maximum MP.
		/// </summary>
		public int MaxMP
		{
			get { return WrappedPlayer.statManaMax; }
			set { WrappedPlayer.statManaMax = value; }
		}

		/// <summary>
		/// Gets or sets the player's MP.
		/// </summary>
		public int MP
		{
			get { return WrappedPlayer.statMana; }
			set { WrappedPlayer.statMana = value; }
		}

		/// <summary>
		/// Gets the player's name.
		/// </summary>
		public string Name => WrappedPlayer.name;

		/// <summary>
		/// Gets or sets the player's position in the world.
		/// </summary>
		public Vector2 Position
		{
			get { return WrappedPlayer.position; }
			set { WrappedPlayer.position = value; }
		}

		/// <summary>
		/// Gets or sets the player's velocity.
		/// </summary>
		public Vector2 Velocity
		{
			get { return WrappedPlayer.velocity; }
			set { WrappedPlayer.velocity = value; }
		}

		/// <summary>
		/// Gets the wrapped Terraria player.
		/// </summary>
		public Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class wrapping the specified Terraria player.
		/// </summary>
		/// <param name="terrariaPlayer">The Terraria player to wrap.</param>
		public Player(Terraria.Player terrariaPlayer)
		{
			_inventory = new IItem[terrariaPlayer.inventory.Length];
			WrappedPlayer = terrariaPlayer;
		}

		/// <summary>
		/// Gets the player's inventory <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to retrieve.</param>
		/// <returns>The <see cref="IItem"/> at the specified index.</returns>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		public IItem GetInventory(int index)
		{
			if (_inventory[index]?.WrappedItem != WrappedPlayer.inventory[index])
			{
				_inventory[index] = new Item(WrappedPlayer.inventory[index]);
			}
			return _inventory[index];
		}

		/// <summary>
		/// Gets the player's selected <see cref="IItem"/>.
		/// </summary>
		/// <returns>The selected <see cref="IItem"/>.</returns>
		public IItem GetSelectedItem() => GetInventory(WrappedPlayer.selectedItem);

		/// <summary>
		/// Sets the player's inventory <see cref="IItem"/> at the specified index.
		/// </summary>
		/// <param name="index">The index to modify.</param>
		/// <param name="item">The <see cref="IItem"/> to set.</param>
		/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> was out of range.</exception>
		public void SetInventory(int index, IItem item)
		{
			_inventory[index] = item;
			WrappedPlayer.inventory[index] = item.WrappedItem;
		}
	}
}
