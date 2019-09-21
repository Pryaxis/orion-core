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
using Orion.Networking.Packets.World.TileEntities;

namespace Orion.Networking.World.TileEntities {
    /// <summary>
    /// Represents a chest modification in a <see cref="ChestModificationPacket"/>.
    /// </summary>
    public class ChestModification {
#pragma warning disable 1591
        public static ChestModification PlaceChest = new ChestModification(0);
        public static ChestModification BreakChest = new ChestModification(1);
        public static ChestModification PlaceDresser = new ChestModification(2);
        public static ChestModification BreakDresser = new ChestModification(3);
        public static ChestModification PlaceChest2 = new ChestModification(4);
        public static ChestModification BreakChest2 = new ChestModification(5);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 6;
        private static readonly ChestModification[] Types = new ChestModification[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the chest modification's ID.
        /// </summary>
        public byte Id { get; }

        static ChestModification() {
            foreach (var field in typeof(ChestModification).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var modificationType = (ChestModification)field.GetValue(null);
                Types[ArrayOffset + modificationType.Id] = modificationType;
                Names[ArrayOffset + modificationType.Id] = field.Name;
            }
        }

        private ChestModification(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a chest modification converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The chest modification, or <c>null</c> if none exists.</returns>
        public static ChestModification FromId(byte id) =>
            ArrayOffset + id < ArraySize ? Types[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
