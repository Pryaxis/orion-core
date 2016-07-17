using Orion.Interfaces;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcTransformed"/> event.
	/// </summary>
	public class NpcTransformedEventArgs : EntityEventArgs<Terraria.NPC>
	{
		/// <summary>
		/// Gets the new type that the NPC transformed to.
		/// </summary>
		public int NewType { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDiedEventArgs"/> class with the specified NPC and new type.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="newType">The new type.</param>
		public NpcTransformedEventArgs(Terraria.NPC npc, int newType) : base(npc)
		{
			NewType = newType;
		}
	}
}
