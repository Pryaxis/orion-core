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
		/// Gets the <see cref="INpc"/>.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcKilledEventArgs"/> class with the specified
		/// <see cref="INpc"/>.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/>.</param>
		public NpcKilledEventArgs(INpc npc)
		{
			Npc = npc;
		}
	}
}
