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
		/// Gets the type of the item that the Npc dropped.
		/// </summary>
		public int Type { get; }

		/// <summary>
		/// Gets the loot's X coordinate.
		/// </summary>
		public int X { get; }

		/// <summary>
		/// Gets the loot's Y coordinate.
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// Gets the loot's width.
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// Gets the loot's height.
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// Gets the loot's stack.
		/// </summary>
		public int Stack { get; }

		/// <summary>
		/// Gets the loot's prefix.
		/// </summary>
		public int Prefix { get; }

		/// <summary>
		/// Gets the NPC that dropped the loot.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppedLootEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The NPC that dropped the loot.</param>
		/// <param name="type">The type of the item that the Npc dropped.</param>
		/// <param name="x">The loot's X coordinate.</param>
		/// <param name="y">The loot's Y coordinate.</param>
		/// <param name="width">The loot's width.</param>
		/// <param name="height">The loot's height.</param>
		/// <param name="stack">The loot's stack.</param>
		/// <param name="prefix">The loot's prefix.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="npc"/> is null.
		/// </exception>
		public NpcDroppedLootEventArgs(INpc npc, int type, int x, int y, int width, int height, int stack, int prefix)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}

			Npc = npc;
			Type = type;
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Stack = stack;
			Prefix = prefix;
		}
	}
}
