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
using Orion.Networking.Packets.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Represents a tile modification in a <see cref="TileModificationPacket"/>.
    /// </summary>
    public sealed class TileModification {
#pragma warning disable 1591
        public static TileModification DestroyBlock = new TileModification(0);
        public static TileModification PlaceBlock = new TileModification(1);
        public static TileModification DestroyWall = new TileModification(2);
        public static TileModification PlaceWall = new TileModification(3);
        public static TileModification DestroyBlockNoItems = new TileModification(4);
        public static TileModification PlaceRedWire = new TileModification(5);
        public static TileModification RemoveRedWire = new TileModification(6);
        public static TileModification HalveBlock = new TileModification(7);
        public static TileModification PlaceActuator = new TileModification(8);
        public static TileModification RemoveActuator = new TileModification(9);
        public static TileModification PlaceBlueWire = new TileModification(10);
        public static TileModification RemoveBlueWire = new TileModification(11);
        public static TileModification PlaceGreenWire = new TileModification(12);
        public static TileModification RemoveGreenWire = new TileModification(13);
        public static TileModification SlopeBlock = new TileModification(14);
        public static TileModification FrameTrack = new TileModification(15);
        public static TileModification PlaceYellowWire = new TileModification(16);
        public static TileModification RemoveYellowWire = new TileModification(17);
        public static TileModification PokeLogicGate = new TileModification(18);
        public static TileModification ActuateBlock = new TileModification(19);
#pragma warning restore 1591

        private const int ArrayOffset = 0;
        private const int ArraySize = ArrayOffset + 20;
        private static readonly TileModification[] Modifications = new TileModification[ArraySize];
        private static readonly string[] Names = new string[ArraySize];

        /// <summary>
        /// Gets the tile modification's ID.
        /// </summary>
        public byte Id { get; }

        static TileModification() {
            foreach (var field in typeof(TileModification).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                var tileModificationType = (TileModification)field.GetValue(null);
                Modifications[ArrayOffset + tileModificationType.Id] = tileModificationType;
                Names[ArrayOffset + tileModificationType.Id] = field.Name;
            }
        }

        private TileModification(byte id) {
            Id = id;
        }

        /// <summary>
        /// Returns a tile modification converted from the given ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>The tile modification, or <c>null</c> if none exists.</returns>
        public static TileModification FromId(byte id) =>
            ArrayOffset + id < ArraySize ? Modifications[ArrayOffset + id] : null;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => Names[ArrayOffset + Id];
    }
}
