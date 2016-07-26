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
		/// <summary>
		/// A value indicating whether the service has been disposed. Used to ignore multiple
		/// <see cref="Dispose(bool)"/> calls.
		/// </summary>
		private bool _disposed;

		/// <summary>
		/// The backing array of <see cref="IItem"/>s. Lazily updated with items from the Terraria item array.
		/// </summary>
		private readonly IItem[] _items;

		/// <summary>
		/// Occurs after an <see cref="IItem"/> has had its defaults set.
		/// </summary>
		public event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs when an <see cref="IItem"/> is having its defaults set.
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
		/// Creates a new <see cref="IItem"/> with the specified type ID, optionally with custom stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting instantiated <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was out of range, <paramref name="stackSize"/> was negative, or
		/// <paramref name="prefix"/> was too large.
		/// </exception>
		public IItem Create(int type, int stackSize = 1, byte prefix = 0)
		{
			if (type < -48 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}
			if (stackSize < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(stackSize));
			}
			if (prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}
			
			var terrariaItem = new Terraria.Item();
			terrariaItem.netDefaults(type);
			terrariaItem.stack = stackSize;
			terrariaItem.prefix = prefix;
			return new Item(terrariaItem);
		}

		/// <summary>
		/// Finds all <see cref="IItem"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/>s.</returns>
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
		/// Spawns a new <see cref="IItem"/> with the specified type ID at a position in the world, optionally with
		/// custom stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting spawned <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was out of range, <paramref name="stackSize"/> was negative, or
		/// <paramref name="prefix"/> was too large.
		/// </exception>
		public IItem Spawn(int type, Vector2 position, int stackSize = 1, byte prefix = 0)
		{
			if (type < -48 || type > Terraria.Main.maxItemTypes)
			{
				throw new ArgumentOutOfRangeException(nameof(type));
			}
			if (stackSize < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(stackSize));
			}
			if (prefix > Terraria.Item.maxPrefixes)
			{
				throw new ArgumentOutOfRangeException(nameof(prefix));
			}

			int index = Terraria.Item.NewItem((int) position.X, (int) position.Y, 0, 0, type, stackSize, pfix: prefix);
			var item = new Item(Terraria.Main.item[index]);
			_items[index] = item;
			return item;
		}

		/// <summary>
		/// Disposes the service and its unmanaged resources, optionally disposing its managed resources.
		/// </summary>
		/// <param name="disposing">
		/// true if called from a managed disposal, and *both* unmanaged and managed resources must be freed. false
		/// if called from a finalizer, and *only* unmanaged resources may be freed.
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

		/// <summary>
		/// Invokes the <see cref="ItemSetDefaults"/> event.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item that had its defaults set.</param>
		/// <param name="type">The Terraria item's type ID. Unused.</param>
		/// <param name="noMaterialCheck">
		/// A value indicating whether to determine whether the Terraria item is a material. Unused.
		/// </param>
		private void InvokeItemSetDefaults(Terraria.Item terrariaItem, ref int type, ref bool noMaterialCheck)
		{
			var item = new Item(terrariaItem);
			var args = new ItemSetDefaultsEventArgs(item);
			ItemSetDefaults?.Invoke(this, args);
		}

		/// <summary>
		/// Invokes the <see cref="ItemSettingDefaults"/> event.
		/// </summary>
		/// <param name="terrariaItem">The Terraria item that is having its defaults set.</param>
		/// <param name="type">The Terraria item's type ID. This will update the normal server's type ID.</param>
		/// <param name="noMaterialCheck">
		/// A value indicating whether to determine whether the Terraria item is a material. Unused.
		/// </param>
		/// <returns>A value indicating whether to continue or cancel normal server code.</returns>
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
