using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Events.Item;
using Orion.Framework;

namespace Orion.Entities.Item
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
		/// Creates a new <see cref="IItem"/> instance with the specified type, optionally with custom stack size and
		/// prefix.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was an invalid item type, <paramref name="stackSize"/> was negative, or
		/// <paramref name="prefix"/> was an invalid item prefix.
		/// </exception>
		IItem Create(int type, int stackSize = 1, int prefix = 0);

		/// <summary>
		/// Finds all <see cref="IItem"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/> instances.</returns>
		IEnumerable<IItem> Find(Predicate<IItem> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="IItem"/> instance with the specified type and position in the world, optionally
		/// with custom stack size and prefix.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stackSize">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting <see cref="IItem"/> instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> was an invalid item type, <paramref name="stackSize"/> was negative, or
		/// <paramref name="prefix"/> was an invalid item prefix.
		/// </exception>
		IItem Spawn(int type, Vector2 position, int stackSize = 1, int prefix = 0);
	}
}
