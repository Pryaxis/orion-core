using System;
using System.ComponentModel;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcSettingDefaults"/> event.
	/// </summary>
	public class NpcSettingDefaultsEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the <see cref="INpc"/> instance that's having its defaults set.
		/// </summary>
		public INpc Npc { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="NpcType"/> that the <see cref="INpc"/> instance is having its defaults set to.
		/// </summary>
		public NpcType Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSettingDefaultsEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> instance that's having its defaults set.</param>
		/// <param name="type">The <see cref="NpcType"/> that the <see cref="INpc"/> instance is having its defaults set to.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
		public NpcSettingDefaultsEventArgs(INpc npc, NpcType type)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
			Type = type;
		}
	}
}
