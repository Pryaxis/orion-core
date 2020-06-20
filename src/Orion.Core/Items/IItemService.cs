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

using System.Collections.Generic;
using Orion.Core.DataStructures;
using Orion.Core.Events.Items;
using Orion.Core.Framework;

namespace Orion.Core.Items
{
    /// <summary>
    /// Represents an item service. Provides access to items and publishes item-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The item service is responsible for publishing the following item-related events:
    /// <list type="bullet">
    /// <item><description><see cref="ItemDefaultsEvent"/></description></item>
    /// <item><description><see cref="ItemTickEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface IItemService
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        IReadOnlyList<IItem> Items { get; }

        /// <summary>
        /// Spawns the given <paramref name="itemStack"/> at the specified <paramref name="position"/>. Returns the
        /// resulting item.
        /// </summary>
        /// <param name="itemStack">The item stack to spawn.</param>
        /// <param name="position">The position to spawn the item at.</param>
        /// <returns>The resulting item.</returns>
        IItem SpawnItem(ItemStack itemStack, Vector2f position);
    }
}
