using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Items.Events;
using OTAPI.Core;

namespace Orion.Items
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
		public IItem CreateItem(ItemType type, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None)
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
		public IEnumerable<IItem> FindItems(Predicate<IItem> predicate = null)
		{
			var items = new List<IItem>();
			for (var i = 0; i < _items.Length; ++i)
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
		public IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None)
		{
			int index = Terraria.Item.NewItem(
				(int)position.X, (int)position.Y, 0, 0, (int)type, stackSize, pfix: (int)prefix);
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

		private HookResult InvokeItemSettingDefaults(
			Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new ItemSettingDefaultsEventArgs(item, (ItemType)type);
			ItemSettingDefaults?.Invoke(this, args);
			type = (int)args.Type;
			return args.Handled ? HookResult.Cancel : HookResult.Continue;
		}
	}
}
