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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to paint a block.
    /// </summary>
    public sealed class PaintBlockPacket : Packet {
        private short _blockX;
        private short _blockY;
        private PaintColor _blockColor;

        /// <inheritdoc />
        public override PacketType Type => PacketType.PaintBlock;

        /// <summary>
        /// Gets or sets the block's X coordinate.
        /// </summary>
        public short BlockX {
            get => _blockX;
            set {
                _blockX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the block's Y coordinate.
        /// </summary>
        public short BlockY {
            get => _blockY;
            set {
                _blockY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the block color.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public PaintColor BlockColor {
            get => _blockColor;
            set {
                _blockColor = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{BlockColor} @ ({BlockX}, {BlockY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            BlockX = reader.ReadInt16();
            BlockY = reader.ReadInt16();
            BlockColor = PaintColor.FromId(reader.ReadByte()) ?? throw new PacketException("Paint color is invalid.");
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(BlockX);
            writer.Write(BlockY);
            writer.Write(BlockColor.Id);
        }
    }
}
