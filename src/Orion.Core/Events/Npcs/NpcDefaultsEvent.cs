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
using Orion.Core.Npcs;
using Serilog.Events;

namespace Orion.Core.Events.Npcs {
    /// <summary>
    /// An event that occurs when an NPC's defaults are being set: i.e., when an NPC's stats are being initialized. This
    /// event can be canceled.
    /// </summary>
    [Event("npc-defaults", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class NpcDefaultsEvent : NpcEvent {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDefaultsEvent"/> class with the specified
        /// <paramref name="npc"/>.
        /// </summary>
        /// <param name="npc">The NPC whose defaults are being set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <see langword="null"/>.</exception>
        public NpcDefaultsEvent(INpc npc) : base(npc) { }

        /// <summary>
        /// Gets or sets the NPC ID that the NPC's defaults are being set to.
        /// </summary>
        /// <value>The NPC ID that the NPC's defaults are being set to.</value>
        public NpcId Id { get; set; }
    }
}
