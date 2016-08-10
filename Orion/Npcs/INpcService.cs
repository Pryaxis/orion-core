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
	public interface INpcService : ISharedService
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
<<<<<<< cc33da6b11c8d846a6d3e46ac7c60f9e027bfc6e
		/// Occurs after an NPC has spawned in the world.
=======
		/// Occurs after an <see cref="INpc"/> instance had its defaults set.
		/// </summary>
		event EventHandler<NpcSetDefaultsEventArgs> NpcSetDefaults;

		/// <summary>
		/// Occurs when an <see cref="INpc"/> instance is having its defaults set.
		/// </summary>
		event EventHandler<NpcSettingDefaultsEventArgs> NpcSettingDefaults;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance has spawned in the world.
>>>>>>> Add INpcService implementation and some missing events
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs when an NPC is spawning in the world.
		/// </summary>
		event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <summary>
<<<<<<< cc33da6b11c8d846a6d3e46ac7c60f9e027bfc6e
		/// Occurs after an NPC has transformed to another type.
=======
		/// Occurs when an <see cref="INpc"/> instance has been hit.
		/// </summary>
		event EventHandler<NpcStrikingEventArgs> NpcStriking;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> instance has transformed to another type.
>>>>>>> Add INpcService implementation and some missing events
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
<<<<<<< cc33da6b11c8d846a6d3e46ac7c60f9e027bfc6e
		/// Occurs when an NPC is transforming to another type.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransforming;

		/// <summary>
		/// Returns all NPCs in the world, optionally matching a predicate.
=======
		/// Occurs when an <see cref="INpc"/> instance is transforming to another type.
		/// </summary>
		event EventHandler<NpcTransformingEventArgs> NpcTransforming;

		/// <summary>
		/// Returns all <see cref="INpc"/> instances in the world, optionally matching a predicate.
>>>>>>> Add INpcService implementation and some missing events
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>An enumerable collection of NPCs.</returns>
		IEnumerable<INpc> FindNpcs(Predicate<INpc> predicate = null);

		/// <summary>
		/// Spawns a new NPC with the specified type ID at a position in the world.
		/// </summary>
		/// <param name="type">The <see cref="NpcType"/>.</param>
		/// <param name="position">The position in the world.</param>
<<<<<<< 904f8c3ad633379923d86b6198b0c0d84fd61d79
		/// <returns>The resulting NPC.</returns>
		INpc SpawnNpc(int type, Vector2 position);
=======
		/// <returns>The resulting <see cref="INpc"/> instance.</returns>
		INpc SpawnNpc(NpcType type, Vector2 position);
>>>>>>> Add missing NpcService tests, fix SpawnNpc
	}
}
