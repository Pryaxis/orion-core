using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcSpawned"/> event.
	/// </summary>
	public class NpcSpawnedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the relevant NPC.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets the position in the world that the NPC spawned at.
		/// </summary>
		public Vector2 Position { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSpawnedEventArgs"/> class with the specified NPC and
		/// position.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="position">The position in the world.</param>
		public NpcSpawnedEventArgs(INpc npc, Vector2 position)
		{
			Npc = npc;
			Position = position;
		}
	}
}
