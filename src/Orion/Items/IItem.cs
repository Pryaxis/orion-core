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

using Orion.Entities;
using Orion.Utils;
using TerrariaItem = Terraria.Item;

namespace Orion.Items {
    /// <summary>
    /// Represents a Terraria item.
    /// </summary>
    /// <remarks>
    /// Items are the main objects that players utilize. They may be weapons, armor, building materials, accessories,
    /// etc. <para/>
    /// 
    /// There are three types of items:
    /// <list type="bullet">
    /// <item>
    /// <description>Items which are abstract and do not affect the game state.</description>
    /// </item>
    /// <item>
    /// <description>
    /// Items which are abstract and exist in some sort of container, like an inventory or chest.
    /// </description>
    /// </item>
    /// <item>
    /// <description>Items which are concrete and affect the game state.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public interface IItem : IEntity, IWrapping<TerrariaItem> {
        /// <summary>
        /// Gets the item's type.
        /// </summary>
        /// <value>The item's type.</value>
        /// <remarks>
        /// The item's type controls many aspects of the item such as its damage, speed, etc. To set the item's type,
        /// use the <see cref="SetType(ItemType)"/> method.
        /// </remarks>
        ItemType Type { get; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        /// <remarks>
        /// The item's prefix modifies aspects of the item such as its damage, speed, etc. To set the item's prefix, use
        /// the <see cref="SetPrefix(ItemPrefix)"/> method.
        /// </remarks>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Gets the item's stats.
        /// </summary>
        /// <value>The item's stats.</value>
        IItemStats Stats { get; }

        /// <summary>
        /// Sets the item's <paramref name="type"/>. This will update the item accordingly.
        /// </summary>
        /// <param name="type">The item type.</param>
        void SetType(ItemType type);

        /// <summary>
        /// Sets the item's <paramref name="prefix"/>. This will update the item accordingly.
        /// </summary>
        /// <param name="prefix">The item prefix.</param>
        void SetPrefix(ItemPrefix prefix);
    }
}
