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
		/// Creates a new <see cref="IItem"/> instance using the specified <see cref="ItemType"/> instance, optionally
		/// with custom stack size and prefix.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/> instance.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="stackSize"/> was negative.</exception>
		IItem Create(ItemType type, int stackSize = 1, ItemPrefix? prefix = null);

		/// <summary>
		/// Finds all <see cref="IItem"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/> instances.</returns>
		IEnumerable<IItem> Find(Predicate<IItem> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="IItem"/> instance using the specified <see cref="ItemType"/> instance and position
		/// in the world, optionally with custom stack size and prefix.
		/// </summary>
		/// <param name="type">The <see cref="ItemType"/> instance.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="stackSize"/> was negative.</exception>
		IItem Spawn(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix? prefix = null);
	}
}
