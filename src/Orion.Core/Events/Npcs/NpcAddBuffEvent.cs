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
using Orion.Core.Entities;
using Orion.Core.Npcs;
using Orion.Core.Players;

namespace Orion.Core.Events.Npcs
{
    /// <summary>
    /// An event that occurs when a buff is being added to an NPC. This event can be canceled.
    /// </summary>
    [Event("npc-add-buff")]
    public sealed class NpcAddBuffEvent : NpcEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcAddBuffEvent"/> class with the specified
        /// <paramref name="npc"/>, <paramref name="player"/>, and <paramref name="buff"/>.
        /// </summary>
        /// <param name="npc">The NPC being buffed.</param>
        /// <param name="player">The player adding the buff to the NPC.</param>
        /// <param name="buff">The buff being added to the NPC.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="npc"/> or <paramref name="player"/> are <see langword="null"/>.
        /// </exception>
        public NpcAddBuffEvent(INpc npc, IPlayer player, Buff buff) : base(npc)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Buff = buff;
        }

        /// <summary>
        /// Gets the player adding the buff to the NPC.
        /// </summary>
        /// <value>The player adding the buff to the NPC.</value>
        public IPlayer Player { get; }

        /// <summary>
        /// Gets the buff being added to the NPC.
        /// </summary>
        /// <value>The buff being added to the NPC.</value>
        public Buff Buff { get; }
    }
}
