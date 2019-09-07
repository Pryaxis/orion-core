using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Ninject;
using Orion.Items;
using Orion.Npcs.Events;
using Orion.Players;

namespace Orion.Npcs {
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly IItemService _itemService;
        private readonly IPlayerService _playerService;
        private readonly ThreadLocal<int> _setDefaultsToIgnore = new ThreadLocal<int>();
        private readonly IList<Terraria.NPC> _terrariaNpcs;
        private readonly IList<OrionNpc> _npcs;

        [ExcludeFromCodeCoverage]
        public override string Author => "Pryaxis";

        [ExcludeFromCodeCoverage]
        public override string Name => "Orion NPC Service";
        
        /*
         * We need to subtract 1 from the count. This is because Terraria actually has an extra slot which is reserved
         * as a failure index.
         */
        public int Count => _npcs.Count - 1;
        
        public INpc this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

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
            get => Terraria.NPC.defaultSpawnRate;
            set => Terraria.NPC.defaultSpawnRate = value;
        }

        public int BaseNpcSpawningLimit {
            get => Terraria.NPC.defaultMaxSpawns;
            set => Terraria.NPC.defaultMaxSpawns = value;
        }

        public event EventHandler<SpawningNpcEventArgs> SpawningNpc;
        public event EventHandler<SpawnedNpcEventArgs> SpawnedNpc;
        public event EventHandler<SettingNpcDefaultsEventArgs> SettingNpcDefaults;
        public event EventHandler<SetNpcDefaultsEventArgs> SetNpcDefaults;
        public event EventHandler<UpdatingNpcEventArgs> UpdatingNpc;
        public event EventHandler<UpdatingNpcEventArgs> UpdatingNpcAi;
        public event EventHandler<UpdatedNpcEventArgs> UpdatedNpcAi;
        public event EventHandler<UpdatedNpcEventArgs> UpdatedNpc;
        public event EventHandler<DamagingNpcEventArgs> DamagingNpc;
        public event EventHandler<DamagedNpcEventArgs> DamagedNpc;
        public event EventHandler<NpcTransformingEventArgs> NpcTransforming;
        public event EventHandler<NpcTransformedEventArgs> NpcTransformed;
        public event EventHandler<NpcDroppingLootItemEventArgs> NpcDroppingLootItem;
        public event EventHandler<NpcDroppedLootItemEventArgs> NpcDroppedLootItem;
        public event EventHandler<KilledNpcEventArgs> KilledNpc;

        [Inject]
        public OrionNpcService(IItemService itemService, IPlayerService playerService) {
            Debug.Assert(itemService != null, $"{nameof(itemService)} should not be null.");
            Debug.Assert(playerService != null, $"{nameof(playerService)} should not be null.");
            
            _itemService = itemService;
            _playerService = playerService;
            _terrariaNpcs = Terraria.Main.npc;
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
            if (!disposeManaged) {
                return;
            }

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
            if (aiValues != null && aiValues.Length != 4) {
                throw new ArgumentException($"{nameof(aiValues)} must have length 4.", nameof(aiValues));
            }

            var ai0 = aiValues?[0] ?? 0;
            var ai1 = aiValues?[1] ?? 0;
            var ai2 = aiValues?[2] ?? 0;
            var ai3 = aiValues?[3] ?? 0;
            var npcIndex = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)type, 0, ai0, ai1, ai2, ai3);
            if (npcIndex < 0 || npcIndex >= Count) {
                return null;
            }

            return this[npcIndex];
        }


        private Terraria.NPC CreateHandler(ref int npcIndex, ref int x, ref int y, ref int type, ref int start,
                                           ref float ai0, ref float ai1, ref float ai2, ref float ai3, ref int target) {
            var args = new SpawningNpcEventArgs {
                NpcType = (NpcType)type,
                Position = new Vector2(x, y),
                NpcTarget = target >= 0 && target < _playerService.Count ? _playerService[target] : null
            };
            args.AiValues[0] = ai0;
            args.AiValues[1] = ai1;
            args.AiValues[2] = ai2;
            args.AiValues[3] = ai3;
            SpawningNpc?.Invoke(this, args);

            var terrariaNpc = new Terraria.NPC();
            if (args.Handled) {
                /*
                 * To properly handle the event, npcIndex needs to be set to the failure index described above.
                 */
                npcIndex = Count;
            } else {
                terrariaNpc.SetDefaults((int)args.NpcType);
                x = (int)args.Position.X;
                y = (int)args.Position.Y;
                type = (int)args.NpcType;
                ai0 = args.AiValues[0];
                ai1 = args.AiValues[1];
                ai2 = args.AiValues[2];
                ai3 = args.AiValues[3];
            }

            return terrariaNpc;
        }

        private OTAPI.HookResult SpawnHandler(ref int npcIndex) {
            if (npcIndex < 0 || npcIndex >= Count) {
                return OTAPI.HookResult.Continue;
            }

            var npc = this[npcIndex];
            var args = new SpawnedNpcEventArgs(npc);
            SpawnedNpc?.Invoke(this, args);

            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreSetDefaultsByIdHandler(Terraria.NPC terrariaNpc, ref int type, ref float scaleOverride) {
            if (_setDefaultsToIgnore.Value > 0) {
                return OTAPI.HookResult.Continue;
            }

            var npc = new OrionNpc(terrariaNpc);
            var args = new SettingNpcDefaultsEventArgs(npc, (NpcType)type);
            SettingNpcDefaults?.Invoke(this, args);

            type = (int)args.Type;
            _setDefaultsToIgnore.Value = type < 0 ? 2 : 0;
            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.NPC terrariaNpc, int type, float scaleOverride) {
            if (_setDefaultsToIgnore.Value > 0) {
                _setDefaultsToIgnore.Value--;
                return;
            }

            var npc = new OrionNpc(terrariaNpc);
            var args = new SetNpcDefaultsEventArgs(npc);
            SetNpcDefaults?.Invoke(this, args);
        }

        private OTAPI.HookResult PreUpdateHandler(Terraria.NPC terrariaNpc, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = this[npcIndex];
            var args = new UpdatingNpcEventArgs(npc);
            UpdatingNpc?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreAiHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatingNpcEventArgs(npc);
            UpdatingNpcAi?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostAiHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatedNpcEventArgs(npc);
            UpdatedNpcAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(Terraria.NPC terrariaNpc, int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = new OrionNpc(terrariaNpc);
            var args = new UpdatedNpcEventArgs(npc);
            UpdatedNpc?.Invoke(this, args);
        }

        private OTAPI.HookResult StrikeHandler(Terraria.NPC terrariaNpc, ref double cancelResult, ref int damage, ref float knockback,
                                         ref int hitDirection, ref bool crit, ref bool noEffect, ref bool fromNet,
                                         Terraria.Entity entity) {
            var npc = new OrionNpc(terrariaNpc);
            IPlayer playerResponsible = null;
            if (entity is Terraria.Player terrariaPlayer) {
                playerResponsible = _playerService[terrariaPlayer.whoAmI];
            }

            var args = new DamagingNpcEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                PlayerResponsible = playerResponsible,
            };
            DamagingNpc?.Invoke(this, args);

            if (args.Handled) {
                return OTAPI.HookResult.Cancel;
            }

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            crit = args.IsCriticalHit;

            var args2 = new DamagedNpcEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                PlayerResponsible = playerResponsible,
            };
            DamagedNpc?.Invoke(this, args2);

            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreTransformHandler(Terraria.NPC terrariaNpc, ref int newType) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformingEventArgs(npc);
            NpcTransforming?.Invoke(this, args);

            return args.Handled ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }

        private void PostTransformHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformedEventArgs(npc);
            NpcTransformed?.Invoke(this, args);
        }

        private OTAPI.HookResult PreDropLootHandler(Terraria.NPC terrariaNpc, ref int itemId, ref int x, ref int y, ref int width,
                                              ref int height, ref int type, ref int stack, ref bool noBroadcast,
                                              ref int prefix, ref bool noGrabDelay, ref bool reverseLookup) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcDroppingLootItemEventArgs(npc) {
                LootItemType = (ItemType)itemId,
                LootItemStackSize = stack,
                LootItemPrefix = (ItemPrefix)prefix
            };
            NpcDroppingLootItem?.Invoke(this, args);

            if (args.Handled) {
                return OTAPI.HookResult.Cancel;
            }

            /*
             * Handle item spawning in this method. This enables us to provide a reference to an IItem in
             * NpcDroppedLootItemEventArgs, but adds a dependency on IItemService.
             */
            var item = _itemService.SpawnItem(args.LootItemType, new Vector2(x + width / 2, y + height / 2),
                                              args.LootItemStackSize, args.LootItemPrefix);
            var args2 = new NpcDroppedLootItemEventArgs(npc, item);
            NpcDroppedLootItem?.Invoke(this, args2);

            return OTAPI.HookResult.Cancel;
        }

        private void KilledHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new KilledNpcEventArgs(npc);
            KilledNpc?.Invoke(this, args);
        }
    }
}
