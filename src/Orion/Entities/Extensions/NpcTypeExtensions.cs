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

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Orion.Entities.Extensions {
    /// <summary>
    /// Provides extensions for the <see cref="NpcType"/> enumeration.
    /// </summary>
    [PublicAPI]
    public static class NpcTypeExtensions {
        [NotNull] private static readonly ISet<NpcType> CatchableNpcTypes = new HashSet<NpcType> {
            (NpcType)46,
            (NpcType)55,
            (NpcType)74,
            (NpcType)148,
            (NpcType)149,
            (NpcType)297,
            (NpcType)298,
            (NpcType)299,
            (NpcType)300,
            (NpcType)355,
            (NpcType)356,
            (NpcType)357,
            (NpcType)358,
            (NpcType)359,
            (NpcType)360,
            (NpcType)361,
            (NpcType)362,
            (NpcType)363,
            (NpcType)364,
            (NpcType)365,
            (NpcType)366,
            (NpcType)367,
            (NpcType)374,
            (NpcType)377,
            (NpcType)442,
            (NpcType)443,
            (NpcType)444,
            (NpcType)445,
            (NpcType)446,
            (NpcType)447,
            (NpcType)448,
            (NpcType)484,
            (NpcType)485,
            (NpcType)486,
            (NpcType)487,
            (NpcType)538,
            (NpcType)539
        };

        /// <summary>
        /// Returns a value indicating whether the NPC type is catchable.
        /// </summary>
        /// <param name="npcType">The NPC type.</param>
        /// <returns>A value indicating whether the NPC type is catchable.</returns>
        public static bool IsCatchable(this NpcType npcType) => CatchableNpcTypes.Contains(npcType);
    }
}
