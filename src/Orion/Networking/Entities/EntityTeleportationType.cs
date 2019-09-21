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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Orion.Networking.Packets.Entities;
using Orion.Networking.Packets.Misc;

namespace Orion.Networking.Entities {
    /// <summary>
    /// Represents an entity teleportation type in a <see cref="EntityTeleportationPacket"/>.
    /// </summary>
    public sealed class EntityTeleportationType {
#pragma warning disable 1591
        public static readonly EntityTeleportationType Player = new EntityTeleportationType(0);
        public static readonly EntityTeleportationType Npc = new EntityTeleportationType(1);
        public static readonly EntityTeleportationType PlayerToPlayer = new EntityTeleportationType(2);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 3;
        private static readonly EntityTeleportationType[] Actions = new EntityTeleportationType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the entity teleportation type's ID.
        /// </summary>
        public byte Id { get; }

        static EntityTeleportationType() {
            foreach (var field in typeof(EntityTeleportationType).GetFields(BindingFlags.Public |
                                                                            BindingFlags.Static)) {
                var entityTeleportationType = (EntityTeleportationType)field.GetValue(null);
                Actions[ArrayOffset + entityTeleportationType.Id] = entityTeleportationType;
                Names[ArrayOffset + entityTeleportationType.Id] = field.Name;
            }
        }

        private EntityTeleportationType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns an entity teleportation type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The entity teleportation type, or <c>null</c> if none exists.</returns>
        public static EntityTeleportationType FromId(byte id) =>
            ArrayOffset + id < ArraySize ? Actions[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
