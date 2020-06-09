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
using Orion.Core.Npcs;
using Orion.Core.Players;

namespace Orion.Core.Events.Npcs {
    /// <summary>
    /// An event that occurs when a player is catching an NPC. This event can be canceled.
    /// </summary>
    [Event("npc-catch")]
    public sealed class NpcCatchEvent : NpcEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcCatchEvent"/> class with the specified
        /// <paramref name="npc"/> and <paramref name="player"/>.
        /// </summary>
        /// <param name="npc">The NPC being caught.</param>
        /// <param name="player">The player catching the NPC.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="npc"/> or <paramref name="player"/> are <see langword="null"/>.
        /// </exception>
        public NpcCatchEvent(INpc npc, IPlayer player) : base(npc) {
            Player = player ?? throw new ArgumentNullException(nameof(player));
        }

        /// <summary>
        /// Gets the player catching the NPC.
        /// </summary>
        /// <value>The player catching the NPC.</value>
        public IPlayer Player { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
