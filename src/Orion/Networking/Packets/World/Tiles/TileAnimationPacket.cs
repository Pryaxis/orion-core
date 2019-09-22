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
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to show a tile animation.
    /// </summary>
    [PublicAPI]
    public sealed class TileAnimationPacket : Packet {
        private short _animationType;
        private BlockType _blockType;
        private short _tileX;
        private short _tileY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileAnimation;

        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>

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

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{AnimationType} @ ({TileX}, {TileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _animationType = reader.ReadInt16();
            _blockType = (BlockType)reader.ReadUInt16();
            _tileX = reader.ReadInt16();
            _tileY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_animationType);
            writer.Write((ushort)_blockType);
            writer.Write(_tileX);
            writer.Write(_tileY);
        }
    }
}
