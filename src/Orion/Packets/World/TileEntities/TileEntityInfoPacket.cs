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

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set tile entity information.
    /// </summary>
    public sealed class TileEntityInfoPacket : Packet {
        private int _tileEntityIndex;
        private NetworkTileEntity? _tileEntity;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileEntityInfo;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || TileEntity?.IsDirty == true;

        /// <summary>
        /// Gets or sets the tile entity index.
        /// </summary>
        public int TileEntityIndex {
            get => _tileEntityIndex;
            set {
                _tileEntityIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile entity. A value of <c>null</c> indicates a removal.
        /// </summary>
        public NetworkTileEntity? TileEntity {
            get => _tileEntity;
            set {
                _tileEntity = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            TileEntity?.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={TileEntityIndex}, {TileEntity}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _tileEntityIndex = reader.ReadInt32();
            if (!reader.ReadBoolean()) return;

            _tileEntity = NetworkTileEntity.ReadFromReader(reader, false);
            _tileEntity._index = TileEntityIndex;
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_tileEntityIndex);
            writer.Write(_tileEntity != null);
            _tileEntity?.WriteToWriter(writer, false);
        }
    }
}
