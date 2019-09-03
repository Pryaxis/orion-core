using System;
using System.ComponentModel;
using Orion.Items;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcDroppedLootItem"/> event.
    /// </summary>
    public sealed class NpcDroppedLootItemEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that dropped the loot item.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets the loot item.
        /// </summary>
        public IItem LootItem { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDroppedLootItemEventArgs"/> class with the specified NPC
        /// and loot item.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <param name="lootItem">The loot item.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="npc"/> or <paramref name="lootItem"/> are null.
        /// </exception>
        public NpcDroppedLootItemEventArgs(INpc npc, IItem lootItem) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
            LootItem = lootItem ?? throw new ArgumentNullException(nameof(lootItem));
        }
    }
}
