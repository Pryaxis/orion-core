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

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent to show a tree growing effect.
    /// </summary>
    public sealed class TreeGrowingEffectPacket : Packet {
        private byte _data;
        private short _x;
        private short _y;
        private byte _height;
        private short _treeType;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TreeGrowingEffect;

        /// <summary>
        /// Gets or sets the tree's X coordinate.
        /// </summary>
        /// <value>The tree's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree's Y coordinate.
        /// </summary>
        /// <value>The tree's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree height.
        /// </summary>
        /// <value>The tree height.</value>
        public byte Height {
            get => _height;
            set {
                _height = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tree type.
        /// </summary>
        /// <value>The tree type.</value>
        public short TreeType {
            get => _treeType;
            set {
                _treeType = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            // This byte is basically useless, but we'll keep track of it anyways.
            _data = reader.ReadByte();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _height = reader.ReadByte();
            _treeType = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_data);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_height);
            writer.Write(_treeType);
        }
    }
}
