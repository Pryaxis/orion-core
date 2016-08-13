using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Framework;
using Orion.Npcs.Events;

namespace Orion.Npcs
{
	/// <summary>
	/// Provides a mechanism for managing NPCs.
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
		/// Occurs after an NPC has dropped loot.
		/// </summary>
		event EventHandler<NpcDroppedLootEventArgs> NpcDroppedLoot;

		/// <summary>
		/// Occurs when an NPC is dropping loot.
		/// </summary>
		event EventHandler<NpcDroppingLootEventArgs> NpcDroppingLoot;

		/// <summary>
		/// Occurs after an NPC was killed.
		/// </summary>
		event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <summary>
		/// Occurs after an NPC has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs when an NPC is spawning in the world.
		/// </summary>
		event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <summary>
		/// Occurs after an NPC has transformed to another type.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Occurs when an NPC is transforming to another type.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransforming;

		/// <summary>
		/// Returns all NPCs in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of NPCs.</returns>
		IEnumerable<INpc> FindNpcs(Predicate<INpc> predicate = null);

		/// <summary>
		/// Spawns a new NPC with the specified type ID at a position in the world.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <returns>The resulting NPC.</returns>
		INpc SpawnNpc(int type, Vector2 position);
	}
}
