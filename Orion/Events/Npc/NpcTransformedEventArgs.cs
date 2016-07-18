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
		/// Gets the relevant NPC.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets the new type that the NPC transformed to.
		/// </summary>
		public int NewType { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDiedEventArgs"/> class with the specified NPC and new type.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="newType">The new type.</param>
		public NpcTransformedEventArgs(INpc npc, int newType)
		{
			Npc = npc;
			NewType = newType;
		}
	}
}
