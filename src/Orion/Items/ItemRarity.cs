// Copyright (c) 2019 Pryaxis & Orion Contributors
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
    /// <remarks>
    /// The item rarity roughly specifies how valuable an item is. The item's prefix can modify its rarity by up to two
    /// levels in either direction.
    /// </remarks>
    public enum ItemRarity {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Rainbow = -12,
        Amber = -11,
        Gray = -1,
        White = 0,
        Blue = 1,
        Green = 2,
        Orange = 3,
        LightRed = 4,
        Pink = 5,
        LightPurple = 6,
        Lime = 7,
        Yellow = 8,
        Cyan = 9,
        Red = 10,
        Purple = 11
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
