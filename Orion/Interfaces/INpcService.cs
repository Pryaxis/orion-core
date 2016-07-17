using Microsoft.Xna.Framework;
using Orion.Events.Npc;
using System;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: INpcService
	/// 
	/// Provides a mechanism for managing NPCs in the Terraria world.
	/// </summary>
	public interface INpcService : IEntityService<Terraria.NPC>
	{
		/// <summary>
		/// Occurs after an NPC has spawned in the world.
		/// </summary>
		event EventHandler<NpcSpawnedEventArgs> NpcSpawned;

		/// <summary>
		/// Occurs after an NPC has died.
		/// </summary>
		event EventHandler<NpcDiedEventArgs> NpcDied;

		/// <summary>
		/// Occurs after an NPC transforms from one type to another.
		/// </summary>
		event EventHandler<NpcTransformedEventArgs> NpcTransformed;

		/// <summary>
		/// Occurs before an NPC teleports to a new position.
		/// </summary>
		event EventHandler<NpcTeleportingEventArgs> NpcTeleporting;

		/// <summary>
		/// Spawns a new NPC at the specified position, optionally with custom HP values.
		/// </summary>
		/// <param name="type">The type ID of the NPC to spawn.</param>
		/// <param name="position">The position to spawn the NPC at.</param>
		/// <param name="life">The HP value the new NPC will spawn with, or null for default.</param>
		/// <param name="maxLife">The maximum HP value the NPC will spawn with, or null for default.</param>
		/// <returns>The resulting spawned NPC.</returns>
		Terraria.NPC Spawn(int type, Vector2 position, int? life = null, int? maxLife = null);

		/// <summary>
		/// Kills an NPC.
		/// </summary>
		/// <param name="npc">The NPC to be killed.</param>
		void Kill(Terraria.NPC npc);
	}
}
