using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Interfaces;

namespace Orion.Services
{
	/// <summary>
	/// Service definition: IItemService
	/// 
	/// Provides a mechanism for managing items.
	/// </summary>
	public interface IItemService : IService
	{
		/// <summary>
		/// Instantiates a new item with the specified type ID, optionally with stack size and prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="stack">The stack size, or null for the maximum.</param>
		/// <param name="prefix">The prefix, or 0 for none.</param>
		/// <returns>The resulting instantiated item.</returns>
		IItem Create(int type, int? stack = null, byte prefix = 0);

		/// <summary>
		/// Finds all items in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of items matching the predicate.</returns>
		IEnumerable<IItem> Find(Predicate<IItem> predicate);

		/// <summary>
		/// Gets all items in the world.
		/// </summary>
		/// <returns>An enumerable collection of items.</returns>
		IEnumerable<IItem> GetAll();

		/// <summary>
		/// Spawns a new item with the specified type ID at a position in the world, optionally with stack size and
		/// prefix.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position.</param>
		/// <param name="stack">The stack size, or null for the maximum.</param>
		/// <param name="prefix">The prefix, or 0 for none.</param>
		/// <returns>The resulting spawned item.</returns>
		IItem Spawn(int type, Vector2 position, int? stack = null, byte prefix = 0);
	}
}
