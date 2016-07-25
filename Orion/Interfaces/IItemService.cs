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
		/// Occurs after an <see cref="IItem"/> has its defaults set.
		/// </summary>
		event EventHandler<ItemSetDefaultsEventArgs> ItemSetDefaults;

		/// <summary>
		/// Occurs before an <see cref="IItem"/> has its defaults set.
		/// </summary>
		event EventHandler<ItemSettingDefaultsEventArgs> ItemSettingDefaults;

		/// <summary>
		/// Creates a new <see cref="IItem"/> with the specified type ID, optionally with stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="stack">The stack size.</param>
		/// <param name="prefix">The prefix, or 0 for none.</param>
		/// <returns>The resulting instantiated <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is out of range, <paramref name="stack"/> is negative, or <paramref name="prefix"/>
		/// is too large.
		/// </exception>
		IItem Create(int type, int stack = 1, byte prefix = 0);

		/// <summary>
		/// Finds all <see cref="IItem"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with, or null for none.</param>
		/// <returns>An enumerable collection of <see cref="IItem"/>s.</returns>
		IEnumerable<IItem> Find(Predicate<IItem> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="IItem"/> with the specified type ID at a position in the world, optionally with
		/// stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="stack">The stack size.</param>
		/// <param name="prefix">The prefix.</param>
		/// <returns>The resulting spawned <see cref="IItem"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is out of range, <paramref name="stack"/> is negative, or <paramref name="prefix"/>
		/// is too large.
		/// </exception>
		IItem Spawn(int type, Vector2 position, int stack = 1, byte prefix = 0);
	}
}
