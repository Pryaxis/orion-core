using Orion.Events.NPC;
using Orion.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Interfaces
{
	/// <summary>
	/// Service definition: INPCService
	/// 
	/// Provides a mechanism for dealing with NPCs in the Terraria world.
	/// </summary>
	public interface INPCService : IService
	{
		/// <summary>
		/// Occurs after an NPC has spawned in the world.
		/// </summary>
		event EventHandler<NPCSpawnEventArgs> NPCSpawned;

		/// <summary>
		/// Occurs after an NPC dies in the world.
		/// </summary>
		event EventHandler<NPCDiedEventArgs> NPCDied;

		/// <summary>
		/// Occurs after an NPC transforms from one type to another.
		/// </summary>
		event EventHandler<NPCTransformEventArgs> NPCTransformed;

		/// <summary>
		/// Occurs before an NPC teleports to a new position.
		/// </summary>
		event EventHandler<NPCTeleportingEventArgs> NPCTeleporting;

		/// <summary>
		/// Finds all NPCs that match the specified predicate.
		/// </summary>
		/// <returns>
		/// An enumerable collection of NPCs that match the specified predicate.
		/// </returns>
		IEnumerable<Terraria.NPC> FindNPCs(Predicate<Terraria.NPC> predicate);

		/// <summary>
		/// Creates a new NPC with the NPC's default parameters, optionally with custom life values.
		/// </summary>
		/// <param name="type">
		/// A number referring to the type ID of the monster to spawn.
		/// </param>
		/// <param name="life">
		/// (optional) A number relating to the HP value the new NPC will spawn with.
		/// </param>
		/// <param name="lifeMax">
		/// (optional) A number relating to the HP Max value the NPC will spawn with.
		/// </param>
		/// <returns>
		/// The resulting spawned NPC.
		/// </returns>
		Terraria.NPC SpawnNPC(int type, int? life = null, int? lifeMax = null);

		/// <summary>
		/// Kills an NPC via its NPC object.
		/// </summary>
		/// <param name="npc">
		/// A reference to an NPC object to be killed.
		/// </param>
		void KillNPC(Terraria.NPC npc);

		/// <summary>
		/// Transforms an NPC via its NPC object.
		/// </summary>
		/// <param name="npc">
		/// A reference to an NPC object to be transformed.
		/// </param>
		/// <param name="newType">
		/// The NPC type the NPC will have after the transformation.
		/// </param>
		void TransformNPC(Terraria.NPC npc, int newType);

		/// <summary>
		/// Teleports an NPC to a new position via its NPC object.
		/// </summary>
		/// <param name="npc">
		/// A reference to an NPC object to be teleported.
		/// </param>
		/// <param name="targetX">
		/// The target position on the x-axis that the NPC will be teleported to.
		/// </param>
		/// <param name="targetY">
		/// The target position on the y-axis that the NPC will be teleported to.
		/// </param>
		/// <param name="style">
		/// (optional) The teleport style that the NPC will use when it teleports.
		/// </param>
		void TeleportNPC(Terraria.NPC npc, int targetX, int targetY, int style = 0);
	}
}
