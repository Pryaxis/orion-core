using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using OTAPI.Core;
using Orion.Framework;
using Orion.Items;
using Orion.Npcs.Events;

namespace Orion.Npcs
{
	/// <summary>
	/// Manages <see cref="INpc"/> instances.
	/// </summary>
	[Service("Npc Service", Author = "Nyx Studios")]
	public class NpcService : SharedService, INpcService
	{
		private readonly INpc[] _npcs;
		private ItemService _itemService;

		/// <inheritdoc/>
		public int BaseNpcSpawningLimit
		{
			get { return Terraria.NPC.defaultMaxSpawns; }
			set { Terraria.NPC.defaultMaxSpawns = value; }
		}

		/// <inheritdoc/>
		public int BaseNpcSpawningRate
		{
			get { return Terraria.NPC.defaultSpawnRate; }
			set { Terraria.NPC.defaultSpawnRate = value; }
		}

		/// <inheritdoc/>
		public event EventHandler<NpcDroppedLootEventArgs> NpcDroppedLoot;

		/// <inheritdoc/>
		public event EventHandler<NpcDroppingLootEventArgs> NpcDroppingLoot;

		/// <inheritdoc/>
		public event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <inheritdoc/>
		public event EventHandler<NpcSetDefaultsEventArgs> NpcSetDefaults;

		/// <inheritdoc/>
		public event EventHandler<NpcSettingDefaultsEventArgs> NpcSettingDefaults;

		/// <inheritdoc/>
		public event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <inheritdoc/>
		public event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <inheritdoc/>
		public event EventHandler<NpcStrikingEventArgs> NpcStriking;

		/// <inheritdoc/>
		public event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <inheritdoc/>
		public event EventHandler<NpcTransformingEventArgs> NpcTransforming;

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		/// <param name="itemService">The <see cref="ItemService"/> instance.</param>
		public NpcService(Orion orion, ItemService itemService) : base(orion)
		{
			_npcs = new INpc[Terraria.Main.npc.Length];
			_itemService = itemService;
			Hooks.Npc.PostDropLoot = InvokeNpcDroppedLoot;
			Hooks.Npc.PreDropLoot = InvokeNpcDroppingLoot;
			Hooks.Npc.Killed = InvokeNpcKilled;
			Hooks.Npc.PostSetDefaultsById = InvokeNpcSetDefaults;
			Hooks.Npc.PreSetDefaultsById = InvokeNpcSettingDefaults;
			Hooks.Npc.Create = InvokeNpcSpawned;
			Hooks.Npc.Spawn = InvokeNpcSpawning;
			Hooks.Npc.Strike = InvokeNpcStriking;
			Hooks.Npc.PostTransform = InvokeNpcTransformed;
			Hooks.Npc.PreTransform = InvokeNpcTransforming;
		}

		/// <inheritdoc/>
		public INpc SpawnNpc(NpcType type, Vector2 position)
		{
			int index = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)type);
			var npc = new Npc(Terraria.Main.npc[index]);
			_npcs[index] = npc;
			npc.SetDefaults(type);
			npc.Position = position;
			return npc;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="INpc"/> instances are cached in an array. Calling this method multiple times will result
		/// in the same <see cref="INpc"/> instances as long as Terraria's npc array remains unchanged.
		/// </remarks>
		public IEnumerable<INpc> FindNpcs(Predicate<INpc> predicate = null)
		{
			var npcs = new List<INpc>();
			for (int i = 0; i < Terraria.Main.npc.Length; i++)
			{
				if (_npcs[i]?.WrappedNpc != Terraria.Main.npc[i])
				{
					_npcs[i] = new Npc(Terraria.Main.npc[i]);
				}
				npcs.Add(_npcs[i]);
			}
			return npcs.Where(n => n.WrappedNpc.active && (predicate?.Invoke(n) ?? true));
		}

		private void InvokeNpcDroppedLoot(Terraria.NPC terrariaNpc, int x, int y, int width, int height, int type, int stack,
			bool noBroadcast, int prefix, bool noGrabDelay, bool reverseLookup)
		{
			var npc = new Npc(terrariaNpc);
			var item = _itemService.FindItems(i => (int)i.Type == type).FirstOrDefault();
			var args = new NpcDroppedLootEventArgs(npc, item);
			NpcDroppedLoot?.Invoke(this, args);
		}

		private HookResult InvokeNpcDroppingLoot(Terraria.NPC terrariaNpc, ref int itemId, ref int x, ref int y, ref int width,
			ref int height, ref int type, ref int stack, ref bool noBroadcast, ref int prefix, ref bool noGrabDelay, ref bool reverseLookup)
		{
			var npc = new Npc(terrariaNpc);
			var item = _itemService.CreateItem((ItemType)type, stack, (ItemPrefix)prefix);
			var args = new NpcDroppingLootEventArgs(npc, item);
			NpcDroppingLoot?.Invoke(this, args);
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private void InvokeNpcKilled(Terraria.NPC terrariaNpc)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcKilledEventArgs(npc);
			NpcKilled?.Invoke(this, args);
		}

		private void InvokeNpcSetDefaults(Terraria.NPC terrariaNpc, int type, float scaleOverride)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcSetDefaultsEventArgs(npc);
			NpcSetDefaults?.Invoke(this, args);
		}

		private HookResult InvokeNpcSettingDefaults(Terraria.NPC terrariaNpc, ref int type, ref float scaleOverride)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcSettingDefaultsEventArgs(npc, (NpcType)type);
			NpcSettingDefaults?.Invoke(this, args);
			type = (int)args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private Terraria.NPC InvokeNpcSpawned(ref int index, ref int x, ref int y, ref int type, ref int start, ref float ai0,
			ref float ai1, ref float ai2, ref float ai3, ref int target)
		{
			var terrariaNpc = Terraria.Main.npc[index];
			var npc = new Npc(terrariaNpc);
			var args = new NpcSpawnedEventArgs(npc);
			NpcSpawned?.Invoke(this, args);
			return terrariaNpc;
		}

		private HookResult InvokeNpcSpawning(ref int index)
		{
			var npc = new Npc(Terraria.Main.npc[index]);
			var args = new NpcSpawningEventArgs(npc, index);
			NpcSpawning?.Invoke(this, args);
			index = args.Index;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private HookResult InvokeNpcStriking(Terraria.NPC terrariaNpc, ref int cancelResult, ref int damage, ref float knockback,
			ref int hitDirection, ref bool crit, ref bool noEffect, ref bool fromNet)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcStrikingEventArgs(npc, damage, knockback, hitDirection, crit, noEffect, fromNet);
			NpcStriking?.Invoke(this, args);
			damage = args.Damage;
			knockback = args.Knockback;
			hitDirection = args.HitDirection;
			crit = args.Critical;
			noEffect = args.NoEffect;
			fromNet = args.FromNet;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private void InvokeNpcTransformed(Terraria.NPC terrariaNpc)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcTransformedEventArgs(npc);
			NpcTransformed?.Invoke(this, args);
		}

		private HookResult InvokeNpcTransforming(Terraria.NPC terrariaNpc, ref int newType)
		{
			var npc = new Npc(terrariaNpc);
			var args = new NpcTransformingEventArgs(npc, (NpcType)newType);
			NpcTransforming?.Invoke(this, args);
			newType = (int)args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
