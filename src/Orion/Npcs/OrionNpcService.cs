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
using System.Threading;
using Microsoft.Xna.Framework;
using Ninject;
using Orion.Hooks;
using Orion.Items;
using Orion.Npcs.Events;
using Orion.Players;
using OTAPI;
using Terraria;

namespace Orion.Npcs {
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly IItemService _itemService;
        private readonly IPlayerService _playerService;
        private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();
        private readonly IList<NPC> _terrariaNpcs;
        private readonly IList<OrionNpc> _npcs;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        // We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
        // as a failure index.
        public int Count => _npcs.Count - 1;

        public INpc this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_npcs[index]?.Wrapped != _terrariaNpcs[index]) {
                    _npcs[index] = new OrionNpc(_terrariaNpcs[index]);
                }

                var npc = _npcs[index];
                Debug.Assert(npc != null, $"{nameof(npc)} should not be null.");
                Debug.Assert(npc.Wrapped != null, $"{nameof(npc.Wrapped)} should not be null.");
                return npc;
            }
        }

        public int BaseNpcSpawningRate {
            get => NPC.defaultSpawnRate;
            set => NPC.defaultSpawnRate = value;
        }

        public int BaseNpcSpawningLimit {
            get => NPC.defaultMaxSpawns;
            set => NPC.defaultMaxSpawns = value;
        }

        public HookHandlerCollection<SpawningNpcEventArgs> SpawningNpc { get; set; }
        public HookHandlerCollection<SpawnedNpcEventArgs> SpawnedNpc { get; set; }
        public HookHandlerCollection<SettingNpcDefaultsEventArgs> SettingNpcDefaults { get; set; }
        public HookHandlerCollection<SetNpcDefaultsEventArgs> SetNpcDefaults { get; set; }
        public HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpc { get; set; }
        public HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpcAi { get; set; }
        public HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpcAi { get; set; }
        public HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpc { get; set; }
        public HookHandlerCollection<DamagingNpcEventArgs> DamagingNpc { get; set; }
        public HookHandlerCollection<DamagedNpcEventArgs> DamagedNpc { get; set; }
        public HookHandlerCollection<NpcTransformingEventArgs> NpcTransforming { get; set; }
        public HookHandlerCollection<NpcTransformedEventArgs> NpcTransformed { get; set; }
        public HookHandlerCollection<NpcDroppingLootItemEventArgs> NpcDroppingLootItem { get; set; }
        public HookHandlerCollection<NpcDroppedLootItemEventArgs> NpcDroppedLootItem { get; set; }
        public HookHandlerCollection<KilledNpcEventArgs> KilledNpc { get; set; }

        [Inject]
        public OrionNpcService(IItemService itemService, IPlayerService playerService) {
            Debug.Assert(itemService != null, $"{nameof(itemService)} should not be null.");
            Debug.Assert(playerService != null, $"{nameof(playerService)} should not be null.");

            _itemService = itemService;
            _playerService = playerService;
            _terrariaNpcs = Main.npc;
            _npcs = new OrionNpc[_terrariaNpcs.Count];

            OTAPI.Hooks.Npc.Create = CreateHandler;
            OTAPI.Hooks.Npc.Spawn = SpawnHandler;
            OTAPI.Hooks.Npc.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            OTAPI.Hooks.Npc.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            OTAPI.Hooks.Npc.PreUpdate = PreUpdateHandler;
            OTAPI.Hooks.Npc.PreAI = PreAiHandler;
            OTAPI.Hooks.Npc.PostAI = PostAiHandler;
            OTAPI.Hooks.Npc.PostUpdate = PostUpdateHandler;
            OTAPI.Hooks.Npc.Strike = StrikeHandler;
            OTAPI.Hooks.Npc.PreTransform = PreTransformHandler;
            OTAPI.Hooks.Npc.PostTransform = PostTransformHandler;
            OTAPI.Hooks.Npc.PreDropLoot = PreDropLootHandler;
            OTAPI.Hooks.Npc.Killed = KilledHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            _setDefaultsToIgnore.Dispose();

            OTAPI.Hooks.Npc.Create = null;
            OTAPI.Hooks.Npc.Spawn = null;
            OTAPI.Hooks.Npc.PreSetDefaultsById = null;
            OTAPI.Hooks.Npc.PostSetDefaultsById = null;
            OTAPI.Hooks.Npc.PreUpdate = null;
            OTAPI.Hooks.Npc.PreAI = null;
            OTAPI.Hooks.Npc.PostAI = null;
            OTAPI.Hooks.Npc.PostUpdate = null;
            OTAPI.Hooks.Npc.Strike = null;
            OTAPI.Hooks.Npc.PreTransform = null;
            OTAPI.Hooks.Npc.PostTransform = null;
            OTAPI.Hooks.Npc.PreDropLoot = null;
            OTAPI.Hooks.Npc.Killed = null;
        }

        public IEnumerator<INpc> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INpc SpawnNpc(NpcType type, Vector2 position, float[] aiValues = null) {
            if (aiValues != null && aiValues.Length != NPC.maxAI) {
                throw new ArgumentException($"{nameof(aiValues)} must have length {NPC.maxAI}.",
                                            nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = NPC.NewNPC((int)position.X, (int)position.Y, (int)type, 0, ai0, ai1, ai2, ai3);
            return npcIndex >= 0 && npcIndex < Count ? this[npcIndex] : null;
        }


        private NPC CreateHandler(ref int npcIndex, ref int x, ref int y, ref int type, ref int start,
                                           ref float ai0, ref float ai1, ref float ai2, ref float ai3, ref int target) {
            var args = new SpawningNpcEventArgs {
                NpcType = (NpcType)type,
                NpcPosition = new Vector2(x, y),
                NpcTarget = target >= 0 && target < _playerService.Count ? _playerService[target] : null
            };
            args.NpcAiValues[0] = ai0;
            args.NpcAiValues[1] = ai1;
            args.NpcAiValues[2] = ai2;
            args.NpcAiValues[3] = ai3;
            SpawningNpc?.Invoke(this, args);

            var terrariaNpc = new NPC();
            if (args.Handled) {
                // To properly handle the event, npcIndex needs to be set to the failure index described above.
                npcIndex = Count;
            } else {
                terrariaNpc.SetDefaults((int)args.NpcType);
                x = (int)args.NpcPosition.X;
                y = (int)args.NpcPosition.Y;
                type = (int)args.NpcType;
                ai0 = args.NpcAiValues[0];
                ai1 = args.NpcAiValues[1];
                ai2 = args.NpcAiValues[2];
                ai3 = args.NpcAiValues[3];
            }

            return terrariaNpc;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            if (npcIndex < 0 || npcIndex >= Count) return HookResult.Continue;

            var npc = this[npcIndex];
            var args = new SpawnedNpcEventArgs(npc);
            SpawnedNpc?.Invoke(this, args);
            return HookResult.Continue;
        }

        private HookResult PreSetDefaultsByIdHandler(NPC terrariaNpc, ref int type,
                                                           ref float scaleOverride) {
            if (_setDefaultsToIgnore.Value > 0) return HookResult.Continue;

            var npc = new OrionNpc(terrariaNpc);
            var args = new SettingNpcDefaultsEventArgs(npc, (NpcType)type);
            SettingNpcDefaults?.Invoke(this, args);

            type = (int)args.Type;

            // Because SetDefaults with a negative ID calls SetDefaultsFromNetId, which calls SetDefaults(0) and then
            // the correct SetDefaults, we need to ignore two SetDefaults calls.
            _setDefaultsToIgnore.Value = type >= 0 ? 0 : 2;
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(NPC terrariaNpc, int type, float scaleOverride) {
            if (_setDefaultsToIgnore.Value > 0) {
                _setDefaultsToIgnore.Value--;
                return;
            }

            var npc = new OrionNpc(terrariaNpc);
            var args = new SetNpcDefaultsEventArgs(npc);
            SetNpcDefaults?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(NPC terrariaNpc, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = this[npcIndex];
            var args = new UpdatingNpcEventArgs(npc);
            UpdatingNpc?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreAiHandler(NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatingNpcEventArgs(npc);
            UpdatingNpcAi?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostAiHandler(NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatedNpcEventArgs(npc);
            UpdatedNpcAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(NPC terrariaNpc, int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatedNpcEventArgs(npc);
            UpdatedNpc?.Invoke(this, args);
        }

        private HookResult StrikeHandler(NPC terrariaNpc, ref double cancelResult, ref int damage,
                                               ref float knockback, ref int hitDirection, ref bool crit,
                                               ref bool noEffect, ref bool fromNet, Entity entity) {
            var npc = new OrionNpc(terrariaNpc);
            IPlayer damagingPlayer = null;
            if (entity is Player terrariaPlayer) {
                damagingPlayer = _playerService[terrariaPlayer.whoAmI];
            }

            var args = new DamagingNpcEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                DamagingPlayer = damagingPlayer
            };
            DamagingNpc?.Invoke(this, args);
            if (args.Handled) return HookResult.Cancel;

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            crit = args.IsCriticalHit;

            var args2 = new DamagedNpcEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                DamagingPlayer = damagingPlayer
            };
            DamagedNpc?.Invoke(this, args2);
            return HookResult.Continue;
        }

        private HookResult PreTransformHandler(NPC terrariaNpc, ref int newType) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformingEventArgs(npc);
            NpcTransforming?.Invoke(this, args);
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostTransformHandler(NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformedEventArgs(npc);
            NpcTransformed?.Invoke(this, args);
        }

        private HookResult PreDropLootHandler(NPC terrariaNpc, ref int itemId, ref int x, ref int y,
                                                    ref int width, ref int height, ref int type, ref int stack,
                                                    ref bool noBroadcast, ref int prefix, ref bool noGrabDelay,
                                                    ref bool reverseLookup) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDroppingLootItemEventArgs(npc) {
                LootItemType = (ItemType)itemId,
                LootItemStackSize = stack,
                LootItemPrefix = (ItemPrefix)prefix
            };
            NpcDroppingLootItem?.Invoke(this, args);
            if (args.Handled) return HookResult.Cancel;

            // Handle item spawning in this method. This enables us to provide a reference to an IItem in
            // NpcDroppedLootItemEventArgs, but adds a dependency on IItemService.
            var item = _itemService.SpawnItem(args.LootItemType, new Vector2(x + width / 2, y + height / 2),
                                              args.LootItemStackSize, args.LootItemPrefix);
            var args2 = new NpcDroppedLootItemEventArgs(npc, item);
            NpcDroppedLootItem?.Invoke(this, args2);
            return HookResult.Cancel;
        }

        private void KilledHandler(NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new KilledNpcEventArgs(npc);
            KilledNpc?.Invoke(this, args);
        }
    }
}
