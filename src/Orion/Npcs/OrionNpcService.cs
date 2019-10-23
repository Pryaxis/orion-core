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

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Npcs;
using Orion.Items;
using Orion.Utils;
using OTAPI;
using Serilog;
using Main = Terraria.Main;
using TerrariaEntity = Terraria.Entity;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Npcs {
    [Service("orion-npcs")]
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();

        public IReadOnlyArray<INpc> Npcs { get; }

        public OrionNpcService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.npc != null, "Terraria NPCs should not be null");

            Npcs = new WrappedReadOnlyArray<OrionNpc, TerrariaNpc>(
                // Ignore the last NPC since it is used as a failure slot.
                Main.npc.AsMemory(..^1),
                (npcIndex, terrariaNpc) => new OrionNpc(npcIndex, terrariaNpc));

            Hooks.Npc.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Npc.Spawn = SpawnHandler;
            Hooks.Npc.PreUpdate = PreUpdateHandler;
            Hooks.Npc.PreTransform = PreTransformHandler;
            Hooks.Npc.Strike = StrikeHandler;
            Hooks.Npc.PreDropLoot = PreDropLootHandler;
            Hooks.Npc.Killed = KilledHandler;
        }

        public override void Dispose() {
            _setDefaultsToIgnore.Dispose();

            Hooks.Npc.PreSetDefaultsById = null;
            Hooks.Npc.Spawn = null;
            Hooks.Npc.PreUpdate = null;
            Hooks.Npc.PreTransform = null;
            Hooks.Npc.Strike = null;
            Hooks.Npc.PreDropLoot = null;
            Hooks.Npc.Killed = null;
        }

        public INpc? SpawnNpc(NpcType type, Vector2 position, float[]? aiValues = null) {
            if (aiValues != null && aiValues.Length != TerrariaNpc.maxAI) {
                // Not localized because this string is developer-facing.
                throw new ArgumentException($"Array does not have length {TerrariaNpc.maxAI}.", nameof(aiValues));
            }

            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {NpcType} at {Position}", type, position);

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = TerrariaNpc.NewNPC((int)position.X, (int)position.Y, (int)type, 0, ai0, ai1, ai2, ai3);
            return npcIndex >= 0 && npcIndex < Npcs.Count ? Npcs[npcIndex] : null;
        }

        private INpc GetNpc(TerrariaNpc terrariaNpc) {
            Debug.Assert(terrariaNpc.whoAmI >= 0 && terrariaNpc.whoAmI < Npcs.Count,
                "Terraria NPC should have a valid index");

            // We want to retrieve the world NPC if this NPC is real. Otherwise, return a "fake" NPC.
            return terrariaNpc == Main.npc[terrariaNpc.whoAmI]
                ? Npcs[terrariaNpc.whoAmI]
                : new OrionNpc(terrariaNpc);
        }

        private HookResult PreSetDefaultsByIdHandler(TerrariaNpc terrariaNpc, ref int npcType, ref float _) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return HookResult.Continue;
            }

            var npc = GetNpc(terrariaNpc);
            var e = new NpcDefaultsEvent(npc, (NpcType)npcType);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            } else if (e.IsDirty) {
                npcType = (int)e.NpcType;
            }

            // Ignore two calls to SetDefaults() if type is negative. This is because SetDefaults gets called twice:
            // once with 0, and once with the base type.
            if (npcType < 0) {
                _setDefaultsToIgnore.Value = 2;
            }

            return HookResult.Continue;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "NPC index should be valid");

            var npc = Npcs[npcIndex];
            var e = new NpcSpawnEvent(npc);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                // To cancel the event, we should remove the NPC and return the failure index.
                npc.IsActive = false;
                npcIndex = Npcs.Count;
                return HookResult.Cancel;
            }

            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(TerrariaNpc _, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "NPC index should be valid");

            var npc = Npcs[npcIndex];
            var e = new NpcUpdateEvent(npc);
            Kernel.RaiseEvent(e, Log);

            return e.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreTransformHandler(TerrariaNpc terrariaNpc, ref int newNpcType) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var e = new NpcTransformEvent(npc, (NpcType)newNpcType);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            } else if (e.IsDirty) {
                newNpcType = (int)e.NewNpcType;
            }

            return HookResult.Continue;
        }

        private HookResult StrikeHandler(
                TerrariaNpc terrariaNpc, ref double _, ref int damage, ref float knockback, ref int hitDirection,
                ref bool isCriticalHit, ref bool _2, ref bool _3, TerrariaEntity _4) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var e = new NpcDamageEvent(npc, damage, knockback, hitDirection == 1, isCriticalHit);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            } else if (e.IsDirty) {
                damage = e.Damage;
                knockback = e.Knockback;
                hitDirection = e.HitDirection ? 1 : -1;
                isCriticalHit = e.IsCriticalHit;
            }

            return HookResult.Continue;
        }

        private HookResult PreDropLootHandler(
                TerrariaNpc terrariaNpc, ref int _, ref int _2, ref int _3, ref int _4, ref int _5, ref int itemType,
                ref int itemStackSize, ref bool _6, ref int itemPrefix, ref bool _7, ref bool _8) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var e = new NpcDropItemEvent(npc, (ItemType)itemType, itemStackSize, (ItemPrefix)itemPrefix);
            Kernel.RaiseEvent(e, Log);

            if (e.IsCanceled()) {
                return HookResult.Cancel;
            } else if (e.IsDirty) {
                itemType = (int)e.ItemType;
                itemStackSize = e.StackSize;
                itemPrefix = (int)e.ItemPrefix;
            }

            return HookResult.Continue;
        }

        private void KilledHandler(TerrariaNpc terrariaNpc) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var e = new NpcKilledEvent(npc);
            Kernel.RaiseEvent(e, Log);
        }
    }
}
