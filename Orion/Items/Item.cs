using System;
using Microsoft.Xna.Framework;
using Orion.Projectiles;

namespace Orion.Items
{
	/// <summary>
	/// Wraps a Terraria item instance.
	/// </summary>
	public class Item : IItem
	{
		/// <inheritdoc/>
		public int AmmoType
		{
			get { return WrappedItem.ammo; }
			set { WrappedItem.ammo = value; }
		}

		/// <inheritdoc/>
		public int AxePower => WrappedItem.axe;

		/// <inheritdoc/>
		public Color Color
		{
			get { return WrappedItem.color; }
			set { WrappedItem.color = value; }
		}

		/// <inheritdoc/>
		public int Damage
		{
			get { return WrappedItem.damage; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.damage = value;
			}
		}

		/// <inheritdoc/>
		public int HammerPower => WrappedItem.hammer;

		/// <inheritdoc/>
		public int Height
		{
			get { return WrappedItem.height; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.height = value;
			}
		}

		/// <inheritdoc/>
		public float Knockback
		{
			get { return WrappedItem.knockBack; }
			set { WrappedItem.knockBack = value; }
		}

		/// <inheritdoc/>
		public int MaxStackSize => WrappedItem.maxStack;

		/// <inheritdoc/>
		public string Name => WrappedItem.name;

		/// <inheritdoc/>
		public int PickaxePower => WrappedItem.pick;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedItem.position; }
			set { WrappedItem.position = value; }
		}

		/// <inheritdoc/>
		public ItemPrefix Prefix => (ItemPrefix)WrappedItem.prefix;

		/// <inheritdoc/>
		public float ProjectileSpeed
		{
			get { return WrappedItem.shootSpeed; }
			set { WrappedItem.shootSpeed = value; }
		}

		/// <inheritdoc/>
		public ProjectileType ProjectileType
		{
			get { return (ProjectileType)WrappedItem.shoot; }
			set { WrappedItem.shoot = (int)value; }
		}

		/// <inheritdoc/>
		public float Scale
		{
			get { return WrappedItem.scale; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.scale = value;
			}
		}

		/// <inheritdoc/>
		public int StackSize
		{
			get { return WrappedItem.stack; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.stack = value;
			}
		}

		/// <inheritdoc/>
		public ItemType Type => (ItemType)WrappedItem.netID;

		/// <inheritdoc/>
		public int UseAmmoType
		{
			get { return WrappedItem.useAmmo; }
			set { WrappedItem.useAmmo = value; }
		}

		/// <inheritdoc/>
		public int UseAnimationTime
		{
			get { return WrappedItem.useAnimation; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.useAnimation = value;
			}
		}

		/// <inheritdoc/>
		public int UseTime
		{
			get { return WrappedItem.useTime; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.useTime = value;
			}
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedItem.velocity; }
			set { WrappedItem.velocity = value; }
		}

		/// <inheritdoc/>
		public int Width
		{
			get { return WrappedItem.width; }
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative.");
				}

				WrappedItem.width = value;
			}
		}

		/// <inheritdoc/>
		public Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class wrapping the specified Terraria item instance.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItem"/> was null.</exception>
		public Item(Terraria.Item terrariaItem)
		{
			if (terrariaItem == null)
			{
				throw new ArgumentNullException(nameof(terrariaItem));
			}

			WrappedItem = terrariaItem;
		}

		/// <inheritdoc/>
		public void SetDefaults(ItemType type) => WrappedItem.SetDefaults((int)type);

		/// <inheritdoc/>
		/// <remarks>
		/// If <paramref name="prefix"/> is not applicable to the item type, then a randomly chosen prefix will be
		/// used.
		/// </remarks>
		public void SetPrefix(ItemPrefix prefix) => WrappedItem.Prefix((int)prefix);
	}
}
