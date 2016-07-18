using System;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcDied"/> event.
	/// </summary>
	public class NpcDiedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the relevant NPC.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDiedEventArgs"/> class with the specified NPC.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		public NpcDiedEventArgs(INpc npc)
		{
			Npc = npc;
		}
	}
}
