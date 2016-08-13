using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Items.Events;

namespace Orion.Items
{
	/// <summary>
	/// Provides a mechanism for managing items.
	/// </summary>
	public interface IItemService : ISharedService
	{
		/// <summary>
		/// Occurs after an item has had its defaults set.
		/// </summary>
		event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs when an item is having its defaults set.
		/// </summary>
		event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

		/// <summary>
		/// Creates a new item using the specified <see cref="ItemType"/>, optionally with the
		/// specified stack size and <see cref="Prefix"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The <see cref="Prefix"/>.</param>
		/// <returns>The resulting item.</returns>
		IItem CreateItem(ItemType type, int stackSize = 1, Prefix prefix = Prefix.None);

		/// <summary>
		/// Returns all items in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of items.</returns>
		IEnumerable<IItem> FindItems(Predicate<IItem> predicate = null);

		/// <summary>
		/// Spawns a new item using the specified <see cref="ItemType"/> at the position in the
		/// world, optionally with the specified stack size and <see cref="Prefix"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The <see cref="Prefix"/>.</param>
		/// <returns>The resulting item.</returns>
		IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1, Prefix prefix = Prefix.None);
	}
}
