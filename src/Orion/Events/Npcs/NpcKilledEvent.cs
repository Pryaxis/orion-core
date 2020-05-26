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
using Orion.Npcs;

namespace Orion.Events.Npcs {
    /// <summary>
    /// An event that occurs when an NPC is killed.
    /// </summary>
    [Event("npc-killed")]
    public sealed class NpcKilledEvent : NpcEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcKilledEvent"/> class with the specified
        /// <paramref name="npc"/>.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcKilledEvent(INpc npc) : base(npc) { }
    }
}
