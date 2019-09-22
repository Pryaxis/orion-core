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

using JetBrains.Annotations;

namespace Orion.Entities {
    /// <summary>
    /// Represents a Terraria item.
    /// </summary>
    [PublicAPI]
    public interface IItem : IEntity {
        /// <summary>
        /// Gets the item's type.
        /// </summary>
        ItemType Type { get; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item's maximum stack size.
        /// </summary>
        int MaxStackSize { get; }

        /// <summary>
        /// Gets the item's prefix.
        /// </summary>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets the item's rarity.
        /// </summary>
        ItemRarity Rarity { get; }

        /// <summary>
        /// Sets the item's type. This will update the item accordingly.
        /// </summary>
        /// <param name="type">The item type.</param>
        void SetType(ItemType type);

        /// <summary>
        /// Sets the item's prefix. This will update the item accordingly.
        /// </summary>
        /// <param name="prefix">The item prefix.</param>
        void SetPrefix(ItemPrefix prefix);
    }
}
