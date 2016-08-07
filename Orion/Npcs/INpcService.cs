using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Npcs.Events;

namespace Orion.Npcs
{
	/// <summary>
	/// Provides a mechanism for managing <see cref="INpc"/> instances.
	/// </summary>
	public interface INpcService : IService
	{
		/// <summary>
		/// Gets or sets the base NPC spawning limit.
		/// </summary>
		int BaseNpcSpawningLimit { get; set; }

		/// <summary>
		/// Gets or sets the base NPC spawning rate.
		/// </summary>
		int BaseNpcSpawningRate { get; set; }

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance has dropped loot.
		/// </summary>
		event EventHandler<NpcDroppedLootEventArgs> NpcDroppedLoot;

		/// <summary>
		/// Occurs when an <see cref="INpc"/> instance is dropping loot.
		/// </summary>
		event EventHandler<NpcDroppingLootEventArgs> NpcDroppingLoot;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance was killed.
		/// </summary>
		event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs when an <see cref="INpc"/> instance is spawning in the world.
		/// </summary>
		event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance has transformed to another type.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Returns all <see cref="INpc"/> instances in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of <see cref="INpc"/> instances.</returns>
		IEnumerable<INpc> FindNpcs(Predicate<INpc> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="INpc"/> instance with the specified type ID at a position in the world.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <returns>The resulting <see cref="INpc"/> instance.</returns>
		INpc SpawnNpc(int type, Vector2 position);
	}
}
