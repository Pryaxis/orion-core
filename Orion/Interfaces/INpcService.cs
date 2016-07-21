using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Events.Npc;
using Orion.Framework;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: INpcService
	/// 
	/// Provides a mechanism for managing <see cref="INpc"/>s.
	/// </summary>
	public interface INpcService : IService
	{
		/// <summary>
		/// Occurs after an <see cref="INpc"/> has been killed.
		/// </summary>
		event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs before an <see cref="INpc"/> teleports to a new position.
		/// </summary>
		event EventHandler<NpcTeleportingEventArgs> NpcTeleporting;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> transforms from one type to another.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Finds all <see cref="INpc"/>s in the world matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with.</param>
		/// <returns>An enumerable collection of <see cref="INpc"/>s matching the predicate.</returns>
		IEnumerable<INpc> Find(Predicate<INpc> predicate);

		/// <summary>
		/// Gets all <see cref="INpc"/>s in the world.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="INpc"/>s.</returns>
		IEnumerable<INpc> GetAll();

		/// <summary>
		/// Spawns a new <see cref="INpc"/> with the specified type ID at a position in the world, optionally with
		/// custom HP values.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position.</param>
		/// <param name="hp">The custom HP, or null for the default.</param>
		/// <param name="maxHP">The custom maximum HP, or null for the default.</param>
		/// <returns>The resulting spawned <see cref="INpc"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is too small or large, or <paramref name="hp"/> or <paramref name="maxHP"/> are
		/// negative.
		/// </exception>
		INpc Spawn(int type, Vector2 position, int? hp = null, int? maxHP = null);
	}
}
