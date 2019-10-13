// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent from the client to the server to open a chest.
    /// </summary>
    public sealed class ChestOpenPacket : Packet {
        private short _chestX;
        private short _chestY;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ChestOpen;

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

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({ChestX}, {ChestY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chestX = reader.ReadInt16();
            _chestY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_chestX);
            writer.Write(_chestY);
        }
    }
}
