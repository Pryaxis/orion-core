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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to paint a wall.
    /// </summary>
    public sealed class PaintWallPacket : Packet {
        /// <summary>
        /// Gets or sets the wall's X coordinate.
        /// </summary>
        public short WallX { get; set; }

        /// <summary>
        /// Gets or sets the wall's Y coordinate.
        /// </summary>
        public short WallY { get; set; }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        public PaintColor WallColor { get; set; }

        internal override PacketType Type => PacketType.PaintWall;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{WallColor} @ ({WallX}, {WallY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            WallX = reader.ReadInt16();
            WallY = reader.ReadInt16();
            WallColor = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(WallX);
            writer.Write(WallY);
            writer.Write((byte)WallColor);
        }
    }
}
