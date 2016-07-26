using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Events.Npc;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="INpc"/>s.
	/// </summary>
	public interface INpcService : IService
	{
		/// <summary>
		/// Occurs after an <see cref="INpc"/> has dropped loot.
		/// </summary>
		event EventHandler<NpcDroppedLootEventArgs> NpcDroppedLoot;

		/// <summary>
		/// Occurs when an <see cref="INpc"/> drops loot.
		/// </summary>
		event EventHandler<NpcDroppingLootEventArgs> NpcDroppingLoot;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> has been killed.
		/// </summary>
		event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs when an <see cref="INpc"/> spawns in the world.
		/// </summary>
		event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> has transformed to another type.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Finds all <see cref="INpc"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="INpc"/>s.</returns>
		IEnumerable<INpc> Find(Predicate<INpc> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="INpc"/> with the specified type ID at a position in the world.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <returns>The resulting spawned <see cref="INpc"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> was out of range.</exception>
		INpc Spawn(int type, Vector2 position);
	}
}
