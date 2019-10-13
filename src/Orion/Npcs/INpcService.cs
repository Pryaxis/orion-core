// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Events;
using Orion.Events.Npcs;
using Orion.Utils;

namespace Orion.Npcs {
    /// <summary>
    /// Represents an NPC service. Provides access to NPC-related events and methods, and in a thread-safe manner unless
    /// specified otherwise.
    /// </summary>
    public interface INpcService {
        /// <summary>
        /// Gets the NPCs in the world. All NPCs are returned, regardless of whether or not they are actually active.
        /// </summary>
        IReadOnlyArray<INpc> Npcs { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC's defaults are being set, which is when NPC data is
        /// initialized. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcSetDefaultsEventArgs> NpcSetDefaults { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is spawning. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcSpawnEventArgs> NpcSpawn { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is updating. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcUpdateEventArgs> NpcUpdate { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is transforming. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcTransformEventArgs> NpcTransform { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is being damaged. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcDamageEventArgs> NpcDamage { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is dropping a loot item. This event can be canceled.
        /// </summary>
        EventHandlerCollection<NpcDropLootItemEventArgs> NpcDropLootItem { get; }

        /// <summary>
        /// Gets the event handlers that occur when an NPC is killed.
        /// </summary>
        EventHandlerCollection<NpcKilledEventArgs> NpcKilled { get; }

        /// <summary>
        /// Spawns an NPC with the given <paramref name="type"/> at the specified <paramref name="position"/> with the
        /// <paramref name="aiValues"/>.
        /// 
        /// <para/>
        /// 
        /// This method is not thread-safe.
        /// </summary>
        /// <param name="type">The NPC type.</param>
        /// <param name="position">The position.</param>
        /// <param name="aiValues">
        /// The AI values to use, or <see langword="null"/> for none. If not <see langword="null"/>, this should have
        /// length 4. These AI values are values that control type-specific behavior.
        /// </param>
        /// <returns>The resulting NPC, or <see langword="null"/> if none was spawned.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="aiValues"/> is not <see langword="null"/> and does not have length 4.
        /// </exception>
        INpc? SpawnNpc(NpcType type, Vector2 position, float[]? aiValues = null);
    }
}
