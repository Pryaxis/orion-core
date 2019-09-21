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
using Orion.Networking.World;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to perform a tile modification.
    /// </summary>
    public sealed class TileModificationPacket : Packet {
        private TileModificationType _tileModificationType;
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
        public TileModificationType TileModificationType {
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
            TileModificationType = TileModificationType.FromId(reader.ReadByte()) ??
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
    }
}
