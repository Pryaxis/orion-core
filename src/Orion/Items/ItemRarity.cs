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

namespace Orion.Items {
    /// <summary>
    /// Specifies an item rarity.
    /// </summary>
    public enum ItemRarity {
        /// <summary>
        /// Indicates rainbow rarity. This is used for expert-mode items.
        /// </summary>
        Rainbow = -12,

        /// <summary>
        /// Indicates amber rarity. This is used for quest items.
        /// </summary>
        Amber = -11,

        /// <summary>
        /// Indicates gray rarity.
        /// </summary>
        Gray = -1,

        /// <summary>
        /// Indicates white rarity. This is the default rarity.
        /// </summary>
        White = 0,

        /// <summary>
        /// Indicates blue rarity.
        /// </summary>
        Blue = 1,

        /// <summary>
        /// Indicates green rarity.
        /// </summary>
        Green = 2,

        /// <summary>
        /// Indicates orange rarity.
        /// </summary>
        Orange = 3,

        /// <summary>
        /// Indicates light red rarity.
        /// </summary>
        LightRed = 4,

        /// <summary>
        /// Indicates pink rarity.
        /// </summary>
        Pink = 5,

        /// <summary>
        /// Indicates light purple rarity.
        /// </summary>
        LightPurple = 6,

        /// <summary>
        /// Indicates lime rarity.
        /// </summary>
        Lime = 7,

        /// <summary>
        /// Indicates yellow rarity.
        /// </summary>
        Yellow = 8,

        /// <summary>
        /// Indicates yellow rarity.
        /// </summary>
        Cyan = 9,

        /// <summary>
        /// Indicates red rarity.
        /// </summary>
        Red = 10,

        /// <summary>
        /// Indicates purple rarity. This is the highest rarity.
        /// </summary>
        Purple = 11
    }
}
