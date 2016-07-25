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
		/// Occurs after an <see cref="INpc"/> has been killed.
		/// </summary>
		event EventHandler<NpcKilledEventArgs> NpcKilled;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> spawns in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs before an <see cref="INpc"/> spawns in the world.
		/// </summary>
		event EventHandler<NpcSpawningEventArgs> NpcSpawning;

		/// <summary>
		/// Occurs after an <see cref="INpc"/> transforms from one type to another.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Finds all <see cref="INpc"/>s in the world, optionally matching a predicate.
		/// </summary>
		/// <param name="predicate">The predicate to match with, or null for none.</param>
		/// <returns>An enumerable collection of <see cref="INpc"/>s.</returns>
		IEnumerable<INpc> Find(Predicate<INpc> predicate = null);

		/// <summary>
		/// Spawns a new <see cref="INpc"/> with the specified type ID at a position in the world, optionally with
		/// custom HP values.
		/// </summary>
		/// <param name="type">The type ID.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="hp">The custom HP, or null for the default.</param>
		/// <param name="maxHP">The custom maximum HP, or null for the default.</param>
		/// <returns>The resulting spawned <see cref="INpc"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="type"/> is out of range, or <paramref name="hp"/> or <paramref name="maxHP"/> are
		/// negative.
		/// </exception>
		INpc Spawn(int type, Vector2 position, int? hp = null, int? maxHP = null);
	}
}
