using Orion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="IItemService.NpcDroppedLoot"/> event.
	/// </summary>
	public class NpcDroppedLootEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the <see cref="IItem"/> that the NPC has dropped.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets the <see cref="INpc"/> that dropped the item.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppedLootEventArgs"/> class.
		/// </summary>
		/// <param name="item">The <see cref="IItem"/> that has dropped.</param>
		/// <param name="npc">The <see cref="INpc"/> that has dropped the item.</param>
		/// <exception cref="ArgumentNullException"><paramref name="item"/> was null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="npc"/> was null.</exception>
		public NpcDroppedLootEventArgs(IItem item, INpc npc)
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
