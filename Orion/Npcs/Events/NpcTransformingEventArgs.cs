using System;
using System.ComponentModel;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTransforming"/> event.
	/// </summary>
	public class NpcTransformingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="INpc"/> instance that's transforming.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets or sets the <see cref="NpcType"/> that the <see cref="INpc"/> instance is transforming to.
		/// </summary>
		public NpcType Type { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcTransformingEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> instance that's transforming.</param>
		/// <param name="type">The <see cref="NpcType"/> that the <see cref="INpc"/> instance is transforming to.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
		public NpcTransformingEventArgs(INpc npc, NpcType type)
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
