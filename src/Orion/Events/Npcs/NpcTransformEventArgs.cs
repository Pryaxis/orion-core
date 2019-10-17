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
using Orion.Npcs;
using Orion.Utils;

namespace Orion.Events.Npcs {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcTransform"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("npc-transform")]
    public sealed class NpcTransformEventArgs : NpcEventArgs, ICancelable, IDirtiable {
        private NpcType _npcNewType;

        /// <inheritdoc/>
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the NPC's new type.
        /// </summary>
        /// <value>The NPC's new type.</value>
        public NpcType NpcNewType {
            get => _npcNewType;
            set {
                _npcNewType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcTransformEventArgs"/> class with the specified NPC and NPC's
        /// new type.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="npcNewType">The NPC's new type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcTransformEventArgs(INpc npc, NpcType npcNewType) : base(npc) {
            NpcNewType = npcNewType;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
