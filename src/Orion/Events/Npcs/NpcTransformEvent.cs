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
    /// An event that occurs when an NPC transforms into another NPC type. This event can be canceled and modified.
    /// </summary>
    [Event("npc-transform")]
    public sealed class NpcTransformEvent : NpcEvent, ICancelable, IDirtiable {
        private NpcType _newNpcType;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the NPC type that the NPC is transforming into.
        /// </summary>
        /// <value>The NPC type that the NPC is transforming into.</value>
        public NpcType NewNpcType {
            get => _newNpcType;
            set {
                _newNpcType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcTransformEvent"/> class with the specified NPC and new NPC
        /// type.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="newNpcType">The new NPC type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcTransformEvent(INpc npc, NpcType newNpcType) : base(npc) {
            _newNpcType = newNpcType;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
