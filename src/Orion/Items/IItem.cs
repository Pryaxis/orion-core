using Orion.Entities;
using Orion.Projectiles;
using Orion.World.Tiles;

namespace Orion.Items {
    /// <summary>
    /// Provides a wrapper around a Terraria.Item instance that may or may not exist within the world.
    /// </summary>
    public interface IItem : IEntity {
        /// <summary>
        /// Gets the item type.
        /// </summary>
        ItemType Type { get; }

        /// <summary>
        /// Gets or sets the item stack size.
        /// </summary>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item prefix.
        /// </summary>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets or sets the maximum item stack size.
        /// </summary>
        int MaxStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item rarity.
        /// </summary>
        ItemRarity Rarity { get; set; }

        /// <summary>
        /// Gets or sets the item's scale.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets or sets the item value.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a melee weapon.
        /// </summary>
        bool IsMeleeWeapon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a ranged weapon.
        /// </summary>
        bool IsRangedWeapon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a magic weapon.
        /// </summary>
        bool IsMagicWeapon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a summon weapon.
        /// </summary>
        bool IsSummonWeapon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is a thrown weapon.
        /// </summary>
        bool IsThrownWeapon { get; set; }

        /// <summary>
        /// Gets or sets the item damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the item's use time.
        /// </summary>
        int UseTime { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile speed.
        /// </summary>
        float ProjectileSpeed { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile type.
        /// </summary>
        ProjectileType ProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the item's block type.
        /// </summary>
        BlockType? BlockType { get; set; }

        /// <summary>
        /// Gets or sets the item's wall type.
        /// </summary>
        WallType? WallType { get; set; }

        /// <summary>
        /// Gets or sets the item's pickaxe power.
        /// </summary>
        int PickaxePower { get; set; }

        /// <summary>
        /// Gets or sets the item's pickaxe power.
        /// </summary>
        int AxePower { get; set; }

        /// <summary>
        /// Gets or sets the item's pickaxe power.
        /// </summary>
        int HammerPower { get; set; }

        /// <summary>
        /// Gets or sets the item's defense.
        /// </summary>
        int Defense { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is an accessory.
        /// </summary>
        bool IsAccessory { get; set; }

        /// <summary>
        /// Applies the given <see cref="ItemType"/> to the item.
        /// </summary>
        /// <param name="type">The type.</param>
        void ApplyType(ItemType type);

        /// <summary>
        /// Applies the given prefix to the item.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        void ApplyPrefix(ItemPrefix prefix);
    }
}
