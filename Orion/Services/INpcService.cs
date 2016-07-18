using Microsoft.Xna.Framework;
using Orion.Events.Npc;
using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Interfaces;

namespace Orion.Services
{
	/// <summary>
	/// Service definition: INpcService
	/// 
	/// Provides a mechanism for managing NPCs.
	/// </summary>
	public interface INpcService : IService
	{
		/// <summary>
		/// Occurs after an NPC has died.
		/// </summary>
		event EventHandler<NpcDiedEventArgs> NpcDied;

		/// <summary>
		/// Occurs after an NPC has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs before an NPC teleports to a new position.
		/// </summary>
		event EventHandler<NpcTeleportingEventArgs> NpcTeleporting;

		/// <summary>
		/// Occurs after an NPC transforms from one type to another.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Finds all NPCs in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of NPCs matching the predicate.</returns>
		IEnumerable<INpc> Find(Predicate<INpc> predicate);

		/// <summary>
		/// Gets all NPCs in the world.
		/// </summary>
		/// <returns>An enumerable collection of NPCs.</returns>
		IEnumerable<INpc> GetAll();

		/// <summary>
		/// Spawns a new NPC with the specified type ID at a position in the world, optionally with custom HP values.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position.</param>
		/// <param name="hp">The custom HP, or null for the default.</param>
		/// <param name="maxHP">The custom maximum HP, or null for the default.</param>
		/// <returns>The resulting spawned NPC.</returns>
		INpc Spawn(int type, Vector2 position, int? hp = null, int? maxHP = null);

		/// <summary>
		/// Kills an NPC.
		/// </summary>
		/// <param name="npc">The NPC to be killed.</param>
		void Kill(INpc npc);
	}
}
