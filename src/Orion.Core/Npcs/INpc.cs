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

using Orion.Core.Entities;

namespace Orion.Core.Npcs {
    /// <summary>
    /// Represents a Terraria NPC.
    /// </summary>
    public interface INpc : IEntity {
        /// <summary>
        /// Gets the NPC's ID.
        /// </summary>
        /// <value>The NPC's ID.</value>
        NpcId Id { get; }

        /// <summary>
        /// Sets the NPC's <paramref name="id"/>. This will update the NPC accordingly. 
        /// </summary>
        /// <param name="id">The NPC ID to set the NPC to.</param>
        void SetId(NpcId id);
    }
}
