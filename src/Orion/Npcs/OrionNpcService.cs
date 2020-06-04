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
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Orion.Collections;
using Orion.Events;
using Orion.Events.Npcs;
using Orion.Framework;
using Orion.Packets.DataStructures;
using Serilog;

namespace Orion.Npcs {
    [Binding("orion-npcs", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();

        public OrionNpcService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            // Construct the `Npcs` array. Note that the last NPC should be ignored, as it is not a real NPC.
            Npcs = new WrappedReadOnlyList<OrionNpc, Terraria.NPC>(
                Terraria.Main.npc.AsMemory(..^1),
                (npcIndex, terrariaNpc) => new OrionNpc(npcIndex, terrariaNpc));

            OTAPI.Hooks.Npc.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Npc.Spawn = SpawnHandler;
            OTAPI.Hooks.Npc.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Npc.Killed = KilledHandler;
        }

        public IReadOnlyList<INpc> Npcs { get; }

        public override void Dispose() {
            _setDefaultsToIgnore.Dispose();

            OTAPI.Hooks.Npc.PreSetDefaultsById = null;
            OTAPI.Hooks.Npc.Spawn = null;
            OTAPI.Hooks.Npc.PreUpdate = null;
            OTAPI.Hooks.Npc.Killed = null;
        }

        public INpc? SpawnNpc(NpcId id, Vector2f position) {
            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {NpcId} at {Position}", id, position);

            var npcIndex = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)id);
            return npcIndex >= 0 && npcIndex < Npcs.Count ? Npcs[npcIndex] : null;
        }

        private OTAPI.HookResult PreSetDefaultsByIdHandler(
                Terraria.NPC terrariaNpc, ref int npcId, ref Terraria.NPCSpawnParams _) {
            Debug.Assert(terrariaNpc != null);

            // Check `_setDefaultsToIgnore` to ignore spurious calls if `SetDefaultsById()` is called with a negative
            // ID. A thread-local value is used in case there is some concurrency.
            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return OTAPI.HookResult.Continue;
            }

            var npc = GetNpc(terrariaNpc);
            var evt = new NpcDefaultsEvent(npc) { Id = (NpcId)npcId };
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                return OTAPI.HookResult.Cancel;
            }

            npcId = (int)evt.Id;
            if (npcId < 0) {
                _setDefaultsToIgnore.Value = 2;
            }
            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult SpawnHandler(ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count);

            var npc = Npcs[npcIndex];
            var evt = new NpcSpawnEvent(npc);
            Kernel.Raise(evt, Log);
            if (evt.IsCanceled()) {
                // To cancel the event, remove the NPC and return the failure index.
                npc.IsActive = false;
                npcIndex = Npcs.Count;
                return OTAPI.HookResult.Cancel;
            }

            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.NPC _, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count);

            var evt = new NpcTickEvent(Npcs[npcIndex]);
            Kernel.Raise(evt, Log);
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void KilledHandler(Terraria.NPC terrariaNpc) {
            Debug.Assert(terrariaNpc != null);

            var evt = new NpcKilledEvent(GetNpc(terrariaNpc));
            Kernel.Raise(evt, Log);
        }

        // Gets an `INpc` which corresponds to the given Terraria NPC. Retrieves the `INpc` from the `Npcs` array, if
        // possible.
        private INpc GetNpc(Terraria.NPC terrariaNpc) {
            Debug.Assert(terrariaNpc != null);

            var npcIndex = terrariaNpc.whoAmI;
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count);

            var isConcrete = terrariaNpc == Terraria.Main.npc[npcIndex];
            return isConcrete ? Npcs[npcIndex] : new OrionNpc(terrariaNpc);
        }
    }
}
