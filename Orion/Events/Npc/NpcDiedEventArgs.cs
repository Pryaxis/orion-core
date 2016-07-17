using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcDied"/> event.
	/// </summary>
	public class NpcDiedEventArgs : EntityEventArgs<Terraria.NPC>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDiedEventArgs"/> class with the specified NPC.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		public NpcDiedEventArgs(Terraria.NPC npc) : base(npc)
		{
		}
	}
}
