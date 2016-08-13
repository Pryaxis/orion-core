using System;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcSpawned"/> event.
	/// </summary>
	public class NpcSpawnedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the NPC that spawned.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSpawnedEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The NPC that spawned.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
		public NpcSpawnedEventArgs(INpc npc)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
		}
	}
}
