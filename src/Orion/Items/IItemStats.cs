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
    /// Represents an item's stats. These are the immutable aspects of an item.
    /// </summary>
    public interface IItemStats {
        /// <summary>
        /// Gets the item's maximum stack size.
        /// </summary>
        /// <value>The item's maximum stack size.</value>
        /// <remarks>
        /// For most weapons and gear, this value is 1. For most building materials, this value is 999.
        /// </remarks>
        int MaxStackSize { get; }

        /// <summary>
        /// Gets the item's rarity.
        /// </summary>
        /// <value>The item's rarity.</value>
        /// <remarks>
        /// The item's rarity can be modified depending on the item's prefix, meaning this property's value is not
        /// necessarily constant with respect to item type.
        /// </remarks>
        ItemRarity Rarity { get; }
    }
}
