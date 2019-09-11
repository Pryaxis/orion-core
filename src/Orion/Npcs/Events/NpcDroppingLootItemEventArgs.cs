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

using System;
using System.ComponentModel;
using Orion.Items;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcDroppingLootItem"/> handlers.
    /// </summary>
    public sealed class NpcDroppingLootItemEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is dropping the loot item.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the loot item type.
        /// </summary>
        public ItemType LootItemType { get; set; }

        /// <summary>
        /// Gets or sets the loot item stack size.
        /// </summary>
        public int LootItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the loot item prefix.
        /// </summary>
        public ItemPrefix LootItemPrefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDroppingLootItemEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public NpcDroppingLootItemEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
