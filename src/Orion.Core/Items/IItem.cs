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

using System;
using System.Diagnostics.Contracts;
using Orion.Core.Entities;

namespace Orion.Core.Items
{
    /// <summary>
    /// Represents a Terraria item.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe: i.e., each operation on the item should be atomic.
    /// </remarks>
    public interface IItem : IEntity
    {
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
        /// Gets or sets the item's damage.
        /// </summary>
        /// <value>The item's damage.</value>
        int Damage { get; set; }

        /// <summary>
        /// Sets the item's <paramref name="id"/>. This will update the item accordingly. 
        /// </summary>
        /// <param name="id">The item ID to set the item to.</param>
        void SetId(ItemId id);

        /// <summary>
        /// Sets the item's <paramref name="prefix"/>. This will update the item accordingly.
        /// </summary>
        /// <param name="prefix">The item prefix to set the item to.</param>
        void SetPrefix(ItemPrefix prefix);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IItem"/> interface.
    /// </summary>
    public static class IItemExtensions
    {
        /// <summary>
        /// Returns the item as an item stack instance.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The item as an item stack instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        [Pure]
        public static ItemStack AsItemStack(this IItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new ItemStack(item.Id, item.StackSize, item.Prefix);
        }
    }
}
