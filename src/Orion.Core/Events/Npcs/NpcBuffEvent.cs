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
using Orion.Core.Buffs;
using Orion.Core.Npcs;
using Orion.Core.Players;

namespace Orion.Core.Events.Npcs {
    /// <summary>
    /// An event that occurs when an NPC is being buffed.
    /// </summary>
    [Event("npc-buff")]
    public sealed class NpcBuffEvent : NpcEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcBuffEvent"/> class with the specified
        /// <paramref name="npc"/> and <paramref name="source"/>.
        /// </summary>
        /// <param name="npc">The NPC being buffed.</param>
        /// <param name="source">The player buffing the NPC.</param>
        /// <param name="buff">The buff to buff the NPC with.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="npc"/> or <paramref name="source"/> are <see langword="null"/>.
        /// </exception>
        public NpcBuffEvent(INpc npc, IPlayer source, Buff buff) : base(npc) {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Buff = buff;
        }

        /// <summary>
        /// Gets the player buffing the NPC.
        /// </summary>
        /// <value>The player buffing the NPC.</value>
        public IPlayer Source { get; }

        /// <summary>
        /// Gets the buff that the NPC is being buffed with.
        /// </summary>
        /// <value>The buff that the NPC is being buffed with.</value>
        public Buff Buff { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
