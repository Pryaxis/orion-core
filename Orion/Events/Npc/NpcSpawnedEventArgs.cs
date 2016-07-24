using System;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

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
		/// Initializes a new instance of the <see cref="NpcSpawnedEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that spawned.</param>
		public NpcSpawnedEventArgs(INpc npc)
		{
			Npc = npc;
		}
	}
}
