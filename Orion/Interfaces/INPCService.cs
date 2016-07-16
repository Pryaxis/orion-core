using Orion.Events;
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
		event EventHandler<NPCSpawnEventArgs> NPCDeath;

		/// <summary>
		/// Creates a new NPC with the NPC's default parameters, optionally with custom life values.
		/// </summary>
		/// <param name="type">
		/// A number referring to the type ID of the monster to spawn.
		/// </param>
		/// <param name="npc">
		/// (out) A reference relating to the Terraria.NPC object which will be updated with the new NPC object
		/// after it has spawned into the world.
		/// </param>
		/// <param name="life">
		/// (optional) A number relating to the HP value the new NPC will spawn with
		/// </param>
		/// <param name="lifeMax">
		/// (optional) A number relating to the HP Max value the NPC will spawn with
		/// </param>
		/// <returns>
		/// A number of the position in the Terraria NPC array.
		/// </returns>
		int SpawnNPC(int type, out Terraria.NPC npc, int? life = null, int? lifeMax = null);

		/// <summary>
		/// Kills an NPC via its NPC object.
		/// </summary>
		/// <param name="npc">
		/// A reference to an NPC object to be killed.
		/// </param>
		void KillNPC(Terraria.NPC npc);

		/// <summary>
		/// Kills an NPC via its position in the Terraria NPC array.
		/// </summary>
		/// <param name="id"></param>
		void KillNPC(int id);
	}
}
