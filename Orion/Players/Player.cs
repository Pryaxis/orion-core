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
		public int Defense
		{
			get { return WrappedPlayer.statDefense; }
			set { WrappedPlayer.statDefense = value; }
		}

		/// <inheritdoc/>
		public IItemArray Dyes { get; }

		/// <inheritdoc/>
		public IItemArray Equips { get; }

		/// <inheritdoc/>
		public bool HasPvpEnabled
		{
			get { return WrappedPlayer.hostile; }
			set { WrappedPlayer.hostile = value; }
		}

		/// <inheritdoc/>
		public int Health
		{
			get { return WrappedPlayer.statLife; }
			set { WrappedPlayer.statLife = value; }
		}

		/// <inheritdoc/>
		public int Height
		{
			get { return WrappedPlayer.height; }
			set { WrappedPlayer.height = value; }
		}

		/// <inheritdoc/>
		public bool IsDead
		{
			get { return WrappedPlayer.dead; }
			set { WrappedPlayer.dead = value; }
		}

		/// <inheritdoc/>
		public IItemArray Inventory { get; }

		/// <inheritdoc/>
		public int MagicCritBonus
		{
			get { return WrappedPlayer.magicCrit; }
			set { WrappedPlayer.magicCrit = value; }
		}

		/// <inheritdoc/>
		public float MagicDamageMultiplier
		{
			get { return WrappedPlayer.magicDamage; }
			set { WrappedPlayer.magicDamage = value; }
		}

		/// <inheritdoc/>
		public int Mana
		{
			get { return WrappedPlayer.statMana; }
			set { WrappedPlayer.statMana = value; }
		}

		/// <inheritdoc/>
		public float ManaCostMultiplier
		{
			get { return WrappedPlayer.manaCost; }
			set { WrappedPlayer.manaCost = value; }
		}

		/// <inheritdoc/>
		public int MaxHealth
		{
			get { return WrappedPlayer.statLifeMax; }
			set { WrappedPlayer.statLifeMax = value; }
		}

		/// <inheritdoc/>
		public int MaxMana
		{
			get { return WrappedPlayer.statManaMax; }
			set { WrappedPlayer.statManaMax = value; }
		}

		/// <inheritdoc/>
		public int MaxMinions
		{
			get { return WrappedPlayer.maxMinions; }
			set { WrappedPlayer.maxMinions = value; }
		}

		/// <inheritdoc/>
		public int MeleeCritBonus
		{
			get { return WrappedPlayer.meleeCrit; }
			set { WrappedPlayer.meleeCrit = value; }
		}

		/// <inheritdoc/>
		public float MeleeDamageMultiplier
		{
			get { return WrappedPlayer.meleeDamage; }
			set { WrappedPlayer.meleeDamage = value; }
		}

		/// <inheritdoc/>
		public float MinionDamageMultiplier
		{
			get { return WrappedPlayer.minionDamage; }
			set { WrappedPlayer.minionDamage = value; }
		}

		/// <inheritdoc/>
		public IItemArray MiscDyes { get; }

		/// <inheritdoc/>
		public IItemArray MiscEquips { get; }

		/// <inheritdoc/>
		public float MovementSpeed
		{
			get { return WrappedPlayer.moveSpeed; }
			set { WrappedPlayer.moveSpeed = value; }
		}

		/// <inheritdoc/>
		public string Name
		{
			get { return WrappedPlayer.name; }
			set { WrappedPlayer.name = value; }
		}

		/// <inheritdoc/>
		public IItemArray PiggyBank { get; }

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedPlayer.position; }
			set { WrappedPlayer.position = value; }
		}

		/// <inheritdoc/>
		public int RangedCritBonus
		{
			get { return WrappedPlayer.rangedCrit; }
			set { WrappedPlayer.rangedCrit = value; }
		}

		/// <inheritdoc/>
		public float RangedDamageMultiplier
		{
			get { return WrappedPlayer.rangedDamage; }
			set { WrappedPlayer.rangedDamage = value; }
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
		public Team Team
		{
			get { return (Team)WrappedPlayer.team; }
			set { WrappedPlayer.team = (int)value; }
		}

		/// <inheritdoc/>
		public int ThrownCritBonus
		{
			get { return WrappedPlayer.thrownCrit; }
			set { WrappedPlayer.thrownCrit = value; }
		}

		/// <inheritdoc/>
		public float ThrownDamageMultiplier
		{
			get { return WrappedPlayer.thrownDamage; }
			set { WrappedPlayer.thrownDamage = value; }
		}

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
				_trashItem = value;
				WrappedPlayer.trashItem = value?.WrappedItem;
			}
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedPlayer.velocity; }
			set { WrappedPlayer.velocity = value; }
		}

		/// <inheritdoc/>
		public int Width
		{
			get { return WrappedPlayer.width; }
			set { WrappedPlayer.width = value; }
		}

		/// <inheritdoc/>
		public Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class wrapping the specified Terraria player
		/// instance.
		/// </summary>
		/// <param name="terrariaPlayer">The Terraria player instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaPlayer"/> is null.</exception>
		public Player(Terraria.Player terrariaPlayer)
		{
			if (terrariaPlayer == null)
			{
				throw new ArgumentNullException(nameof(terrariaPlayer));
			}

			Dyes = new ItemArray(terrariaPlayer.dye);
			Equips = new ItemArray(terrariaPlayer.armor);
			Inventory = new ItemArray(terrariaPlayer.inventory);
			MiscDyes = new ItemArray(terrariaPlayer.miscDyes);
			MiscEquips = new ItemArray(terrariaPlayer.miscEquips);
			PiggyBank = new ItemArray(terrariaPlayer.bank.item);
			Safe = new ItemArray(terrariaPlayer.bank2.item);
			WrappedPlayer = terrariaPlayer;
		}
	}
}
