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
        public EventHandlerCollection<NpcLootEventArgs> NpcLoot { get; }
        public EventHandlerCollection<NpcKilledEventArgs> NpcKilled { get; }

        public OrionNpcService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.npc != null, "Terraria NPCs should not be null");

            Npcs = new WrappedReadOnlyArray<OrionNpc, TerrariaNpc>(
                // Ignore the last NPC since it is used as a failure slot.
                Main.npc.AsMemory(..^1),
                (npcIndex, terrariaNpc) => new OrionNpc(npcIndex, terrariaNpc));

            NpcSetDefaults = new EventHandlerCollection<NpcSetDefaultsEventArgs>();
            NpcSpawn = new EventHandlerCollection<NpcSpawnEventArgs>();
            NpcUpdate = new EventHandlerCollection<NpcUpdateEventArgs>();
            NpcTransform = new EventHandlerCollection<NpcTransformEventArgs>();
            NpcDamage = new EventHandlerCollection<NpcDamageEventArgs>();
            NpcLoot = new EventHandlerCollection<NpcLootEventArgs>();
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
                // Not localized because this string is developer-facing.
                throw new ArgumentException($"Array does not have length {TerrariaNpc.maxAI}.", nameof(aiValues));
            }

            LogSpawnNpc(type, position);

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = TerrariaNpc.NewNPC((int)position.X, (int)position.Y, (int)type, 0, ai0, ai1, ai2, ai3);
            return npcIndex >= 0 && npcIndex < Npcs.Count ? Npcs[npcIndex] : null;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogSpawnNpc(NpcType type, Vector2 position) =>
            // Not localized because this string is developer-facing.
            Log.Debug("Spawning {NpcType} at {Position}", type, position);

        private INpc GetNpc(TerrariaNpc terrariaNpc) {
            Debug.Assert(terrariaNpc.whoAmI >= 0 && terrariaNpc.whoAmI < Npcs.Count,
                "Terraria NPC should have a valid index");

            // We want to retrieve the world NPC if this NPC is real. Otherwise, return a "fake" NPC.
            return terrariaNpc == Main.npc[terrariaNpc.whoAmI]
                ? Npcs[terrariaNpc.whoAmI]
                : new OrionNpc(terrariaNpc);
        }

        // =============================================================================================================
        // Handling NpcSetDefaults
        // =============================================================================================================

        private HookResult PreSetDefaultsByIdHandler(TerrariaNpc terrariaNpc, ref int npcType, ref float _) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return HookResult.Continue;
            }

            var npc = GetNpc(terrariaNpc);
            var args = new NpcSetDefaultsEventArgs(npc, (NpcType)npcType);

            LogNpcSetDefaults_Before(args);
            NpcSetDefaults.Invoke(this, args);
            LogNpcSetDefaults_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (args.IsDirty) {
                npcType = (int)args.NpcType;
            }

            // Ignore two calls to SetDefaults() if type is negative. This is because SetDefaults gets called twice:
            // once with 0, and once with the base type.
            if (npcType < 0) {
                _setDefaultsToIgnore.Value = 2;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcSetDefaults_Before(NpcSetDefaultsEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Npc}, {NpcType}]", NpcSetDefaults, args.Npc, args.NpcType);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcSetDefaults_After(NpcSetDefaultsEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {CancellationReason}", NpcSetDefaults, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Altered {Event} to [{Npc}, {NpcType}]", NpcSetDefaults, args.Npc, args.NpcType);
            }
        }

        // =============================================================================================================
        // Handling NpcSpawn
        // =============================================================================================================

        private HookResult SpawnHandler(ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "NPC index should be valid");

            var npc = Npcs[npcIndex];
            var args = new NpcSpawnEventArgs(npc);

            LogNpcSpawn_Before(args);
            NpcSpawn.Invoke(this, args);
            LogNpcSpawn_After(args);

            if (args.IsCanceled()) {
                // To cancel the event, we should remove the NPC and return the failure index.
                npc.IsActive = false;
                npcIndex = Npcs.Count;
                return HookResult.Cancel;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcSpawn_Before(NpcSpawnEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Npc}, {NpcType} @ {Position}]",
                NpcSpawn, args.Npc, args.Npc.Type, args.Npc.Position);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcSpawn_After(NpcSpawnEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcSpawn, args.CancellationReason);
            }
        }

        // =============================================================================================================
        // Handling NpcUpdate
        // =============================================================================================================

        private HookResult PreUpdateHandler(TerrariaNpc _, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Npcs.Count, "NPC index should be valid");

            var npc = Npcs[npcIndex];
            var args = new NpcUpdateEventArgs(npc);

            LogNpcUpdate_Before(args);
            NpcUpdate.Invoke(this, args);
            LogNpcUpdate_After(args);

            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcUpdate_Before(NpcUpdateEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Npc}]", NpcUpdate, args.Npc);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcUpdate_After(NpcUpdateEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {CancellationReason}", NpcUpdate, args.CancellationReason);
            }
        }

        // =============================================================================================================
        // Handling NpcTransform
        // =============================================================================================================

        private HookResult PreTransformHandler(TerrariaNpc terrariaNpc, ref int newNpcType) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcTransformEventArgs(npc, (NpcType)newNpcType);

            LogNpcTransform_Before(args);
            NpcTransform.Invoke(this, args);
            LogNpcTransform_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (args.IsDirty) {
                newNpcType = (int)args.NewNpcType;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcTransform_Before(NpcTransformEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcNewType}]", NpcTransform, args.Npc, args.NewNpcType);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcTransform_After(NpcTransformEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcTransform, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{Npc}, {NpcNewType}]", NpcTransform, args.Npc, args.NewNpcType);
            }
        }

        // =============================================================================================================
        // Handling NpcDamage
        // =============================================================================================================

        private HookResult StrikeHandler(
                TerrariaNpc terrariaNpc, ref double _, ref int damage, ref float knockback, ref int hitDirection,
                ref bool isCriticalHit, ref bool _2, ref bool _3, TerrariaEntity _4) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcDamageEventArgs(npc, damage, knockback, hitDirection == 1, isCriticalHit);

            LogNpcDamage_Before(args);
            NpcDamage.Invoke(this, args);
            LogNpcDamage_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (args.IsDirty) {
                damage = args.Damage;
                knockback = args.Knockback;
                hitDirection = args.HitDirection ? 1 : -1;
                isCriticalHit = args.IsCriticalHit;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcDamage_Before(NpcDamageEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Npc}, {NpcType} for {Damage}]",
                NpcDamage, args.Npc, args.Npc.Type, args.Damage);

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcDamage_After(NpcDamageEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcDamage, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Npc}, {NpcType} for {Damage}]",
                    NpcDamage, args.Npc, args.Npc.Type, args.Damage);
            }
        }

        // =============================================================================================================
        // Handling NpcLoot
        // =============================================================================================================

        private HookResult PreDropLootHandler(
                TerrariaNpc terrariaNpc, ref int _, ref int _2, ref int _3, ref int _4, ref int _5, ref int itemType,
                ref int itemStackSize, ref bool _6, ref int itemPrefix, ref bool _7, ref bool _8) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcLootEventArgs(npc, (ItemType)itemType, itemStackSize, (ItemPrefix)itemPrefix);

            LogNpcLoot_Before(args);
            NpcLoot.Invoke(this, args);
            LogNpcLoot_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (args.IsDirty) {
                itemType = (int)args.ItemType;
                itemStackSize = args.StackSize;
                itemPrefix = (int)args.ItemPrefix;
            }

            return HookResult.Continue;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcLoot_Before(NpcLootEventArgs args) {
            if (args.ItemPrefix == ItemPrefix.None) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Npc}, {NpcType} dropping {ItemType} x{ItemStackSize}]",
                    NpcLoot, args.Npc, args.Npc.Type, args.ItemType, args.StackSize);
            } else {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Npc}, {NpcType} dropping {ItemPrefix} {ItemType}]",
                    NpcLoot, args.Npc, args.Npc.Type, args.ItemPrefix, args.ItemType);
            }
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcLoot_After(NpcLootEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {CancellationReason}", NpcLoot, args.CancellationReason);
            } else if (args.IsDirty) {
                if (args.ItemPrefix == ItemPrefix.None) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Npc}, {NpcType} dropping {ItemType} x{ItemStackSize}]",
                        NpcLoot, args.Npc, args.Npc.Type, args.ItemType, args.StackSize);
                } else {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Npc}, {NpcType} dropping {ItemPrefix} {ItemType}]",
                        NpcLoot, args.Npc, args.Npc.Type, args.ItemPrefix, args.ItemType);
                }
            }
        }

        // =============================================================================================================
        // Handling NpcKilled
        // =============================================================================================================

        private void KilledHandler(TerrariaNpc terrariaNpc) {
            Debug.Assert(terrariaNpc != null, "Terraria NPC should not be null");

            var npc = GetNpc(terrariaNpc);
            var args = new NpcKilledEventArgs(npc);

            LogNpcKilled(args);
            NpcKilled.Invoke(this, args);
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogNpcKilled(NpcKilledEventArgs args) =>
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Npc}, {NpcType}]", NpcKilled, args.Npc, args.Npc.Type);
    }
}
