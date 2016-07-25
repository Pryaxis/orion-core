using System;
using Orion.Interfaces;
using System.ComponentModel;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.NpcDroppingLoot"/> event.
	/// </summary>
	public class NpcDroppingLootEventArgs : HandledEventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> that the NPC is dropping.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets the <see cref="INpc"/> that is dropping the item.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppingLootEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that is dropping.</param>
		/// <param name="npc">The <see cref="INpc"/> that is dropping the item.</param>
		/// <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="npc"/> was null.</exception>
		public NpcDroppingLootEventArgs(IItem item, INpc npc)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}
			if (npc == null)
			{
				throw new ArgumentException(nameof(npc));
			}

			Item = item;
			Npc = npc;
		}
	}
}
