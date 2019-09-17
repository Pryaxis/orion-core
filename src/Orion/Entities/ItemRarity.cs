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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Orion.Entities {
    /// <summary>
    /// Specifies an item rarity.
    /// </summary>
    public sealed class ItemRarity {
#pragma warning disable 1591
        public static readonly ItemRarity Rainbow = new ItemRarity(-12);
        public static readonly ItemRarity Amber = new ItemRarity(-11);
        public static readonly ItemRarity Gray = new ItemRarity(-1);
        public static readonly ItemRarity White = new ItemRarity(0);
        public static readonly ItemRarity Blue = new ItemRarity(1);
        public static readonly ItemRarity Green = new ItemRarity(2);
        public static readonly ItemRarity Orange = new ItemRarity(3);
        public static readonly ItemRarity LightRed = new ItemRarity(4);
        public static readonly ItemRarity Pink = new ItemRarity(5);
        public static readonly ItemRarity LightPurple = new ItemRarity(6);
        public static readonly ItemRarity Lime = new ItemRarity(7);
        public static readonly ItemRarity Yellow = new ItemRarity(8);
        public static readonly ItemRarity Cyan = new ItemRarity(9);
        public static readonly ItemRarity Red = new ItemRarity(10);
        public static readonly ItemRarity Purple = new ItemRarity(11);
#pragma warning restore 1591

        private static readonly IDictionary<int, FieldInfo> LevelToField = new Dictionary<int, FieldInfo>();
        private static readonly IDictionary<int, ItemRarity> LevelToItemRarity = new Dictionary<int, ItemRarity>();

        private static readonly IDictionary<ItemRarity, Color> Colors = new Dictionary<ItemRarity, Color> {
            [Amber] = new Color(255, 175, 0),
            [Gray] = new Color(130, 130, 130),
            [Blue] = new Color(150, 150, 255),
            [Green] = new Color(150, 255, 150),
            [Orange] = new Color(255, 200, 150),
            [LightRed] = new Color(255, 150, 150),
            [Pink] = new Color(255, 150, 255),
            [LightPurple] = new Color(210, 160, 255),
            [Lime] = new Color(150, 255, 10),
            [Yellow] = new Color(255, 255, 10),
            [Cyan] = new Color(5, 200, 255),
            [Red] = new Color(255, 40, 100),
            [Purple] = new Color(180, 40, 255)
        };

        /// <summary>
        /// Gets the item rarity's level.
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Gets the item rarity's color.
        /// </summary>
        public Color Color => Colors.TryGetValue(this, out var color) ? color : Color.White;

        // Initializes lookup tables.
        static ItemRarity() {
            var fields = typeof(ItemRarity).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields) {
                if (!(field.GetValue(null) is ItemRarity itemRarity)) continue;

                LevelToField[itemRarity.Level] = field;
                LevelToItemRarity[itemRarity.Level] = itemRarity;
            }
        }

        private ItemRarity(int level) {
            Level = level;
        }

        /// <summary>
        /// Returns an item rarity converted from the given level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>The item rarity, or <c>null</c> if none exists.</returns>
        public static ItemRarity FromLevel(int level) =>
            LevelToItemRarity.TryGetValue(level, out var itemRarity) ? itemRarity : null;

        /// <inheritdoc />
        public override string ToString() => LevelToField[Level].Name;
    }
}
