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

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to set a tile's wall color.
    /// </summary>
    [PublicAPI]
    public sealed class TileWallColorPacket : Packet {
        private short _wallX;
        private short _wallY;
        private PaintColor _wallColor;

        /// <inheritdoc />
        public override PacketType Type => PacketType.TileWallColor;

        /// <summary>
        /// Gets or sets the wall's X coordinate.
        /// </summary>
        public short WallX {
            get => _wallX;
            set {
                _wallX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wall's Y coordinate.
        /// </summary>
        public short WallY {
            get => _wallY;
            set {
                _wallY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        public PaintColor WallColor {
            get => _wallColor;
            set {
                _wallColor = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{WallColor} @ ({WallX}, {WallY})]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _wallX = reader.ReadInt16();
            _wallY = reader.ReadInt16();
            _wallColor = (PaintColor)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_wallX);
            writer.Write(_wallY);
            writer.Write((byte)_wallColor);
        }
    }
}
