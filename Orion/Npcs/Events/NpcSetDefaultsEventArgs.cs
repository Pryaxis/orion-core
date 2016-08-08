using System;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcSetDefaults"/> event.
	/// </summary>
	public class NpcSetDefaultsEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="INpc"/> instance that had its defaults set.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSetDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> instance that had its defaults set.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
		public NpcSetDefaultsEventArgs(INpc npc)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
		}
	}
}
