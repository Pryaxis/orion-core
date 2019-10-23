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

using System;
using Destructurama.Attributed;
using Orion.Npcs;
using Orion.Utils;

namespace Orion.Events.Npcs {
    /// <summary>
    /// An event that occurs when an NPC's defaults are being set. This event can be canceled and modified.
    /// </summary>
    /// <remarks>
    /// This event occurs when an NPC is being created, which can happen:
    /// <list type="bullet">
    /// <item>
    /// <description>When the server starts up, where all NPCs are initialized.</description>
    /// </item>
    /// <item>
    /// <description>When an NPC spawns in the world.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [Event("npc-defaults")]
    public sealed class NpcDefaultsEvent : NpcEvent, ICancelable, IDirtiable {
        private NpcType _npcType;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the NPC type that the NPC's defaults are being set to.
        /// </summary>
        /// <value>The NPC type that the NPC's defaults are being set to.</value>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDefaultsEvent"/> class with the specified NPC and NPC type.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="npcType">The NPC type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcDefaultsEvent(INpc npc, NpcType npcType) : base(npc) {
            _npcType = npcType;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
