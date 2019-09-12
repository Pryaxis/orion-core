// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.ComponentModel;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.SettingNpcDefaults"/> handlers.
    /// </summary>
    public sealed class SettingNpcDefaultsEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is having its defaults set.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the <see cref="NpcType"/> that the NPC is having its defaults set to.
        /// </summary>
        public NpcType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingNpcDefaultsEventArgs"/> class with the specified NPC
        /// and NPC type.
        /// </summary>
        /// <param name="npc">The NPC that is having its defaults set.</param>
        /// <param name="type">The NPC type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public SettingNpcDefaultsEventArgs(INpc npc, NpcType type) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
            Type = type;
        }
    }
}
