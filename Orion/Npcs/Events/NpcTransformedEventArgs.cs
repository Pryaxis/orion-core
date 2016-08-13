using System;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTransformed"/> event.
	/// </summary>
	public class NpcTransformedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the NPC that transformed.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcTransformedEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The NPC that transformed.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
		public NpcTransformedEventArgs(INpc npc)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
		}
	}
}
