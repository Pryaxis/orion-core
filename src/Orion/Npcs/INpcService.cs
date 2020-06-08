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

using System.Collections.Generic;
using Orion.Framework;
using Orion.Packets.DataStructures;

namespace Orion.Npcs {
    /// <summary>
    /// Represents an NPC service. Provides access to NPCs and publishes NPC-related events.
    /// </summary>
    [Service(ServiceScope.Singleton)]
    public interface INpcService {
        /// <summary>
        /// Gets the NPCs.
        /// </summary>
        /// <value>The NPCs.</value>
        IReadOnlyList<INpc> Npcs { get; }

        /// <summary>
        /// Spawns an NPC with the given <paramref name="id"/> at the specified <paramref name="position"/>.
        /// </summary>
        /// <param name="id">The NPC ID to spawn.</param>
        /// <param name="position">The position to spawn the NPC at.</param>
        /// <returns>The resulting NPC, or <see langword="null"/> if none was spawned.</returns>
        INpc? SpawnNpc(NpcId id, Vector2f position);
    }
}
