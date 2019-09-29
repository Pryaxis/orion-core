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
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Extensions;
using Orion.Events.Npcs;
using Orion.Items;
using Orion.Utils;
using OTAPI;

namespace Orion.Npcs {
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public IReadOnlyArray<INpc> Npcs { get; }
        public EventHandlerCollection<NpcSetDefaultsEventArgs>? NpcSetDefaults { get; set; }
        public EventHandlerCollection<NpcSpawnEventArgs>? NpcSpawn { get; set; }
        public EventHandlerCollection<NpcUpdateEventArgs>? NpcUpdate { get; set; }
        public EventHandlerCollection<NpcTransformEventArgs>? NpcTransform { get; set; }
        public EventHandlerCollection<NpcDamageEventArgs>? NpcDamage { get; set; }
        public EventHandlerCollection<NpcDropLootItemEventArgs>? NpcDropLootItem { get; set; }
        public EventHandlerCollection<NpcKilledEventArgs>? NpcKilled { get; set; }

        public OrionNpcService() {
            // Ignore the last NPC since it is used as a failure slot.
            Npcs = new WrappedReadOnlyArray<OrionNpc, Terraria.NPC>(
                Terraria.Main.npc.AsMemory(..^1),
                (_, terrariaNpc) => new OrionNpc(terrariaNpc));

            Hooks.Npc.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Npc.Spawn = SpawnHandler;
            Hooks.Npc.PreUpdate = PreUpdateHandler;
            Hooks.Npc.PreTransform = PreTransformHandler;
            Hooks.Npc.Strike = StrikeHandler;
            Hooks.Npc.PreDropLoot = PreDropLootHandler;
            Hooks.Npc.Killed = KilledHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            _setDefaultsToIgnore.Dispose();

            Hooks.Npc.PreSetDefaultsById = null;
            Hooks.Npc.Spawn = null;
            Hooks.Npc.PreUpdate = null;
            Hooks.Npc.PreTransform = null;
            Hooks.Npc.Strike = null;
            Hooks.Npc.PreDropLoot = null;
            Hooks.Npc.Killed = null;
        }

        public INpc? SpawnNpc(NpcType npcType, Vector2 position, float[]? aiValues = null) {
            if (aiValues != null && aiValues.Length != Terraria.NPC.maxAI) {
                throw new ArgumentException($"Array does not have length {Terraria.NPC.maxAI}.", nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)npcType, 0, ai0, ai1, ai2, ai3);
            return npcIndex >= 0 && npcIndex < Npcs.Count ? Npcs[npcIndex] : null;
        }

        private HookResult PreSetDefaultsByIdHandler(Terraria.NPC terrariaNpc, ref int type, ref float scaleOverride) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return HookResult.Continue;
            }

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcSetDefaultsEventArgs(npc, (NpcType)type);
            NpcSetDefaults?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            // Ignore two calls to SetDefaults() if type is negative. This is because SetDefaults gets called twice:
            // once with 0, and once with the base type.
            if ((type = (int)args.NpcType) < 0) {
                _setDefaultsToIgnore.Value = 2;
            }

            return HookResult.Continue;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "npcIndex >= 0 && npcIndex < Count");

            var npc = Npcs[npcIndex];
            var args = new NpcSpawnEventArgs(npc);
            NpcSpawn?.Invoke(this, args);
            if (args.IsCanceled()) {
                // To cancel the event, we should remove the NPC and return the failure index.
                npc.IsActive = false;
                npcIndex = Npcs.Count;
                return HookResult.Cancel;
            }

            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler(Terraria.NPC terrariaNpc, ref int npcIndex) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcUpdateEventArgs(npc);
            NpcUpdate?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreTransformHandler(Terraria.NPC terrariaNpc, ref int newType) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformEventArgs(npc, (NpcType)newType);
            NpcTransform?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            newType = (int)args.NpcNewType;
            return HookResult.Continue;
        }

        private HookResult StrikeHandler(Terraria.NPC terrariaNpc, ref double cancelResult, ref int damage,
                                         ref float knockback, ref int hitDirection, ref bool isCriticalHit,
                                         ref bool noEffect, ref bool fromNetwork, Terraria.Entity damagingEntity) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDamageEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = (sbyte)hitDirection,
                IsCriticalHit = isCriticalHit
            };
            NpcDamage?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            isCriticalHit = args.IsCriticalHit;
            return HookResult.Continue;
        }

        private HookResult PreDropLootHandler(Terraria.NPC terrariaNpc, ref int itemIndex, ref int x, ref int y,
                                              ref int width, ref int height, ref int type, ref int stack,
                                              ref bool noBroadcast, ref int prefix, ref bool noGrabDelay,
                                              ref bool reverseLookup) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDropLootItemEventArgs(npc) {
                LootItemType = (ItemType)type,
                LootItemStackSize = stack,
                LootItemPrefix = (ItemPrefix)prefix
            };
            NpcDropLootItem?.Invoke(this, args);
            if (args.IsCanceled()) return HookResult.Cancel;

            type = (int)args.LootItemType;
            stack = args.LootItemStackSize;
            prefix = (int)args.LootItemPrefix;
            return HookResult.Continue;
        }

        private void KilledHandler(Terraria.NPC terrariaNpc) {
            Debug.Assert(terrariaNpc != null, "terrariaNpc != null");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcKilledEventArgs(npc);
            NpcKilled?.Invoke(this, args);
        }
    }
}
