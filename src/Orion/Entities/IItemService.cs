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
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Entities;
using Orion.Utils;

namespace Orion.Entities {
    /// <summary>
    /// Represents an item service. Provides access to item-related events and methods.
    /// </summary>
    [PublicAPI]
    public interface IItemService : IReadOnlyArray<IItem>, IService {
        /// <summary>
        /// Gets or sets the event handlers that occur when an item's defaults are being set. This event can be
        /// canceled.
        /// </summary>
        EventHandlerCollection<ItemSetDefaultsEventArgs> ItemSetDefaults { get; set; }

        /// <summary>
        /// Gets or sets the event handlers that occur when an item is updating. This event can be canceled.
        /// </summary>
        EventHandlerCollection<ItemUpdateEventArgs> ItemUpdate { get; set; }

        /// <summary>
        /// Spawns and returns an item with the given item type at the specified position with the stack
        /// size and item prefix.
        /// </summary>
        /// <param name="itemType">The item type.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackSize">The stack size.</param>
        /// <param name="prefix">The item prefix..</param>
        /// <returns>The resulting item, or <c>null</c> if none was spawned.</returns>
        [CanBeNull]
        IItem SpawnItem(ItemType itemType, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);
    }
}
