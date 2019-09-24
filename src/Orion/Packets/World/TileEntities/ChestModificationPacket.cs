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
using System.IO;
using JetBrains.Annotations;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to perform a chest modification. See <see cref="TileEntities.ChestModification"/>
    /// for a list of chest modifications.
    /// </summary>
    [PublicAPI]
    public sealed class ChestModificationPacket : Packet {
        private ChestModification _chestModification;
        private short _chestX;
        private short _chestY;
        private short _chestStyle;
        private short _chestIndex;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ChestModification;

        /// <summary>
        /// Gets or sets the chest modification.
        /// </summary>
        public ChestModification ChestModification {
            get => _chestModification;
            set {
                _chestModification = value;
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
            $"{Type}[{ChestModification}, #={ChestIndex} @ ({ChestX}, {ChestY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chestModification = (ChestModification)reader.ReadByte();
            _chestX = reader.ReadInt16();
            _chestY = reader.ReadInt16();
            _chestStyle = reader.ReadInt16();
            _chestIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_chestModification);
            writer.Write(_chestX);
            writer.Write(_chestY);
            writer.Write(_chestStyle);
            writer.Write(_chestIndex);
        }
    }
}
