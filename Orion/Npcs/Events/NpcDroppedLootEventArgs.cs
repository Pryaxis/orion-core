using System;
using Orion.Items;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcDroppedLoot"/> event.
	/// </summary>
	public class NpcDroppedLootEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the loot that the NPC dropped.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets the NPC that dropped the loot.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppedLootEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The NPC that dropped the loot.</param>
		/// <param name="item">The loot that the NPC dropped.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="npc"/> or <paramref name="item"/> are null.
		/// </exception>
		public NpcDroppedLootEventArgs(INpc npc, IItem item)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			Npc = npc;
			Item = item;
		}
	}
}
