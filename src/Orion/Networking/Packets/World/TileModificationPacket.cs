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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to perform a tile modification.
    /// </summary>
    public sealed class TileModificationPacket : Packet {
        private ModificationType _tileModificationType;
        private short _tileX;
        private short _tileY;
        private short _tileModificationData;
        private byte _tileModificationStyle;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileModification;

        /// <summary>
        /// Gets or sets the tile modification type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ModificationType TileModificationType {
            get => _tileModificationType;
            set {
                _tileModificationType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX {
            get => _tileX;
            set {
                _tileX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY {
            get => _tileY;
            set {
                _tileY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile modification data.
        /// </summary>
        public short TileModificationData {
            get => _tileModificationData;
            set {
                _tileModificationData = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile modification style.
        /// </summary>
        public byte TileModificationStyle {
            get => _tileModificationStyle;
            set {
                _tileModificationStyle = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{TileModificationType} @ ({TileX}, {TileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            TileModificationType = ModificationType.FromId(reader.ReadByte()) ??
                                   throw new PacketException("Modification type is invalid.");
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            TileModificationData = reader.ReadInt16();
            TileModificationStyle = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(TileModificationType.Id);
            writer.Write(TileX);
            writer.Write(TileY);
            writer.Write(TileModificationData);
            writer.Write(TileModificationStyle);
        }

        /// <summary>
        /// Represents a modification type in a <see cref="TileModificationPacket"/>.
        /// </summary>
        public class ModificationType {
#pragma warning disable 1591
            public static ModificationType DestroyBlock = new ModificationType(0);
            public static ModificationType PlaceBlock = new ModificationType(1);
            public static ModificationType DestroyWall = new ModificationType(2);
            public static ModificationType PlaceWall = new ModificationType(3);
            public static ModificationType DestroyBlockNoItems = new ModificationType(4);
            public static ModificationType PlaceRedWire = new ModificationType(5);
            public static ModificationType RemoveRedWire = new ModificationType(6);
            public static ModificationType HalveBlock = new ModificationType(7);
            public static ModificationType PlaceActuator = new ModificationType(8);
            public static ModificationType RemoveActuator = new ModificationType(9);
            public static ModificationType PlaceBlueWire = new ModificationType(10);
            public static ModificationType RemoveBlueWire = new ModificationType(11);
            public static ModificationType PlaceGreenWire = new ModificationType(12);
            public static ModificationType RemoveGreenWire = new ModificationType(13);
            public static ModificationType SlopeBlock = new ModificationType(14);
            public static ModificationType FrameTrack = new ModificationType(15);
            public static ModificationType PlaceYellowWire = new ModificationType(16);
            public static ModificationType RemoveYellowWire = new ModificationType(17);
            public static ModificationType PokeLogicGate = new ModificationType(18);
            public static ModificationType ActuateBlock = new ModificationType(19);
#pragma warning restore 1591

            private const int ArrayOffset = 0;
            private const int ArraySize = ArrayOffset + 20;
            private static readonly ModificationType[] Modifications = new ModificationType[ArraySize];
            private static readonly string[] Names = new string[ArraySize];

            /// <summary>
            /// Gets the modification type's ID.
            /// </summary>
            public byte Id { get; }

            static ModificationType() {
                foreach (var field in typeof(ModificationType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                    var modificationType = (ModificationType)field.GetValue(null);
                    Modifications[ArrayOffset + modificationType.Id] = modificationType;
                    Names[ArrayOffset + modificationType.Id] = field.Name;
                }
            }

            private ModificationType(byte id) {
                Id = id;
            }

            /// <summary>
            /// Returns a modification type converted from the given ID.
            /// </summary>
            /// <param name="id">The ID.</param>
            /// <returns>The modification type, or <c>null</c> if none exists.</returns>
            public static ModificationType FromId(byte id) =>
                ArrayOffset + id < ArraySize ? Modifications[ArrayOffset + id] : null;

            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public override string ToString() => Names[ArrayOffset + Id];
        }
    }
}
