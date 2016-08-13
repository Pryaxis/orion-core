using Microsoft.Xna.Framework;
using Orion.Projectiles;

namespace Orion.Items
{
	/// <summary>
	/// Provides a wrapper around a Terraria item instance.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets or sets the item's ammo type.
		/// </summary>
		int AmmoType { get; set; }

		/// <summary>
		/// Gets or sets the item's <see cref="Items.AnimationStyle"/>.
		/// </summary>
		AnimationStyle AnimationStyle { get; set; }

		/// <summary>
		/// Gets or sets the item's animation time.
		/// </summary>
		int AnimationTime { get; set; }

		/// <summary>
		/// Gets or sets the item's axe power.
		/// </summary>
		int AxePower { get; set; }

		/// <summary>
		/// Gets or sets the item's bait power.
		/// </summary>
		int BaitPower { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item can automatically be reused.
		/// </summary>
		bool CanAutoReuse { get; set; }

		/// <summary>
		/// Gets or sets the item's color.
		/// </summary>
		Color Color { get; set; }

		/// <summary>
		/// Gets or sets the item's damage.
		/// </summary>
		int Damage { get; set; }

		/// <summary>
		/// Gets or sets the item's fishing power.
		/// </summary>
		int FishingPower { get; set; }

		/// <summary>
		/// Gets or sets the item's graphical scale.
		/// </summary>
		float GraphicalScale { get; set; }

		/// <summary>
		/// Gets or sets the item's hammer power.
		/// </summary>
		int HammerPower { get; set; }

		/// <summary>
		/// Gets or sets the item's height.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item is an accessory.
		/// </summary>
		bool IsAccessory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item is a magic weapon.
		/// </summary>
		bool IsMagicWeapon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item is a melee weapon.
		/// </summary>
		bool IsMeleeWeapon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item is a ranged weapon.
		/// </summary>
		bool IsRangedWeapon { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the item is a thrown weapon.
		/// </summary>
		bool IsThrownWeapon { get; set; }

		/// <summary>
		/// Gets or sets the item's knockback.
		/// </summary>
		float Knockback { get; set; }

		/// <summary>
		/// Gets or sets the item's mana cost.
		/// </summary>
		int ManaCost { get; set; }

		/// <summary>
		/// Gets or sets the item's maximum stack size.
		/// </summary>
		int MaxStackSize { get; set; }

		/// <summary>
		/// Gets or sets the item's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the item's pickaxe power.
		/// </summary>
		int PickaxePower { get; set; }

		/// <summary>
		/// Gets or sets the item's position in the world.
		/// </summary>
		Vector2 Position { get; set; }

		/// <summary>
		/// Gets the item's <see cref="Items.Prefix"/>.
		/// </summary>
		Prefix Prefix { get; }

		/// <summary>
		/// Gets or sets the item's projectile speed.
		/// </summary>
		float ProjectileSpeed { get; set; }

		/// <summary>
		/// Gets or sets the item's <see cref="Projectiles.ProjectileType"/>.
		/// </summary>
		ProjectileType ProjectileType { get; set; }

		/// <summary>
		/// Gets or sets the item's <see cref="Items.Rarity"/>.
		/// </summary>
		Rarity Rarity { get; set; }

		/// <summary>
		/// Gets or sets the item's stack size.
		/// </summary>
		int StackSize { get; set; }

		/// <summary>
		/// Gets the item's <see cref="ItemType"/>.
		/// </summary>
		ItemType Type { get; }

		/// <summary>
		/// Gets or sets the item's used ammo type.
		/// </summary>
		int UseAmmoType { get; set; }

		/// <summary>
		/// Gets or sets the item's use time.
		/// </summary>
		int UseTime { get; set; }

		/// <summary>
		/// Gets or sets the item's velocity in the world.
		/// </summary>
		Vector2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the item's width.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// Gets the wrapped Terraria item instance.
		/// </summary>
		Terraria.Item WrappedItem { get; }

		/// <summary>
		/// Sets the item's defaults using an <see cref="ItemType"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		void SetDefaults(ItemType type);

		/// <summary>
		/// Tries to set the item's prefix using an <see cref="Items.Prefix"/>.
		/// </summary>
		/// <param name="prefix">The <see cref="Items.Prefix"/>.</param>
		void SetPrefix(Prefix prefix);
	}
}
