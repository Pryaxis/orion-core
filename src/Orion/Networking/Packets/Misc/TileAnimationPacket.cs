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
using Orion.World.Tiles;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to show a tile animation.
    /// </summary>
    public sealed class TileAnimationPacket : Packet {
        /// <summary>
        /// Gets or sets the animation type.
        /// </summary>

        // TODO: implement enum for this.
        public short AnimationType { get; set; }

        /// <summary>
        /// Gets or sets the block type.
        /// </summary>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        internal override PacketType Type => PacketType.TileAnimation;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{AnimationType} @ ({TileX}, {TileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            AnimationType = reader.ReadInt16();
            BlockType = (BlockType)reader.ReadUInt16();
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(AnimationType);
            writer.Write((ushort)BlockType);
            writer.Write(TileX);
            writer.Write(TileY);
        }
    }
}
