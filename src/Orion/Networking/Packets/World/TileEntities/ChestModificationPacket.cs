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

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to perform a chest modification.
    /// </summary>
    public sealed class ChestModificationPacket : Packet {
        private ModificationType _chestModificationType;
        private short _chestX;
        private short _chestY;
        private short _chestStyle;
        private short _chestIndex;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ChestModification;

        /// <summary>
        /// Gets or sets the chest modification type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public ModificationType ChestModificationType {
            get => _chestModificationType;
            set {
                _chestModificationType = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        public short ChestX {
            get => _chestX;
            set {
                _chestX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        public short ChestY {
            get => _chestY;
            set {
                _chestY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest style.
        /// </summary>
        public short ChestStyle {
            get => _chestStyle;
            set {
                _chestStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        public short ChestIndex {
            get => _chestIndex;
            set {
                _chestIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{ChestModificationType}, #={ChestIndex} @ ({ChestX}, {ChestY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ChestModificationType = ModificationType.FromId(reader.ReadByte()) ??
                                    throw new PacketException("Modification type is invalid.");
            ChestX = reader.ReadInt16();
            ChestY = reader.ReadInt16();
            ChestStyle = reader.ReadInt16();
            ChestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ChestModificationType.Id);
            writer.Write(ChestX);
            writer.Write(ChestY);
            writer.Write(ChestStyle);
            writer.Write(ChestIndex);
        }

        /// <summary>
        /// Represents a modification type in a <see cref="ChestModificationPacket"/>.
        /// </summary>
        public class ModificationType {
#pragma warning disable 1591
            public static ModificationType PlaceChest = new ModificationType(0);
            public static ModificationType BreakChest = new ModificationType(1);
            public static ModificationType PlaceDresser = new ModificationType(2);
            public static ModificationType BreakDresser = new ModificationType(3);
            public static ModificationType PlaceChest2 = new ModificationType(4);
            public static ModificationType BreakChest2 = new ModificationType(5);
#pragma warning restore 1591

            private const int ArrayOffset = 0;
            private const int ArraySize = ArrayOffset + 6;
            private static readonly ModificationType[] Types = new ModificationType[ArraySize];
            private static readonly string[] Names = new string[ArraySize];

            /// <summary>
            /// Gets the modification type's ID.
            /// </summary>
            public byte Id { get; }

            static ModificationType() {
                foreach (var field in typeof(ModificationType).GetFields(BindingFlags.Public | BindingFlags.Static)) {
                    var modificationType = (ModificationType)field.GetValue(null);
                    Types[ArrayOffset + modificationType.Id] = modificationType;
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
                ArrayOffset + id < ArraySize ? Types[ArrayOffset + id] : null;

            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public override string ToString() => Names[ArrayOffset + Id];
        }
    }
}
