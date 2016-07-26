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
	/// Manages <see cref="IItem"/>s with a backing array, retrieving information from the Terraria item array.
	/// </summary>
	[Service("Item Service", Author = "Nyx Studios")]
	public class ItemService : ServiceBase, IItemService
	{
		private bool _disposed;
		private readonly IItem[] _items;

		/// <summary>
		/// Occurs after an item has had its defaults set.
		/// </summary>
		public event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs when an item is having its defaults set.
		/// </summary>
		public event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemService"/> class.
		/// </summary>
		/// <param name="orion">The parent <see cref="Orion"/> instance.</param>
		public ItemService(Orion orion) : base(orion)
		{
			_items = new IItem[Terraria.Main.item.Length];
			Hooks.Item.PostSetDefaultsById = InvokeItemSetDefaults;
			Hooks.Item.PreSetDefaultsById = InvokeItemSettingDefaults;
		}

		/// <summary>
		/// Creates a new <see cref="IItem"/> with the specified type, optionally with custom stack size and prefix.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was an invalid type, <paramref name="stackSize"/> was an invalid stack size, or
		/// <paramref name="prefix"/> was an invalid prefix.
		/// </exception>
		public IItem Create(int type, int stackSize = 1, int prefix = 0)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);
			item.SetDefaults(type);
			item.StackSize = stackSize;
			item.SetPrefix(prefix);
			return item;
		}

		/// <summary>
		/// Finds all <see cref="IItem"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/>s.</returns>
		/// <remarks>
		/// The <see cref="IItem"/>s are cached in an array. Calling this method multiple times will result in the same
		/// <see cref="IItem"/> references as long as the Terraria item array is not updated.
		/// </remarks>
		public IEnumerable<IItem> Find(Predicate<IItem> predicate = null)
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
			return items.Where(i => i.WrappedItem.active && (predicate?.Invoke(i) ?? true));
		}

		/// <summary>
		/// Spawns a new <see cref="IItem"/> with the specified type and position in the world, optionally with custom
		/// stack size and prefix.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was an invalid type, <paramref name="stackSize"/> was an invalid stack size, or
		/// <paramref name="prefix"/> was an invalid prefix.
		/// </exception>
		public IItem Spawn(int type, Vector2 position, int stackSize = 1, int prefix = 0)
		{
			if (type < 0 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}
			if (stackSize < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(stackSize));
			}
			if (prefix < 0 || prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}

			int index = Terraria.Item.NewItem((int) position.X, (int) position.Y, 0, 0, type, stackSize, pfix: prefix);
			var item = new Item(Terraria.Main.item[index]);
			_items[index] = item;
			return item;
		}

		/// <summary>
		/// Overrides <see cref="ServiceBase.Dispose"/>.
		/// </summary>
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

		private void InvokeItemSetDefaults(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new ItemSetDefaultsEventArgs(item);
			ItemSetDefaults?.Invoke(this, args);
		}

		private HookResult InvokeItemSettingDefaults(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new ItemSettingDefaultsEventArgs(item, type);
			ItemSettingDefaults?.Invoke(this, args);
			type = args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
