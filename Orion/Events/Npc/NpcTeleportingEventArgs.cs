using System.ComponentModel;
using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTeleporting"/> event.
	/// </summary>
	public class NpcTeleportingEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="INpc"/> that is teleporting.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Gets the position in the world that the <see cref="INpc"/> is teleporting to.
		/// </summary>
		public Vector2 Position { get; }

		/// <summary>
		/// Gets the teleportation style.
		/// </summary>
		public int Style { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcTeleportingEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that is teleporting.</param>
		/// <param name="position">The position in the world that the <see cref="INpc"/> is teleporting to.</param>
		/// <param name="style">The teleportation style.</param>
		public NpcTeleportingEventArgs(INpc npc, Vector2 position, int style)
		{
			Npc = npc;
			Position = position;
			Style = style;
		}
	}
}
