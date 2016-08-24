using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using OTAPI;
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
		private readonly ItemService _itemService;

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
			Hooks.Npc.Spawn = InvokeNpcSpawned;
			Hooks.Npc.Create = InvokeNpcSpawning;
			Hooks.Npc.Strike = InvokeNpcStriking;
			Hooks.Npc.PostTransform = InvokeNpcTransformed;
			Hooks.Npc.PreTransform = InvokeNpcTransforming;
		}

		/// <inheritdoc/>
		public INpc SpawnNpc(int type, Vector2 position)
		{
			int index = Terraria.NPC.NewNPC((int)position.X, (int)position.Y, (int)type);
			var npc = new Npc(Terraria.Main.npc[index]);
			_npcs[index] = npc;
			npc.SetDefaults((NpcType)type);
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
			var args = new NpcDroppedLootEventArgs(npc, type, x, y, width, height, stack, prefix);
			NpcDroppedLoot?.Invoke(this, args);
		}

		private HookResult InvokeNpcDroppingLoot(Terraria.NPC terrariaNpc, ref int itemId, ref int x, ref int y, ref int width,
			ref int height, ref int type, ref int stack, ref bool noBroadcast, ref int prefix, ref bool noGrabDelay, ref bool reverseLookup)
		{
			var npc = new Npc(terrariaNpc);
			var item = _itemService.CreateItem((ItemType)type, stack, (Prefix)prefix);
			var args = new NpcDroppingLootEventArgs(npc, item);
			NpcDroppingLoot?.Invoke(this, args);
			x = (int)args.Item.Position.X;
			y = (int)args.Item.Position.Y;
			width = args.Item.Width;
			height = args.Item.Height;
			type = (int)args.Item.Type;
			stack = args.Item.StackSize;
			prefix = (int)args.Item.Prefix;
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

		private HookResult InvokeNpcSpawned(ref int index)
		{
			var terrariaNpc = Terraria.Main.npc[index];
			var npc = new Npc(terrariaNpc);
			var spawnedArgs = new NpcSpawnedEventArgs(npc);
			NpcSpawned?.Invoke(this, spawnedArgs);
			return spawnedArgs.Handled ? HookResult.Cancel : HookResult.Continue;
		}

		private Terraria.NPC InvokeNpcSpawning(ref int index, ref int x, ref int y, ref int type, ref int start, ref float ai0,
			ref float ai1, ref float ai2, ref float ai3, ref int target)
		{
			var terrariaNpc = new Terraria.NPC();
			terrariaNpc.SetDefaults(type);
			var npc = new Npc(terrariaNpc);
			var args = new NpcSpawningEventArgs(npc, index);
			index = args.Index;
			type = (int)args.Type;
			NpcSpawning?.Invoke(this, args);
			return terrariaNpc;
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
