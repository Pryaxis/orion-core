// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using Orion.Entities;
using Orion.Utils;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Npcs {
    /// <summary>
    /// Represents a Terraria NPC. NPCs are equivalent to "mobs", and may either be friendly or hostile.
    /// </summary>
    public interface INpc : IEntity, IWrapping<TerrariaNpc> {
        /// <summary>
        /// Gets the NPC's type.
        /// </summary>
        NpcType Type { get; }

        /// <summary>
        /// Sets the NPC's type. This will update the NPC accordingly.
        /// </summary>
        /// <param name="type">The NPC type.</param>
        void SetType(NpcType type);
    }
}
