using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Orion.Hooks;
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
        /// Occurs when an NPC is spawning.
        /// </summary>
        HookHandlerCollection<SpawningNpcEventArgs> SpawningNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC spawned.
        /// </summary>
        HookHandlerCollection<SpawnedNpcEventArgs> SpawnedNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC is having its defaults set.
        /// </summary>
        HookHandlerCollection<SettingNpcDefaultsEventArgs> SettingNpcDefaults { get; set; }

        /// <summary>
        /// Occurs when an NPC had its defaults set.
        /// </summary>
        HookHandlerCollection<SetNpcDefaultsEventArgs> SetNpcDefaults { get; set; }

        /// <summary>
        /// Occurs when an NPC is being updated.
        /// </summary>
        HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC's AI is being updated.
        /// </summary>
        HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpcAi { get; set; }

        /// <summary>
        /// Occurs when an NPC's AI is updated.
        /// </summary>
        HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpcAi { get; set; }

        /// <summary>
        /// Occurs when an NPC is updated.
        /// </summary>
        HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC is being damaged.
        /// </summary>
        HookHandlerCollection<DamagingNpcEventArgs> DamagingNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC is damaged.
        /// </summary>
        HookHandlerCollection<DamagedNpcEventArgs> DamagedNpc { get; set; }

        /// <summary>
        /// Occurs when an NPC is transforming to another type.
        /// </summary>
        HookHandlerCollection<NpcTransformingEventArgs> NpcTransforming { get; set; }

        /// <summary>
        /// Occurs when an NPC has transformed to another type.
        /// </summary>
        HookHandlerCollection<NpcTransformedEventArgs> NpcTransformed { get; set; }

        /// <summary>
        /// Occurs when an NPC is dropping a loot item.
        /// </summary>
        HookHandlerCollection<NpcDroppingLootItemEventArgs> NpcDroppingLootItem { get; set; }

        /// <summary>
        /// Occurs when an NPC has dropped a loot item.
        /// </summary>
        HookHandlerCollection<NpcDroppedLootItemEventArgs> NpcDroppedLootItem { get; set; }

        /// <summary>
        /// Occurs when an NPC was killed.
        /// </summary>
        HookHandlerCollection<KilledNpcEventArgs> KilledNpc { get; set; }

        /// <summary>
        /// Spawns an NPC with the given type at the specified position with the AI values.
        /// </summary>
        /// <param name="type">The NPC type.</param>
        /// <param name="position">The position.</param>
        /// <param name="aiValues">
        /// The AI values to use, or <c>null</c> for none. If not <c>null</c>, this should have length 4.
        /// </param>
        /// <returns>The resulting NPC, or <c>null</c> if none was spawned.</returns>
        /// <exception cref="ArgumentException"><paramref name="aiValues"/> does not have length 4.</exception>
        INpc SpawnNpc(NpcType type, Vector2 position, float[] aiValues = null);
    }
}
