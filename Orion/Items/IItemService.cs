using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Items.Events;

namespace Orion.Items
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IItem"/> instances.
	/// </summary>
	public interface IItemService : IService
	{
		/// <summary>
		/// Occurs after an <see cref="IItem"/> instance had its defaults set.
		/// </summary>
		event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs when an <see cref="IItem"/> instance is having its defaults set.
		/// </summary>
		event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

		/// <summary>
		/// Creates a new <see cref="IItem"/> instance using the specified <see cref="ItemType"/>, optionally with the
		/// specified stack size and <see cref="ItemPrefix"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The <see cref="ItemPrefix"/>.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="stackSize"/> was negative.</exception>
		IItem CreateItem(ItemType type, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);

		/// <summary>
		/// Returns all <see cref="IItem"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/> instances.</returns>
		IEnumerable<IItem> FindItems(Predicate<IItem> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="IItem"/> instance using the specified <see cref="ItemType"/> at the position in the
		/// world, optionally with the specified stack size and <see cref="ItemPrefix"/>.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/>.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The <see cref="ItemPrefix"/>.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="stackSize"/> was negative.</exception>
		IItem SpawnItem(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);
	}
}
