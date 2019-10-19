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

using System.IO;
using TerrariaChest = Terraria.Chest;

namespace Orion.Packets.World.TileEntities {
    /// <summary>
    /// Packet sent to set chest information.
    /// </summary>
    public sealed class ChestInfoPacket : Packet {
        private short _chestIndex;
        private short _x;
        private short _y;
        private string? _name;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ChestInfo;

        /// <summary>
        /// Gets or sets the chest index.
        /// </summary>
        /// <value>The chest index.</value>
        public short ChestIndex {
            get => _chestIndex;
            set {
                _chestIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        /// <value>The chest's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        /// <value>The Chest's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        /// <value>The chest's name.</value>
        public string? Name {
            get => _name;
            set {
                _name = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _chestIndex = reader.ReadInt16();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            var nameLength = reader.ReadByte();

            if (nameLength > 0 && nameLength <= TerrariaChest.MaxNameLength) {
                _name = reader.ReadString();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_chestIndex);
            writer.Write(_x);
            writer.Write(_y);

            // This packet's logic is actually insane... why is it set up this way?
            if (_name is null) {
                writer.Write((byte)0);
            } else if (_name.Length == 0 || _name.Length > TerrariaChest.MaxNameLength) {
                writer.Write(byte.MaxValue);
            } else {
                writer.Write((byte)_name.Length);
                writer.Write(_name);
            }
        }
    }
}
