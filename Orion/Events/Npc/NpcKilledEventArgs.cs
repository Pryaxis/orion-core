using System;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcKilled"/> event.
	/// </summary>
	public class NpcKilledEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="INpc"/> that was killed.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcKilledEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that was killed.</param>
		public NpcKilledEventArgs(INpc npc)
		{
			Npc = npc;
		}
	}
}
