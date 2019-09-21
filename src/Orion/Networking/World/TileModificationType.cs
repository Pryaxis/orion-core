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
using Orion.Networking.Packets.World;

namespace Orion.Networking.World {
    /// <summary>
    /// Represents a tile modification type in a <see cref="TileModificationPacket"/>.
    /// </summary>
    public sealed class TileModificationType {
#pragma warning disable 1591
        public static TileModificationType DestroyBlock = new TileModificationType(0);
        public static TileModificationType PlaceBlock = new TileModificationType(1);
        public static TileModificationType DestroyWall = new TileModificationType(2);
        public static TileModificationType PlaceWall = new TileModificationType(3);
        public static TileModificationType DestroyBlockNoItems = new TileModificationType(4);
        public static TileModificationType PlaceRedWire = new TileModificationType(5);
        public static TileModificationType RemoveRedWire = new TileModificationType(6);
        public static TileModificationType HalveBlock = new TileModificationType(7);
        public static TileModificationType PlaceActuator = new TileModificationType(8);
        public static TileModificationType RemoveActuator = new TileModificationType(9);
        public static TileModificationType PlaceBlueWire = new TileModificationType(10);
        public static TileModificationType RemoveBlueWire = new TileModificationType(11);
        public static TileModificationType PlaceGreenWire = new TileModificationType(12);
        public static TileModificationType RemoveGreenWire = new TileModificationType(13);
        public static TileModificationType SlopeBlock = new TileModificationType(14);
        public static TileModificationType FrameTrack = new TileModificationType(15);
        public static TileModificationType PlaceYellowWire = new TileModificationType(16);
        public static TileModificationType RemoveYellowWire = new TileModificationType(17);
        public static TileModificationType PokeLogicGate = new TileModificationType(18);
        public static TileModificationType ActuateBlock = new TileModificationType(19);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 20;
        private static readonly TileModificationType[] Modifications = new TileModificationType[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the modification type's ID.
        /// </summary>
        public byte Id { get; }

        static TileModificationType() {
            foreach (var field in typeof(TileModificationType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var modificationType = (TileModificationType)field.GetValue(null);
                Modifications[ArrayOffset + modificationType.Id] = modificationType;
                Names[ArrayOffset + modificationType.Id] = field.Name;
            }
        }

        private TileModificationType(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a modification type converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The modification type, or <c>null</c> if none exists.</returns>
        public static TileModificationType FromId(byte id) =>
            ArrayOffset + id < ArraySize ? Modifications[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
