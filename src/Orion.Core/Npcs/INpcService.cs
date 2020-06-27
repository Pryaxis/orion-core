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
using Orion.Core.Events.Npcs;
using Orion.Core.Utils;

namespace Orion.Core.Npcs
{
    /// <summary>
    /// Represents an NPC service. Provides access to NPCs and publishes NPC-related events.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// The NPC service is responsible for publishing the following NPC-related events:
    /// <list type="bullet">
    /// <item><description><see cref="NpcDefaultsEvent"/></description></item>
    /// <item><description><see cref="NpcSpawnEvent"/></description></item>
    /// <item><description><see cref="NpcTickEvent"/></description></item>
    /// <item><description><see cref="NpcKilledEvent"/></description></item>
    /// <item><description><see cref="NpcLootEvent"/></description></item>
    /// <item><description><see cref="NpcBuffEvent"/></description></item>
    /// <item><description><see cref="NpcCatchEvent"/></description></item>
    /// <item><description><see cref="NpcFishEvent"/></description></item>
    /// </list>
    /// </remarks>
    [Service(ServiceScope.Singleton)]
    public interface INpcService : IReadOnlyList<INpc>
    {
        /// <summary>
        /// Spawns an NPC with the given <paramref name="id"/> at the specified <paramref name="position"/>. Returns the
        /// resulting NPC.
        /// </summary>
        /// <param name="id">The NPC ID to spawn.</param>
        /// <param name="position">The position to spawn the NPC at.</param>
        /// <returns>The resulting NPC, or <see langword="null"/> if none was spawned.</returns>
        INpc? Spawn(NpcId id, Vector2f position);
    }
}
