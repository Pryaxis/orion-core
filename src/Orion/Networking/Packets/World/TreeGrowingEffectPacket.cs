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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to show a tree growing effect.
    /// </summary>
    public sealed class TreeGrowingEffectPacket : Packet {
        /// <summary>
        /// Gets or sets the tree's X coordinate.
        /// </summary>
        public short TreeX { get; set; }

        /// <summary>
        /// Gets or sets the tree's Y coordinate.
        /// </summary>
        public short TreeY { get; set; }

        /// <summary>
        /// Gets or sets the tree height.
        /// </summary>
        public byte TreeHeight { get; set; }

        /// <summary>
        /// Gets or sets the tree type.
        /// </summary>
        public short TreeType { get; set; }

        public override PacketType Type => PacketType.TreeGrowingEffect;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[({TreeX}, {TreeY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            reader.ReadByte();
            TreeX = reader.ReadInt16();
            TreeY = reader.ReadInt16();
            TreeHeight = reader.ReadByte();
            TreeType = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)1);
            writer.Write(TreeX);
            writer.Write(TreeY);
            writer.Write(TreeHeight);
            writer.Write(TreeType);
        }
    }
}
