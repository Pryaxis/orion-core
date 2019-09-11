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
using Orion.Players;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.DamagedNpc"/> handlers.
    /// </summary>
    public sealed class DamagedNpcEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that was damaged.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public int Damage { get; internal set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; internal set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCriticalHit { get; internal set; }

        /// <summary>
        /// Gets the player responsible for damaging the NPC. A value of <c>null</c> indicates that no player was
        /// responsible.
        /// </summary>
        public IPlayer DamagingPlayer { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DamagedNpcEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        public DamagedNpcEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
