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
using System.Diagnostics;
using Orion.Collections;
using Orion.Entities;
using Serilog;

namespace Orion.Npcs {
    [Service("orion-npcs")]
    internal sealed class OrionNpcService : OrionService, INpcService {
        public IReadOnlyArray<INpc> Npcs { get; }

        public OrionNpcService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            // Construct the `Npcs` array. Note that the last NPC should be ignored, as it is not a real NPC.
            Npcs = new WrappedReadOnlyArray<OrionNpc, Terraria.NPC>(
                Terraria.Main.npc.AsMemory(..^1),
                (npcIndex, terrariaNpc) => new OrionNpc(npcIndex, terrariaNpc));
        }

        public override void Dispose() { }
    }
}
