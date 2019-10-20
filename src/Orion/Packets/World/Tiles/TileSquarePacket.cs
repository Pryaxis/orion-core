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

using System;
using System.IO;
using Orion.Items;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent to set a square of tiles.
    /// </summary>
    /// <remarks>
    /// This packet is sent when a player needs to "unnaturally" modify tiles: e.g., if blocks need to be replaced,
    /// such as when using a <see cref="ItemType.Clentaminator"/>
    /// </remarks>
    public sealed class TileSquarePacket : Packet {
        private short _size;
        private byte _data;
        private short _x;
        private short _y;
        private NetworkTiles _tiles = new NetworkTiles(0, 0);

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _tiles.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.TileSquare;

        /// <summary>
        /// Gets or sets the square size.
        /// </summary>
        /// <value>The square size.</value>
        public short Size {
            get => _size;
            set {
                _size = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the top-left tile's X coordinate.
        /// </summary>
        /// <value>The top-left tile's X coordinate.</value>
        public short X {
            get => _x;
            set {
                _x = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the top-left tile's Y coordinate.
        /// </summary>
        /// <value>The top-left tile's Y coordinate.</value>
        public short Y {
            get => _y;
            set {
                _y = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tiles.
        /// </summary>
        /// <value>The tiles.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkTiles Tiles {
            get => _tiles;
            set {
                _tiles = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _tiles.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var size = reader.ReadUInt16();
            if ((size & 32768) == 32768) {
                // This byte is basically useless, but we'll keep track of it anyways.
                _data = reader.ReadByte();
            }

            _size = (short)(size & short.MaxValue);
            _x = reader.ReadInt16();
            _y = reader.ReadInt16();
            _tiles = new NetworkTiles(_size, _size);

            void ReadTile(ref Tile tile) {
                Terraria.BitsByte header = reader.ReadByte();
                Terraria.BitsByte header2 = reader.ReadByte();

                tile.HasRedWire = header[4];
                tile.IsBlockHalved = header[5];
                tile.HasActuator = header[6];
                tile.IsBlockActuated = header[7];
                tile.HasBlueWire = header2[0];
                tile.HasGreenWire = header2[1];
                tile.HasYellowWire = header2[7];

                if (header2[2]) {
                    tile.BlockColor = (PaintColor)reader.ReadByte();
                }

                if (header2[3]) {
                    tile.WallColor = (PaintColor)reader.ReadByte();
                }

                if (header[0]) {
                    tile.IsBlockActive = true;
                    tile.BlockType = (BlockType)reader.ReadUInt16();
                    if (tile.BlockType.AreFramesImportant()) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
                    } else {
                        tile.BlockFrameX = -1;
                        tile.BlockFrameY = -1;
                    }

                    byte slope = 0;
                    if (header2[4]) {
                        slope += 1;
                    }

                    if (header2[5]) {
                        slope += 2;
                    }

                    if (header2[6]) {
                        slope += 4;
                    }

                    tile.Slope = (Slope)slope;
                }

                if (header[2]) {
                    tile.WallType = (WallType)reader.ReadByte();
                }

                if (header[3]) {
                    tile.LiquidAmount = reader.ReadByte();
                    tile.LiquidType = (LiquidType)reader.ReadByte();
                }
            }

            for (var x = 0; x < Size; ++x) {
                for (var y = 0; y < Size; ++y) {
                    ReadTile(ref _tiles[x, y]);
                }
            }

            _tiles.Clean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            if (_data > 0) {
                writer.Write((ushort)(_size | 32768));
                writer.Write(_data);
            } else {
                writer.Write(_size);
            }

            writer.Write(_x);
            writer.Write(_y);

            void WriteTile(ref Tile tile) {
                Terraria.BitsByte header = 0;
                Terraria.BitsByte header2 = 0;
                header[0] = tile.IsBlockActive;
                header[2] = tile.WallType != WallType.None;
                header[3] = tile.LiquidAmount > 0;
                header[4] = tile.HasRedWire;
                header[5] = tile.IsBlockHalved;
                header[6] = tile.HasActuator;
                header[7] = tile.IsBlockActuated;
                header2[0] = tile.HasBlueWire;
                header2[1] = tile.HasGreenWire;
                header2[2] = tile.BlockColor != PaintColor.None;
                header2[3] = tile.WallColor != PaintColor.None;
                header2 = (byte)(header2 + (byte)tile.Slope << 4);
                header2[7] = tile.HasYellowWire;

                writer.Write(header);
                writer.Write(header2);

                if (header[0]) {
                    writer.Write((ushort)tile.BlockType);
                    if (tile.BlockType.AreFramesImportant()) {
                        writer.Write(tile.BlockFrameX);
                        writer.Write(tile.BlockFrameY);
                    }
                }

                if (header[2]) {
                    writer.Write((byte)tile.WallType);
                }

                if (header[3]) {
                    writer.Write(tile.LiquidAmount);
                    writer.Write((byte)tile.LiquidType);
                }
            }

            for (var x = 0; x < Size; ++x) {
                for (var y = 0; y < Size; ++y) {
                    WriteTile(ref _tiles[x, y]);
                }
            }
        }
    }
}
