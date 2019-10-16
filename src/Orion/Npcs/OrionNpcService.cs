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
        public EventHandlerCollection<NpcSetDefaultsEventArgs> NpcSetDefaults { get; }
        public EventHandlerCollection<NpcSpawnEventArgs> NpcSpawn { get; }
        public EventHandlerCollection<NpcUpdateEventArgs> NpcUpdate { get; }
        public EventHandlerCollection<NpcTransformEventArgs> NpcTransform { get; }
        public EventHandlerCollection<NpcDamageEventArgs> NpcDamage { get; }
        public EventHandlerCollection<NpcDropLootItemEventArgs> NpcDropLootItem { get; }
        public EventHandlerCollection<NpcKilledEventArgs> NpcKilled { get; }

        public OrionNpcService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.npc != null, "Terraria NPCs should not be null");

            // Ignore the last NPC since it is used as a failure slot.
            Npcs = new WrappedReadOnlyArray<OrionNpc, TerrariaNpc>(
                Main.npc.AsMemory(..^1), (npcIndex, terrariaNpc) => new OrionNpc(npcIndex, terrariaNpc));

            NpcSetDefaults = new EventHandlerCollection<NpcSetDefaultsEventArgs>();
            NpcSpawn = new EventHandlerCollection<NpcSpawnEventArgs>();
            NpcUpdate = new EventHandlerCollection<NpcUpdateEventArgs>();
            NpcTransform = new EventHandlerCollection<NpcTransformEventArgs>();
            NpcDamage = new EventHandlerCollection<NpcDamageEventArgs>();
            NpcDropLootItem = new EventHandlerCollection<NpcDropLootItemEventArgs>();
            NpcKilled = new EventHandlerCollection<NpcKilledEventArgs>();

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

        private HookResult PreSetDefaultsByIdHandler(TerrariaNpc terrariaNpc, ref int npcType_, ref float _) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return HookResult.Continue;
            }

            var npc = GetNpc(terrariaNpc);
            var npcType = (NpcType)npcType_;
            var args = new NpcSetDefaultsEventArgs(npc, npcType);

            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Npc}, {NpcType}]", NpcSetDefaults, npc, npcType);
            NpcSetDefaults.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {CancellationReason}", NpcSetDefaults, args.CancellationReason);
                return HookResult.Cancel;
            }

            // Ignore two calls to SetDefaults() if type is negative. This is because SetDefaults gets called twice:
            // once with 0, and once with the base type.
            if ((npcType_ = (int)args.NpcType) < 0) {
                _setDefaultsToIgnore.Value = 2;
            }

            return HookResult.Continue;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "NPC index should be valid");

            var npc = Npcs[npcIndex];
            var args = new NpcSpawnEventArgs(npc);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcType} @ {Position}]", NpcSpawn, npc, npc.Type, npc.Position);
            NpcSpawn.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcSpawn, args.CancellationReason);

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
            var args = new NpcUpdateEventArgs(npc);

            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Npc}]", NpcUpdate, npc);
            NpcUpdate.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {CancellationReason}", NpcUpdate, args.CancellationReason);
                return HookResult.Cancel;
            }

            return HookResult.Continue;
        }

        private HookResult PreTransformHandler(TerrariaNpc terrariaNpc, ref int npcNewType_) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var npcNewType = (NpcType)npcNewType_;
            var args = new NpcTransformEventArgs(npc, npcNewType);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcNewType}]", NpcTransform, npc, npcNewType);
            NpcTransform.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcTransform, args.CancellationReason);
                return HookResult.Cancel;
            }

            npcNewType_ = (int)args.NpcNewType;
            return HookResult.Continue;
        }

        private HookResult StrikeHandler(
                TerrariaNpc terrariaNpc, ref double _, ref int damage, ref float knockback, ref int hitDirection,
                ref bool isCriticalHit, ref bool _2, ref bool _3, TerrariaEntity _4) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcDamageEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = (sbyte)hitDirection,
                IsCriticalHit = isCriticalHit
            };

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcType} by {Damage}]", NpcDamage, npc, npc.Type, damage);
            NpcDamage.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcDamage, args.CancellationReason);
                return HookResult.Cancel;
            }

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            isCriticalHit = args.IsCriticalHit;
            return HookResult.Continue;
        }

        private HookResult PreDropLootHandler(
                TerrariaNpc terrariaNpc, ref int _, ref int _2, ref int _3, ref int _4, ref int _5, ref int itemType_,
                ref int itemStackSize, ref bool _6, ref int itemPrefix, ref bool _7, ref bool _8) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var itemType = (ItemType)itemType_;
            var args = new NpcDropLootItemEventArgs(npc) {
                LootItemType = itemType,
                LootItemStackSize = itemStackSize,
                LootItemPrefix = (ItemPrefix)itemPrefix
            };

            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Npc}, {NpcType} dropping {ItemType} x{ItemStackSize}]",
                NpcDropLootItem, npc, npc.Type, itemType, itemStackSize);
            NpcDropLootItem.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcDropLootItem, args.CancellationReason);
                return HookResult.Cancel;
            }

            itemType_ = (int)args.LootItemType;
            itemStackSize = args.LootItemStackSize;
            itemPrefix = (int)args.LootItemPrefix;
            return HookResult.Continue;
        }

        private void KilledHandler(TerrariaNpc terrariaNpc) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcKilledEventArgs(npc);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcType}]", NpcKilled, npc, npc.Type);
            NpcKilled.Invoke(this, args);
        }
    }
}
