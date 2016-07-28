using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Orion.Events.Item;
using Orion.Framework;
using OTAPI.Core;

namespace Orion.Entities.Item
{
	/// <summary>
	/// Manages <see cref="IItem"/> instances.
	/// </summary>
	[Service("Item Service", Author = "Nyx Studios")]
	public class ItemService : ServiceBase, IItemService
	{
		private readonly IItem[] _items;
		private bool _disposed;

		/// <inheritdoc/>
		public event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public IItem Create(int type, int stackSize = 1, int prefix = 0)
		{
			var terrariaItem = new Terraria.Item();
			var item = new Item(terrariaItem);
			item.SetDefaults(type);
			item.SetPrefix(prefix);
			item.StackSize = stackSize;
			return item;
		}

		/// <inheritdoc/>
		/// <remarks>
		/// The <see cref="IItem"/> instances are cached in an array. Calling this method multiple times will result
		/// in the same <see cref="IItem"/> instances as long as Terraria's item array remains unchanged.
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

		/// <inheritdoc/>
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

			int index = Terraria.Item.NewItem((int)position.X, (int)position.Y, 0, 0, type, stackSize, pfix: prefix);
			var item = new Item(Terraria.Main.item[index]);
			_items[index] = item;
			return item;
		}

		/// <inheritdoc/>
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
