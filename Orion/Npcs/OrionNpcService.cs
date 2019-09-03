using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Ninject;
using Orion.Framework;
using Orion.Items;
using Orion.Npcs.Events;
using Orion.Players;
using OTAPI;

namespace Orion.Npcs {
    /// <summary>
    /// Orion's implementation of <see cref="INpcService"/>.
    /// </summary>
    internal sealed class OrionNpcService : OrionService, INpcService {
        private readonly IItemService _itemService;
        private readonly IPlayerService _playerService;
        private readonly INpc[] _npcs;

        public override string Author => "Pryaxis";
        public override string Name => "Orion NPC Service";

        public int Count => Terraria.Main.maxNPCs;
        
        public INpc this[int index] {
            get {
                if (index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (_npcs[index]?.WrappedNpc != Terraria.Main.npc[index]) {
                    _npcs[index] = new OrionNpc(Terraria.Main.npc[index]);
                }

                var npc = _npcs[index];
                Debug.Assert(npc != null, $"{nameof(npc)} should not be null.");
                Debug.Assert(npc.WrappedNpc != null, $"{nameof(npc.WrappedNpc)} should not be null.");
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

        public event EventHandler<NpcSpawningEventArgs> NpcSpawning;
        public event EventHandler<NpcSpawnedEventArgs> NpcSpawned;
        public event EventHandler<NpcSettingDefaultsEventArgs> NpcSettingDefaults;
        public event EventHandler<NpcSetDefaultsEventArgs> NpcSetDefaults;
        public event EventHandler<NpcUpdatingEventArgs> NpcUpdating;
        public event EventHandler<NpcUpdatedEventArgs> NpcUpdated;
        public event EventHandler<NpcUpdatingEventArgs> NpcUpdatingAi;
        public event EventHandler<NpcUpdatedEventArgs> NpcUpdatedAi;
        public event EventHandler<NpcStrikingEventArgs> NpcStriking;
        public event EventHandler<NpcStruckEventArgs> NpcStruck;
        public event EventHandler<NpcTransformingEventArgs> NpcTransforming;
        public event EventHandler<NpcTransformedEventArgs> NpcTransformed;
        public event EventHandler<NpcDroppingLootItemEventArgs> NpcDroppingLootItem;
        public event EventHandler<NpcDroppedLootItemEventArgs> NpcDroppedLootItem;
        public event EventHandler<NpcKilledEventArgs> NpcKilled;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionNpcService"/> class with the specified item and player
        /// services.
        /// </summary>
        /// <param name="itemService">The item service.</param>
        /// <param name="playerService">The player service.</param>
        [Inject]
        public OrionNpcService(IItemService itemService, IPlayerService playerService) {
            Debug.Assert(itemService != null, $"{nameof(itemService)} should not be null.");
            
            _itemService = itemService;
            _playerService = playerService;
            _npcs = new INpc[Terraria.Main.maxNPCs];

            Hooks.Npc.Create = CreateHandler;
            Hooks.Npc.Spawn = SpawnHandler;
            Hooks.Npc.PreSetDefaultsById = PreSetDefaultsByIdHandler;
            Hooks.Npc.PostSetDefaultsById = PostSetDefaultsByIdHandler;
            Hooks.Npc.PreUpdate = PreUpdateHandler;
            Hooks.Npc.PreAI = PreAiHandler;
            Hooks.Npc.PostAI = PostAiHandler;
            Hooks.Npc.PostUpdate = PostUpdateHandler;
            Hooks.Npc.Strike = StrikeHandler;
            Hooks.Npc.PreTransform = PreTransformHandler;
            Hooks.Npc.PostTransform = PostTransformHandler;
            Hooks.Npc.PreDropLoot = PreDropLootHandler;
            Hooks.Npc.Killed = KilledHandler;
        }

        public IEnumerator<INpc> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public INpc SpawnNpc(NpcType type, Vector2 position) {
            var npcIndex = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)type);
            if (npcIndex < 0 || npcIndex >= Terraria.Main.maxNPCs) {
                return null;
            }

            return this[npcIndex];
        }


        private Terraria.NPC CreateHandler(ref int npcIndex, ref int x, ref int y, ref int type, ref int start, ref float ai0,
                                  ref float ai1, ref float ai2, ref float ai3, ref int target) {
            var args = new NpcSpawningEventArgs {
                NpcType = (NpcType)type,
                Position = new Vector2(x, y),
                NpcTarget = target >= 0 && target < _playerService.Count ? _playerService[target] : null
            };
            args.AiValues[0] = ai0;
            args.AiValues[1] = ai1;
            args.AiValues[2] = ai2;
            args.AiValues[3] = ai3;
            NpcSpawning?.Invoke(this, args);

            var terrariaNpc = new Terraria.NPC();
            if (args.Handled) {
                npcIndex = Terraria.Main.maxNPCs;
            } else {
                terrariaNpc.SetDefaults((int)args.NpcType);
            }

            return terrariaNpc;
        }

        private HookResult SpawnHandler(ref int npcIndex) {
            if (npcIndex < 0 || npcIndex >= Terraria.Main.maxNPCs) {
                return HookResult.Continue;
            }

            var npc = this[npcIndex];
            var args = new NpcSpawnedEventArgs(npc);
            NpcSpawned?.Invoke(this, args);

            return HookResult.Continue;
        }

        private HookResult PreSetDefaultsByIdHandler(Terraria.NPC terrariaNpc, ref int type, ref float scaleOverride) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcSettingDefaultsEventArgs(npc, (NpcType)type);
            NpcSettingDefaults?.Invoke(this, args);

            type = (int)args.Type;
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSetDefaultsByIdHandler(Terraria.NPC terrariaNpc, int type, float scaleOverride) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcSetDefaultsEventArgs(npc);
            NpcSetDefaults?.Invoke(this, args);
        }

        private HookResult PreUpdateHandler(Terraria.NPC terrariaNpc, ref int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = this[npcIndex];
            var args = new NpcUpdatingEventArgs(npc);
            NpcUpdating?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreAiHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcUpdatingEventArgs(npc);
            NpcUpdatingAi?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostAiHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcUpdatedEventArgs(npc);
            NpcUpdatedAi?.Invoke(this, args);
        }

        private void PostUpdateHandler(Terraria.NPC terrariaNpc, int npcIndex) {
            Debug.Assert(npcIndex >= 0 && npcIndex < Count, $"{nameof(npcIndex)} should be a valid index.");

            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcUpdatedEventArgs(npc);
            NpcUpdated?.Invoke(this, args);
        }

        private HookResult StrikeHandler(Terraria.NPC terrariaNpc, ref double cancelResult, ref int damage, ref float knockback,
                                         ref int hitDirection, ref bool crit, ref bool noEffect, ref bool fromNet,
                                         Terraria.Entity entity) {
            var npc = new OrionNpc(terrariaNpc);
            IPlayer strikePlayer = null;
            if (entity is Terraria.Player terrariaPlayer) {
                strikePlayer = _playerService[terrariaPlayer.whoAmI];
            }

            var args = new NpcStrikingEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                StrikePlayer = strikePlayer,
            };
            NpcStriking?.Invoke(this, args);

            if (args.Handled) {
                return HookResult.Cancel;
            }

            damage = args.Damage;
            knockback = args.Knockback;
            hitDirection = args.HitDirection;
            crit = args.IsCriticalHit;

            var args2 = new NpcStruckEventArgs(npc) {
                Damage = damage,
                Knockback = knockback,
                HitDirection = hitDirection,
                IsCriticalHit = crit,
                StrikePlayer = strikePlayer,
            };
            NpcStruck?.Invoke(this, args2);
            return HookResult.Continue;
        }

        private HookResult PreTransformHandler(Terraria.NPC terrariaNpc, ref int newType) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformingEventArgs(npc);
            NpcTransforming?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostTransformHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcTransformedEventArgs(npc);
            NpcTransformed?.Invoke(this, args);
        }

        private HookResult PreDropLootHandler(Terraria.NPC terrariaNpc, ref int itemId, ref int x, ref int y, ref int width,
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
                return HookResult.Cancel;
            }

            var item = _itemService.SpawnItem(args.LootItemType, new Vector2(x + width / 2, y + height / 2),
                                              args.LootItemStackSize, args.LootItemPrefix);
            var args2 = new NpcDroppedLootItemEventArgs(npc, item);
            NpcDroppedLootItem?.Invoke(this, args2);

            return HookResult.Cancel;
        }

        private void KilledHandler(Terraria.NPC terrariaNpc) {
            var npc = new OrionNpc(terrariaNpc);
            var args = new NpcKilledEventArgs(npc);
            NpcKilled?.Invoke(this, args);
        }
    }
}
