using System;
using Orion.Interfaces;
using Orion.Services;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTransformed"/> event.
	/// </summary>
	public class NpcTransformedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the new type ID.
		/// </summary>
		public int NewType { get; }

		/// <summary>
		/// Gets the <see cref="INpc"/>.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcKilledEventArgs"/> class with the specified
		/// <see cref="INpc"/> and new type ID.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/>.</param>
		/// <param name="newType">The new type ID.</param>
		public NpcTransformedEventArgs(INpc npc, int newType)
		{
			Npc = npc;
			NewType = newType;
		}
	}
}
