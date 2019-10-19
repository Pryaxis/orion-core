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
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to show a tile animation such as a fake chest opening or a mushroom statue being powered.
    /// </summary>
    public sealed class TileAnimationPacket : Packet {
        private short _animationType;
        private BlockType _blockType;
        private short _x;
        private short _y;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TileAnimation;

        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>
        /// <value>The animation type.</value>
        // TODO: implement enum for this.
        public short AnimationType {
            get => _animationType;
            set {
                _animationType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the block type.
        /// </summary>
        /// <value>The block type.</value>
        public BlockType BlockType {
            get => _blockType;
            set {
                _blockType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        /// <value>The tile's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        /// <value>The tile's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _animationType = reader.ReadInt16();
            _blockType = (BlockType)reader.ReadUInt16();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_animationType);
            writer.Write((ushort)_blockType);
            writer.Write(_x);
            writer.Write(_y);
        }
    }
}
