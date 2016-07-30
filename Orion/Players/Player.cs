using System;
using Microsoft.Xna.Framework;
using Orion.Items;

namespace Orion.Players
{
	/// <summary>
	/// Wraps a Terraria player instance.
	/// </summary>
	public class Player : IPlayer
	{
		private IItem _trashItem;

		/// <inheritdoc/>
		public int Defense => WrappedPlayer.statDefense;

		/// <inheritdoc/>
		public IItemArray Dyes { get; }

		/// <inheritdoc/>
		public IItemArray Equips { get; }

		/// <inheritdoc/>
		public int Health
		{
			get { return WrappedPlayer.statLife; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedPlayer.statLife = value;
			}
		}

		/// <inheritdoc/>
		public IItemArray Inventory { get; }

		/// <inheritdoc/>
		public int Mana
		{
			get { return WrappedPlayer.statMana; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedPlayer.statMana = value;
			}
		}

		/// <inheritdoc/>
		public int MaxHealth
		{
			get { return WrappedPlayer.statLifeMax; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedPlayer.statLifeMax = value;
			}
		}

		/// <inheritdoc/>
		public int MaxMana
		{
			get { return WrappedPlayer.statManaMax; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedPlayer.statManaMax = value;
			}
		}

		/// <inheritdoc/>
		public IItemArray MiscDyes { get; }

		/// <inheritdoc/>
		public IItemArray MiscEquips { get; }

		/// <inheritdoc/>
		public string Name => WrappedPlayer.name;

		/// <inheritdoc/>
		public IItemArray PiggyBank { get; }

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedPlayer.position; }
			set { WrappedPlayer.position = value; }
		}

		/// <inheritdoc/>
		public IItemArray Safe { get; }

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IItem"/> instance is cached. Calling this getter multiple times will return the same
		/// <see cref="IItem"/> instance as long as the player's selected item remains unchanged.
		/// </remarks>
		public IItem SelectedItem => Inventory[WrappedPlayer.selectedItem];

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IItem"/> instance is cached. Calling this getter multiple times will return the same
		/// <see cref="IItem"/> instance as long as the player's trash item remains unchanged.
		/// </remarks>
		public IItem TrashItem
		{
			get
			{
				if (_trashItem?.WrappedItem != WrappedPlayer.trashItem)
				{
					_trashItem = new Item(WrappedPlayer.trashItem);
				}
				return _trashItem;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(nameof(value));
				}

				_trashItem = value;
				WrappedPlayer.trashItem = value.WrappedItem;
			}
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedPlayer.velocity; }
			set { WrappedPlayer.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class wrapping the specified Terraria player
		/// instance.
		/// </summary>
		/// <param name="player">The Terraria player instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="player"/> was null.</exception>
		public Player(Terraria.Player player)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player));
			}

			Dyes = new ItemArray(player.dye);
			Equips = new ItemArray(player.armor);
			Inventory = new ItemArray(player.inventory);
			MiscDyes = new ItemArray(player.miscDyes);
			MiscEquips = new ItemArray(player.miscEquips);
			PiggyBank = new ItemArray(player.bank.item);
			Safe = new ItemArray(player.bank2.item);
			WrappedPlayer = player;
		}
	}
}
