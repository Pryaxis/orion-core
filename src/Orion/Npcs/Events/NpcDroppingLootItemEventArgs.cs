using System;
using System.ComponentModel;
using Orion.Items;

namespace Orion.Npcs.Events {
    /// <summary>
    /// Provides data for the <see cref="INpcService.NpcDroppingLootItem"/> event.
    /// </summary>
    public sealed class NpcDroppingLootItemEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the NPC that is dropping the loot item.
        /// </summary>
        public INpc Npc { get; }

        /// <summary>
        /// Gets or sets the loot item type.
        /// </summary>
        public ItemType LootItemType { get; set; }

        /// <summary>
        /// Gets or sets the loot item stack size.
        /// </summary>
        public int LootItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the loot item prefix.
        /// </summary>
        public ItemPrefix LootItemPrefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NpcDroppingLootItemEventArgs"/> class with the specified NPC.
        /// </summary>
        /// <param name="npc">The NPC.</param>
        /// <exception cref="ArgumentNullException"><paramref name="npc"/> is null.</exception>
        public NpcDroppingLootItemEventArgs(INpc npc) {
            Npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }
    }
}
