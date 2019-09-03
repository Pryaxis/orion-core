using System;
using System.Collections.Generic;
using Orion.Framework;
using Orion.Npcs.Events;

namespace Orion.Npcs {
    /// <summary>
    /// Provides a mechanism for managing NPCs.
    /// </summary>
    public interface INpcService : IReadOnlyList<INpc>, IService {
        /// <summary>
        /// Gets or sets the base NPC spawning rate.
        /// </summary>
        int BaseNpcSpawningRate { get; set; }

        /// <summary>
        /// Gets or sets the base NPC spawning limit.
        /// </summary>
        int BaseNpcSpawningLimit { get; set; }

        /// <summary>
        /// Occurs when an NPC is having its defaults set.
        /// </summary>
        event EventHandler<NpcSettingDefaultsEventArgs> NpcSettingDefaults;

        /// <summary>
        /// Occurs when an NPC had its defaults set.
        /// </summary>
        event EventHandler<NpcSetDefaultsEventArgs> NpcSetDefaults;

        /// <summary>
        /// Occurs when an NPC is being updated.
        /// </summary>
        event EventHandler<NpcUpdatingEventArgs> NpcUpdating;

        /// <summary>
        /// Occurs when an NPC is updated.
        /// </summary>
        event EventHandler<NpcUpdatedEventArgs> NpcUpdated;

        /// <summary>
        /// Occurs when an NPC's AI is being updated.
        /// </summary>
        event EventHandler<NpcUpdatingEventArgs> NpcUpdatingAi;

        /// <summary>
        /// Occurs when an NPC's AI is updated.
        /// </summary>
        event EventHandler<NpcUpdatedEventArgs> NpcUpdatedAi;

        /// <summary>
        /// Occurs when an NPC is transforming to another type.
        /// </summary>
        event EventHandler<NpcTransformingEventArgs> NpcTransforming;

        /// <summary>
        /// Occurs when an NPC has transformed to another type.
        /// </summary>
        event EventHandler<NpcTransformedEventArgs> NpcTransformed;

        /// <summary>
        /// Occurs when an NPC is dropping a loot item.
        /// </summary>
        event EventHandler<NpcDroppingLootItemEventArgs> NpcDroppingLootItem;

        /// <summary>
        /// Occurs when an NPC has dropped a loot item.
        /// </summary>
        event EventHandler<NpcDroppedLootItemEventArgs> NpcDroppedLootItem;

        /// <summary>
        /// Occurs when an NPC was killed.
        /// </summary>
        event EventHandler<NpcKilledEventArgs> NpcKilled;
    }
}
