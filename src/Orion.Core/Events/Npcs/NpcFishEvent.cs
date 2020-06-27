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
using Orion.Core.Events.Players;
using Orion.Core.Npcs;
using Orion.Core.Players;
using Orion.Core.Utils;

namespace Orion.Core.Events.Npcs
{
    /// <summary>
    /// An event that occurs when a player is fishing an NPC. This event can be canceled.
    /// </summary>
    [Event("npc-fish")]
    public sealed class NpcFishEvent : PlayerEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcFishEvent"/> class with the specified
        /// <paramref name="player"/>, NPC <paramref name="position"/>, and NPC <paramref name="id"/>.
        /// </summary>
        /// <param name="player">The player fishing the NPC.</param>
        /// <param name="position">The fished NPC's position.</param>
        /// <param name="id">The NPC ID being fished.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public NpcFishEvent(IPlayer player, Vector2f position, NpcId id) : base(player)
        {
            Position = position;
            Id = id;
        }

        /// <summary>
        /// Gets the fished NPC's position.
        /// </summary>
        /// <value>The fished NPC's position.</value>
        public Vector2f Position { get; }

        /// <summary>
        /// Gets the NPC ID being fished.
        /// </summary>
        /// <value>The NPC ID being fished.</value>
        public NpcId Id { get; }
    }
}
