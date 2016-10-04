using System;
using System.ComponentModel;
using Orion.Items;

namespace Orion.Npcs.Events
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcDroppingLoot"/> event.
	/// </summary>
	public class NpcDroppingLootEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets or sets the loot that the NPC is dropping.
		/// </summary>
		public IItem Item { get; set; }

		/// <summary>
		/// Gets the NPC that is dropping the loot.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppingLootEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The NPC that is dropping the loot.</param>
		/// <param name="item">The loot that the NPC is dropping.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="npc"/> or <paramref name="item"/> are null.
		/// </exception>
		public NpcDroppingLootEventArgs(INpc npc, IItem item)
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
