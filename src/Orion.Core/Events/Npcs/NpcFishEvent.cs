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
using Orion.Core.Events.Players;
using Orion.Core.Npcs;
using Orion.Core.Players;

namespace Orion.Core.Events.Npcs {
    /// <summary>
    /// An event that occurs when a player is fishing an NPC. This event can be canceled.
    /// </summary>
    [Event("npc-fish")]
    public sealed class NpcFishEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcFishEvent"/> class with the specified
        /// <paramref name="player"/>, NPC coordinates, and NPC <paramref name="id"/>.
        /// </summary>
        /// <param name="player">The player fishing the NPC.</param>
        /// <param name="x">The NPC's X coordinate.</param>
        /// <param name="y">The NPC's Y coordinate.</param>
        /// <param name="id">The NPC ID being fished.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public NpcFishEvent(IPlayer player, int x, int y, NpcId id) : base(player) {
            X = x;
            Y = y;
            Id = id;
        }

        /// <summary>
        /// Gets the NPC's X coordinate.
        /// </summary>
        /// <value>The NPC's X coordinate.</value>
        public int X { get; }

        /// <summary>
        /// Gets the NPC's Y coordinate.
        /// </summary>
        /// <value>The NPC's Y coordinate.</value>
        public int Y { get; }

        /// <summary>
        /// Gets the NPC ID being fished.
        /// </summary>
        /// <value>The NPC ID being fished.</value>
        public NpcId Id { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
