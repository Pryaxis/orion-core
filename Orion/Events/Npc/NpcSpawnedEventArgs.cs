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
		/// Gets the <see cref="INpc"/> that spawned.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets the position in the world that the <see cref="INpc"/> spawned.
		/// </summary>
		public Vector2 Position { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSpawnedEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that spawned.</param>
		/// <param name="position">The position in the world that the <see cref="INpc"/> spawned.</param>
		public NpcSpawnedEventArgs(INpc npc, Vector2 position)
		{
			Npc = npc;
			Position = position;
		}
	}
}
