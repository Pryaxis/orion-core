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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Events.Npcs;
using Orion.Items;
using OTAPI;

namespace Orion.Npcs {
    internal sealed class OrionNpcService : OrionService, INpcService {
        [NotNull, ItemNotNull] private readonly IList<Terraria.NPC> _terrariaNpcs;
        [NotNull, ItemCanBeNull] private readonly IList<OrionNpc> _npcs;
        [NotNull] private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        // Subtract 1 from the count. This is because Terraria has an extra slot.
        public int Count => _npcs.Count - 1;

        [NotNull]
        public INpc this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_npcs[index]?.Wrapped != _terrariaNpcs[index]) {
                    _npcs[index] = new OrionNpc(_terrariaNpcs[index]);
                }

                Debug.Assert(_npcs[index] != null, "_npcs[index] != null");
                return _npcs[index];
            }
        }

        public EventHandlerCollection<NpcSetDefaultsEventArgs> NpcSetDefaults { get; set; }
        public EventHandlerCollection<NpcSpawnEventArgs> NpcSpawn { get; set; }
        public EventHandlerCollection<NpcUpdateEventArgs> NpcUpdate { get; set; }
        public EventHandlerCollection<NpcTransformEventArgs> NpcTransform { get; set; }
        public EventHandlerCollection<NpcDamageEventArgs> NpcDamage { get; set; }
        public EventHandlerCollection<NpcDropLootItemEventArgs> NpcDropLootItem { get; set; }
        public EventHandlerCollection<NpcKilledEventArgs> NpcKilled { get; set; }

        public OrionNpcService() {
            Debug.Assert(Terraria.Main.npc != null, "Terraria.Main.npc != null");
            Debug.Assert(Terraria.Main.npc.All(n => n != null), "Terraria.Main.npc.All(n => n != null)");

            _terrariaNpcs = Terraria.Main.npc;
            _npcs = new OrionNpc[_terrariaNpcs.Count];

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

            Hooks.Npc.PreSetDefaultsById = null;
            Hooks.Npc.Spawn = null;
            Hooks.Npc.PreUpdate = null;
            Hooks.Npc.PreTransform = null;
            Hooks.Npc.Strike = null;
            Hooks.Npc.PreDropLoot = null;
            Hooks.Npc.Killed = null;
        }

        public IEnumerator<INpc> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INpc SpawnNpc(NpcType npcType, Vector2 position, float[] aiValues = null) {
            if (aiValues != null && aiValues.Length != 4) {
                throw new ArgumentException("Array does not have length 4.", nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)npcType, 0, ai0, ai1, ai2, ai3);
            return npcIndex >= 0 && npcIndex < Count ? this[npcIndex] : null;
        }

        private HookResult PreSetDefaultsByIdHandler([NotNull] Terraria.NPC terrariaNpc, ref int type,
                                                     ref float scaleOverride) {
            if (_setDefaultsToIgnore.Value > 0) {
                --_setDefaultsToIgnore.Value;
                return HookResult.Continue;
            }

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcSetDefaultsEventArgs(npc, (NpcType)type);
            NpcSetDefaults?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;

            // Ignore two calls to SetDefaults() if type is negative.
            if ((type = (int)args.NpcType) < 0) {
                _setDefaultsToIgnore.Value = 2;
            }

            return HookResult.Continue;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            var npc = this[npcIndex];
            var args = new NpcSpawnEventArgs(npc);
            NpcSpawn?.Invoke(this, args);
            if (args.IsCanceled) {
                // To cancel the event, we should remove the NPC and return the failure index.
                npc.IsActive = false;
                npcIndex = Count;
                return HookResult.Cancel;
            }

            return HookResult.Continue;
        }

        private HookResult PreUpdateHandler([NotNull] Terraria.NPC terrariaNpc, ref int npcIndex) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcUpdateEventArgs(npc);
            NpcUpdate?.Invoke(this, args);
            return args.IsCanceled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreTransformHandler([NotNull] Terraria.NPC terrariaNpc, ref int newType) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformEventArgs(npc, (NpcType)newType);
            NpcTransform?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;

            newType = (int)args.NpcNewType;
            return HookResult.Continue;
        }

        private HookResult StrikeHandler([NotNull] Terraria.NPC terrariaNpc, ref double cancelResult, ref int damage,
                                         ref float knockback, ref int hitDirection, ref bool isCriticalHit,
                                         ref bool noEffect, ref bool fromNetwork, Terraria.Entity damagingEntity) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDamageEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = (sbyte)hitDirection,
                IsCriticalHit = isCriticalHit
            };
            NpcDamage?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            isCriticalHit = args.IsCriticalHit;
            return HookResult.Continue;
        }

        private HookResult PreDropLootHandler([NotNull] Terraria.NPC terrariaNpc, ref int itemIndex, ref int x,
                                              ref int y, ref int width, ref int height, ref int type, ref int stack,
                                              ref bool noBroadcast, ref int prefix, ref bool noGrabDelay,
                                              ref bool reverseLookup) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDropLootItemEventArgs(npc) {
                LootItemType = (ItemType)type,
                LootItemStackSize = stack,
                LootItemPrefix = (ItemPrefix)prefix
            };
            NpcDropLootItem?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;

            type = (int)args.LootItemType;
            stack = args.LootItemStackSize;
            prefix = (int)args.LootItemPrefix;
            return HookResult.Continue;
        }

        private void KilledHandler([NotNull] Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcKilledEventArgs(npc);
            NpcKilled?.Invoke(this, args);
        }
    }
}
