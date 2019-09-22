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
using Microsoft.Xna.Framework;

namespace Orion.Entities.Extensions {
    /// <summary>
    /// Provides extensions for the <see cref="ItemRarity"/> enumeration.
    /// </summary>
    public static class ItemRarityExtensions {
        private static readonly IDictionary<ItemRarity, Color> Colors = new Dictionary<ItemRarity, Color> {
            [ItemRarity.Amber] = new Color(255, 175, 0),
            [ItemRarity.Gray] = new Color(130, 130, 130),
            [ItemRarity.Blue] = new Color(150, 150, 255),
            [ItemRarity.Green] = new Color(150, 255, 150),
            [ItemRarity.Orange] = new Color(255, 200, 150),
            [ItemRarity.LightRed] = new Color(255, 150, 150),
            [ItemRarity.Pink] = new Color(255, 150, 255),
            [ItemRarity.LightPurple] = new Color(210, 160, 255),
            [ItemRarity.Lime] = new Color(150, 255, 10),
            [ItemRarity.Yellow] = new Color(255, 255, 10),
            [ItemRarity.Cyan] = new Color(5, 200, 255),
            [ItemRarity.Red] = new Color(255, 40, 100),
            [ItemRarity.Purple] = new Color(180, 40, 255)
        };

        /// <summary>
        /// Gets the item rarity's corresponding color.
        /// </summary>
        /// <param name="itemRarity">The item rarity.</param>
        /// <returns>The corresponding color.</returns>
        public static Color GetColor(this ItemRarity itemRarity) =>
            Colors.TryGetValue(itemRarity, out var color) ? color : Color.White;
    }
}
