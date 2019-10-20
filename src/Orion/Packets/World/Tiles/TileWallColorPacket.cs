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
    /// Packet sent to set a tile's wall color.
    /// </summary>
    /// <remarks>This packet is sent when a player paints a wall.</remarks>
    public sealed class TileWallColorPacket : Packet {
        private short _x;
        private short _y;
        private PaintColor _color;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TileWallColor;

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

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        /// <value>The wall color.</value>
        public PaintColor Color {
            get => _color;
            set {
                _color = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _color = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_x);
            writer.Write(_y);
            writer.Write((byte)_color);
        }
    }
}
