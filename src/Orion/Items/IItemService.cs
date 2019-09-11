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

using Microsoft.Xna.Framework;
using Orion.Hooks;
using Orion.Items.Events;
using Orion.Utils;

namespace Orion.Items {
    /// <summary>
    /// Represents a service that manages items. Provides item-related hooks and methods.
    /// </summary>
    public interface IItemService : IReadOnlyArray<IItem>, IService {
        /// <summary>
        /// Gets or sets the hook handlers that occur when an item is having its defaults set. This hook can be handled.
        /// </summary>
        HookHandlerCollection<SettingItemDefaultsEventArgs> SettingItemDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an item had its defaults set.
        /// </summary>
        HookHandlerCollection<SetItemDefaultsEventArgs> SetItemDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an item is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingItemEventArgs> UpdatingItem { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an item was updated.
        /// </summary>
        HookHandlerCollection<UpdatedItemEventArgs> UpdatedItem { get; set; }

        /// <summary>
        /// Spawns and returns an item with the given <see cref="ItemType"/> at the specified position with the stack
        /// size and <see cref="ItemPrefix"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/>.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackSize">The stack size.</param>
        /// <param name="prefix">The <see cref="ItemPrefix"/>.</param>
        /// <returns>The resulting <see cref="IItem"/> instance, or <c>null</c> if none was spawned.</returns>
        IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);
    }
}
