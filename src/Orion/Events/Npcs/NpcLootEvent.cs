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
using Destructurama.Attributed;
using Orion.Items;
using Orion.Npcs;

namespace Orion.Events.Npcs {
    /// <summary>
    /// An event that occurs when an NPC drops loot. This event can be canceled.
    /// </summary>
    [Event("npc-loot")]
    public sealed class NpcLootEvent : NpcEvent, ICancelable {
        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        /// <value>The item ID.</value>
        public ItemId Id { get; set; }

        /// <summary>
        /// Gets or sets the stack size.
        /// </summary>
        /// <value>The stack size.</value>
        public int StackSize { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public ItemPrefix Prefix { get; set; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcLootEvent"/> class with the specified
        /// <paramref name="npc"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcLootEvent(INpc npc) : base(npc) { }
    }
}
