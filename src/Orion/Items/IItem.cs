// Copyright (c) 2020 Pryaxis & Orion Contributors
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

namespace Orion.Items {
    /// <summary>
    /// Represents a Terraria item.
    /// </summary>
    public interface IItem : IEntity, IWrapping<Terraria.Item> {
        /// <summary>
        /// Gets the item's ID.
        /// </summary>
        /// <value>The item's ID.</value>
        ItemId Id { get; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        /// <value>The item's stack size.</value>
        int StackSize { get; set; }

        /// <summary>
        /// Gets the item's prefix.
        /// </summary>
        /// <value>The item's prefix.</value>
        ItemPrefix Prefix { get; }

        /// <summary>
        /// Sets the item's <paramref name="id"/>. This will update the item accordingly. 
        /// </summary>
        /// <param name="id">The item ID.</param>
        void SetId(ItemId id);

        /// <summary>
        /// Sets the item's <paramref name="prefix"/>. This will update the item accordingly.
        /// </summary>
        /// <param name="prefix">The item prefix.</param>
        void SetPrefix(ItemPrefix prefix);
    }
}
