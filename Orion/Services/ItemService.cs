using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Orion.Core;
using Orion.Events.Item;
using Orion.Framework;
using Orion.Interfaces;
using OTAPI.Core;

namespace Orion.Services
{
	/// <summary>
	/// Implements the <see cref="IItemService"/> functionality.
	/// </summary>
	[Service("Item Service", Author = "Nyx Studios")]
	public class ItemService : ServiceBase, IItemService
	{
		private bool _disposed;
		private readonly IItem[] _items;

		/// <summary>
		/// Occurs after an <see cref="IItem"/> has its defaults set.
		/// </summary>
		public event EventHandler<SetDefaultsEventArgs> SetDefaults;

		/// <summary>
		/// Occurs before an <see cref="IItem"/> has its defaults set.
		/// </summary>
		public event EventHandler<SettingDefaultsEventArgs> SettingDefaults;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public ItemService(Orion orion) : base(orion)
		{
			_items = new IItem[Terraria.Main.item.Length];
			Hooks.Item.PostSetDefaultsById = InvokeSetDefaults;
			Hooks.Item.PreSetDefaultsById = InvokeSettingDefaults;
		}

		/// <summary>
		/// Creates a new <see cref="IItem"/> with the specified type ID, optionally with stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="stack">The stack size.</param>
		/// <param name="prefix">The prefix, or 0 for none.</param>
		/// <returns>The resulting instantiated <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is too small or large, <paramref name="stack"/> is negative, or
		/// <paramref name="prefix"/> is too large.
		/// </exception>
		public IItem Create(int type, int stack = 1, byte prefix = 0)
		{
			if (type < -48 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}
			if (stack < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(stack));
			}
			if (prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}
			
			var terrariaItem = new Terraria.Item();
			terrariaItem.netDefaults(type);
			terrariaItem.stack = stack;
			terrariaItem.prefix = prefix;
			return new Item(terrariaItem);
		}

		/// <summary>
		/// Finds all <see cref="IItem"/>s in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/>s matching the predicate.</returns>
		public IEnumerable<IItem> Find(Predicate<IItem> predicate)
		{
			var items = new List<IItem>();
			for (int i = 0; i < _items.Length; ++i)
			{
				if (_items[i]?.WrappedItem != Terraria.Main.item[i])
				{
					_items[i] = new Item(Terraria.Main.item[i]);
				}
				items.Add(_items[i]);
			}
			return items.Where(i => i.WrappedItem.active && predicate(i));
		}

		/// <summary>
		/// Gets all <see cref="IItem"/>s in the world.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="IItem"/>s.</returns>
		public IEnumerable<IItem> GetAll()
		{
			var items = new List<IItem>();
			for (int i = 0; i < _items.Length; ++i)
			{
				if (_items[i]?.WrappedItem != Terraria.Main.item[i])
				{
					_items[i] = new Item(Terraria.Main.item[i]);
				}
				items.Add(_items[i]);
			}
			return items.Where(i => i.WrappedItem.active);
		}

		/// <summary>
		/// Spawns a new <see cref="IItem"/> with the specified type ID at a position in the world, optionally with
		/// custom stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stack">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting spawned <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is too small or large, <paramref name="stack"/> is negative, or
		/// <paramref name="prefix"/> is too large.
		/// </exception>
		public IItem Spawn(int type, Vector2 position, int stack = 1, byte prefix = 0)
		{
			if (type < -48 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}
			if (stack < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(stack));
			}
			if (prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}

			int index = Terraria.Item.NewItem((int) position.X, (int) position.Y, 0, 0, type, stack, pfix: prefix);
			var item = new Item(Terraria.Main.item[index]);
			_items[index] = item;
			return item;
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, if any, optionally disposing its managed resources, if
		/// any.
		/// </summary>
		/// <param name="disposing">
		/// true to dispose managed and unmanaged resources, false to only dispose unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Hooks.Item.PostSetDefaultsById = null;
					Hooks.Item.PreSetDefaultsById = null;
				}
				_disposed = true;
			}
			base.Dispose(disposing);
		}

		private void InvokeSetDefaults(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new SetDefaultsEventArgs(item, type);
			SetDefaults?.Invoke(this, args);
		}

		private HookResult InvokeSettingDefaults(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new SettingDefaultsEventArgs(item, type);
			SettingDefaults?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
