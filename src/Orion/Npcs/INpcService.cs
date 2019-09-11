// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using Microsoft.Xna.Framework;
using Orion.Hooks;
using Orion.Npcs.Events;
using Orion.Utils;

namespace Orion.Npcs {
    /// <summary>
    /// Represents a service that manages players. Provides NPC-related hooks and methods.
    /// </summary>
    public interface INpcService : IReadOnlyArray<INpc>, IService {
        /// <summary>
        /// Gets or sets the base NPC spawning rate.
        /// </summary>
        int BaseNpcSpawningRate { get; set; }

        /// <summary>
        /// Gets or sets the base NPC spawning limit.
        /// </summary>
        int BaseNpcSpawningLimit { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is spawning. This hook can be handled.
        /// </summary>
        HookHandlerCollection<SpawningNpcEventArgs> SpawningNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC spawned.
        /// </summary>
        HookHandlerCollection<SpawnedNpcEventArgs> SpawnedNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is having its defaults set. This hook can be handled.
        /// </summary>
        HookHandlerCollection<SettingNpcDefaultsEventArgs> SettingNpcDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC had its defaults set.
        /// </summary>
        HookHandlerCollection<SetNpcDefaultsEventArgs> SetNpcDefaults { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC's AI is being updated. This hook can be handled.
        /// </summary>
        HookHandlerCollection<UpdatingNpcEventArgs> UpdatingNpcAi { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC's AI is updated.
        /// </summary>
        HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpcAi { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is updated.
        /// </summary>
        HookHandlerCollection<UpdatedNpcEventArgs> UpdatedNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is being damaged. This hook can be handled.
        /// </summary>
        HookHandlerCollection<DamagingNpcEventArgs> DamagingNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is damaged.
        /// </summary>
        HookHandlerCollection<DamagedNpcEventArgs> DamagedNpc { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is transforming to another type. This hook can be
        /// handled.
        /// </summary>
        HookHandlerCollection<NpcTransformingEventArgs> NpcTransforming { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC has transformed to another type.
        /// </summary>
        HookHandlerCollection<NpcTransformedEventArgs> NpcTransformed { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC is dropping a loot item. This hook can be handled.
        /// </summary>
        HookHandlerCollection<NpcDroppingLootItemEventArgs> NpcDroppingLootItem { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC has dropped a loot item.
        /// </summary>
        HookHandlerCollection<NpcDroppedLootItemEventArgs> NpcDroppedLootItem { get; set; }

        /// <summary>
        /// Gets or sets the hook handlers that occur when an NPC was killed.
        /// </summary>
        HookHandlerCollection<KilledNpcEventArgs> KilledNpc { get; set; }

        /// <summary>
        /// Spawns and returns an NPC with the given <see cref="NpcType"/> at the specified position with the AI values.
        /// </summary>
        /// <param name="type">The <see cref="NpcType"/>.</param>
        /// <param name="position">The position.</param>
        /// <param name="aiValues">
        /// The AI values, or <c>null</c> for none. If not <c>null</c>, this should have length 4.
        /// </param>
        /// <returns>The resulting <see cref="INpc"/> instance, or <c>null</c> if none was spawned.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <c>null</c> and does not have length 4.
        /// </exception>
        INpc SpawnNpc(NpcType type, Vector2 position, float[] aiValues = null);
    }
}
