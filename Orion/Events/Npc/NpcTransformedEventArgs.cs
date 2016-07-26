using System;
using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTransformed"/> event.
	/// </summary>
	public class NpcTransformedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the new type ID that the <see cref="INpc"/> transformed to.
		/// </summary>
		public int NewType { get; }

		/// <summary>
		/// Gets the <see cref="INpc"/> that transformed.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcTransformedEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that transformed.</param>
		/// <param name="newType">The new type ID that the <see cref="INpc"/> transformed to.</param>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> was null.</exception>
		public NpcTransformedEventArgs(INpc npc, int newType)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
			NewType = newType;
		}
	}
}
