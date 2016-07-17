using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTeleporting"/> event.
	/// </summary>
	public class NpcTeleportingEventArgs : HandledEntityEventArgs<Terraria.NPC>
	{
		/// <summary>
		/// Gets the position in the world that the NPC is teleporting to.
		/// </summary>
		public Vector2 Position { get; }

		/// <summary>
		/// Gets the teleportation style.
		/// </summary>
		public int Style { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcTeleportingEventArgs"/> class with the specified NPC,
		/// position, and style.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="position">The position in the world.</param>
		/// <param name="style">The teleportation style.</param>
		public NpcTeleportingEventArgs(Terraria.NPC npc, Vector2 position, int style) : base(npc)
		{
			Position = position;
			Style = style;
		}
	}
}
