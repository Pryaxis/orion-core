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

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to perform a tile modification.
    /// </summary>
    /// <remarks>This packet is sent when a player modifies a tile.</remarks>
    /// <seealso cref="TileModification"/>
    public sealed class TileModificationPacket : Packet {
        private TileModification _modification;
        private short _x;
        private short _y;
        private short _data;
        private byte _style;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TileModification;

        /// <summary>
        /// Gets or sets the tile modification.
        /// </summary>
        /// <value>The tile modification.</value>
        public TileModification Modification {
            get => _modification;
            set {
                _modification = value;
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

        /// <summary>
        /// Gets or sets the modification data.
        /// </summary>
        /// <value>The modification data.</value>
        /// <remarks>
        /// This property is only applicable if the modification is <see cref="TileModification.PlaceBlock"/>,
        /// <see cref="TileModification.PlaceWall"/>, <see cref="TileModification.SlopeBlock"/>.
        /// </remarks>
        public short Data {
            get => _data;
            set {
                _data = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the modification style.
        /// </summary>
        /// <value>The modification style.</value>
        /// <remarks>
        /// This property is only applicable if the modification is <see cref="TileModification.PlaceBlock"/>.
        /// </remarks>
        public byte Style {
            get => _style;
            set {
                _style = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _modification = (TileModification)reader.ReadByte();
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _data = reader.ReadInt16();
            _style = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((byte)_modification);
            writer.Write(_x);
            writer.Write(_y);
            writer.Write(_data);
            writer.Write(_style);
        }
    }
}
