using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Events.Item;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="IItem"/>s.
	/// </summary>
	public interface IItemService : IService
	{
		/// <summary>
		/// Occurs after an <see cref="IItem"/> has had its defaults set.
		/// </summary>
		event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs when an <see cref="IItem"/> is having its defaults set.
		/// </summary>
		event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

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
		IItem Create(int type, int stackSize = 1, byte prefix = 0);

		/// <summary>
		/// Finds all <see cref="IItem"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/>s.</returns>
		IEnumerable<IItem> Find(Predicate<IItem> predicate = null);

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
		IItem Spawn(int type, Vector2 position, int stackSize = 1, byte prefix = 0);
	}
}
