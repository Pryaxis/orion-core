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
using Orion.World.TileEntities;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to place a tile entity.
    /// </summary>
    [PublicAPI]
    public sealed class TileEntityPlacePacket : Packet {
        private short _tileEntityX;
        private short _tileEntityY;
        private TileEntityType _tileEntityType;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileEntityPlace;

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        public short TileEntityX {
            get => _tileEntityX;
            set {
                _tileEntityX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        public short TileEntityY {
            get => _tileEntityY;
            set {
                _tileEntityY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile entity's type.
        /// </summary>
        public TileEntityType TileEntityType {
            get => _tileEntityType;
            set {
                _tileEntityType = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{TileEntityType} @ ({TileEntityX}, {TileEntityY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _tileEntityX = reader.ReadInt16();
            _tileEntityY = reader.ReadInt16();
            _tileEntityType = (TileEntityType)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_tileEntityX);
            writer.Write(_tileEntityY);
            writer.Write((byte)_tileEntityType);
        }
    }
}
