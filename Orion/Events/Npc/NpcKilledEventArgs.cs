using System;
using Orion.Entities.Npc;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcKilled"/> event.
	/// </summary>
	public class NpcKilledEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="INpc"/> instance that was killed.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcKilledEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> instance that was killed.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> was null.</exception>
		public NpcKilledEventArgs(INpc npc)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
		}
	}
}
