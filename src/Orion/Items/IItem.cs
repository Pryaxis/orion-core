// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Projectiles;
using Orion.World.Tiles;

namespace Orion.Items {
    /// <summary>
    /// Represents a Terraria item.
    /// </summary>
    public interface IItem : IEntity {
        /// <summary>
        /// Gets the item's <see cref="ItemType"/>.
        /// </summary>
        ItemType Type { get; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item's <see cref="ItemPrefix"/>.
        /// </summary>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets or sets the maximum item stack size.
        /// </summary>
        int MaxStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="ItemRarity"/>.
        /// </summary>
        ItemRarity Rarity { get; set; }

        /// <summary>
        /// Gets or sets the item's color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Gets or sets the item's scale.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets or sets the item's value.
        /// </summary>
        /// <remarks>The item's value is used to determine its buy and sell prices.</remarks>
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
        /// Gets or sets the item's damage.
        /// </summary>
        int Damage { get; set; }

        /// <summary>
        /// Gets or sets the item's knockback.
        /// </summary>
        float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the item's use time.
        /// </summary>
        int UseTime { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Items.AmmoType"/> the item uses.
        /// </summary>
        AmmoType UsesAmmoType { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile speed.
        /// </summary>
        float ProjectileSpeed { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Items.AmmoType"/>.
        /// </summary>
        AmmoType AmmoType { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="Projectiles.ProjectileType"/>.
        /// </summary>
        /// <remarks>
        /// This can either be the <see cref="Projectiles.ProjectileType"/> that the item shoots, or the
        /// <see cref="Projectiles.ProjectileType"/> that the item creates when shot from something.
        /// </remarks>
        ProjectileType ProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the item's <see cref="World.Tiles.BlockType"/>. A value of <c>null</c> indicates that the item
        /// does not create any blocks.
        /// </summary>
        BlockType? BlockType { get; set; }

        /// <summary>
        /// Gets or sets the item's wall type. A value of <c>null</c> indicates that the item does not create any walls.
        /// </summary>
        WallType? WallType { get; set; }

        /// <summary>
        /// Gets or sets the item's pickaxe power.
        /// </summary>
        int PickaxePower { get; set; }

        /// <summary>
        /// Gets or sets the item's axe power.
        /// </summary>
        int AxePower { get; set; }

        /// <summary>
        /// Gets or sets the item's hammer power.
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
        /// Applies the given <see cref="ItemType"/> to the item. This will update all of the properties accordingly.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/>.</param>
        void ApplyType(ItemType type);

        /// <summary>
        /// Applies the given <see cref="ItemPrefix"/> to the item. This will update all of the properties accordingly.
        /// </summary>
        /// <param name="prefix">The <see cref="ItemPrefix"/>.</param>
        void ApplyPrefix(ItemPrefix prefix);
    }
}
