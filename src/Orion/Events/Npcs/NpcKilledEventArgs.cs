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
using JetBrains.Annotations;
using Orion.Npcs;

namespace Orion.Events.Npcs {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcKilled"/> event.
    /// </summary>
    [PublicAPI]
    public sealed class NpcKilledEventArgs : NpcEventArgs {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcKilledEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is <c>null</c>.</exception>
        public NpcKilledEventArgs([NotNull] INpc npc) : base(npc) { }
    }
}
