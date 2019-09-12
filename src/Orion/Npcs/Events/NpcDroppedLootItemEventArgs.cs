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
    /// Provides data for the <see cref="INpcService.NpcDroppedLootItem"/> handlers.
    /// </summary>
    public sealed class NpcDroppedLootItemEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that dropped the loot item.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets the loot item.
        /// </summary>
        public IItem LootItem { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDroppedLootItemEventArgs"/> class with the specified NPC
        /// and loot item.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="lootItem">The loot item.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="npc"/> or <paramref name="lootItem"/> are <c>null</c>.
        /// </exception>
        public NpcDroppedLootItemEventArgs(INpc npc, IItem lootItem) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
            LootItem = lootItem ?? throw new ArgumentNullException(nameof(lootItem));
        }
    }
}
