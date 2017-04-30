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
		public AnimationStyle AnimationStyle
		{
			get { return (AnimationStyle)WrappedItem.useStyle; }
			set { WrappedItem.useStyle = (int)value; }
		}

		/// <inheritdoc/>
		public int AnimationTime
		{
			get { return WrappedItem.useAnimation; }
			set { WrappedItem.useAnimation = value; }
		}

		/// <inheritdoc/>
		public int AxePower
		{
			get { return WrappedItem.axe; }
			set { WrappedItem.axe = value; }
		}

		/// <inheritdoc/>
		public int BaitPower
		{
			get { return WrappedItem.bait; }
			set { WrappedItem.bait = value; }
		}

		/// <inheritdoc/>
		public bool CanAutoReuse
		{
			get { return WrappedItem.autoReuse; }
			set { WrappedItem.autoReuse = value; }
		}

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
			set { WrappedItem.damage = value; }
		}

		/// <inheritdoc/>
		public int FishingPower
		{
			get { return WrappedItem.fishingPole; }
			set { WrappedItem.fishingPole = value; }
		}

		/// <inheritdoc/>
		public float GraphicalScale
		{
			get { return WrappedItem.scale; }
			set { WrappedItem.scale = value; }
		}

		/// <inheritdoc/>
		public int HammerPower
		{
			get { return WrappedItem.hammer; }
			set { WrappedItem.hammer = value; }
		}

		/// <inheritdoc/>
		public int Height
		{
			get { return WrappedItem.height; }
			set { WrappedItem.height = value; }
		}

		/// <inheritdoc/>
		public bool IsAccessory
		{
			get { return WrappedItem.accessory; }
			set { WrappedItem.accessory = value; }
		}

		/// <inheritdoc/>
		public bool IsMagicWeapon
		{
			get { return WrappedItem.magic; }
			set { WrappedItem.magic = value; }
		}

		/// <inheritdoc/>
		public bool IsMeleeWeapon
		{
			get { return WrappedItem.melee; }
			set { WrappedItem.melee = value; }
		}

		/// <inheritdoc/>
		public bool IsRangedWeapon
		{
			get { return WrappedItem.ranged; }
			set { WrappedItem.ranged = value; }
		}

		/// <inheritdoc/>
		public bool IsThrownWeapon
		{
			get { return WrappedItem.thrown; }
			set { WrappedItem.thrown = value; }
		}

		/// <inheritdoc/>
		public float Knockback
		{
			get { return WrappedItem.knockBack; }
			set { WrappedItem.knockBack = value; }
		}

		/// <inheritdoc/>
		public int ManaCost
		{
			get { return WrappedItem.mana; }
			set { WrappedItem.mana = value; }
		}

		/// <inheritdoc/>
		public int MaxStackSize
		{
			get { return WrappedItem.maxStack; }
			set { WrappedItem.maxStack = value; }
		}
		
		/// <inheritdoc/>
		public string Name
		{
			get { return WrappedItem.Name; }
			set { WrappedItem.SetNameOverride(value); }
		}

		/// <inheritdoc/>
		public int PickaxePower
		{
			get { return WrappedItem.pick; }
			set { WrappedItem.pick = value; }
		}

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedItem.position; }
			set { WrappedItem.position = value; }
		}

		/// <inheritdoc/>
		public Prefix Prefix => (Prefix)WrappedItem.prefix;

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
		public Rarity Rarity
		{
			get { return (Rarity)WrappedItem.rare; }
			set { WrappedItem.rare = (int)value; }
		}

		/// <inheritdoc/>
		public int StackSize
		{
			get { return WrappedItem.stack; }
			set { WrappedItem.stack = value; }
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
		public int UseTime
		{
			get { return WrappedItem.useTime; }
			set { WrappedItem.useTime = value; }
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
			set { WrappedItem.width = value; }
		}

		/// <inheritdoc/>
		public Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Item"/> class wrapping the specified Terraria item instance.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item instance to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaItem"/> is null.</exception>
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
		public void SetPrefix(Prefix prefix) => WrappedItem.Prefix((int)prefix);
	}
}
