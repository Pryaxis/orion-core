using Microsoft.Xna.Framework;
using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcSpawned"/> event.
	/// </summary>
	public class NpcSpawnedEventArgs : EntityEventArgs<Terraria.NPC>
	{
		/// <summary>
		/// Gets the position in the world that the NPC spawned at.
		/// </summary>
		public Vector2 Position { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcSpawnedEventArgs"/> class with the specified NPC and
		/// position.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="position">The position in the world.</param>
		public NpcSpawnedEventArgs(Terraria.NPC npc, Vector2 position) : base(npc)
		{
			Position = position;
		}
	}
}
