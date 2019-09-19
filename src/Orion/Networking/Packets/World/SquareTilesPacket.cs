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
using Orion.Networking.Tiles;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to set a square of tiles.
    /// </summary>
    public sealed class SquareTilesPacket : Packet {
        private short _squareSize;
        private byte _data;
        private short _tileX;
        private short _tileY;
        private NetworkTiles _tiles = new NetworkTiles(0, 0);

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || _tiles.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.SquareTiles;

        /// <summary>
        /// Gets or sets the size of the square.
        /// </summary>
        public short SquareSize {
            get => _squareSize;
            set {
                _squareSize = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the top-left tile's X coordinate.
        /// </summary>
        public short TileX {
            get => _tileX;
            set {
                _tileX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the top-left tile's Y coordinate.
        /// </summary>
        public short TileY {
            get => _tileY;
            set {
                _tileY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the tiles.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkTiles Tiles {
            get => _tiles;
            set {
                _tiles = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            Tiles.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{SquareSize}x{SquareSize} @ ({TileX}, {TileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var size = reader.ReadUInt16();
            if ((size & 32768) == 32768) {
                // This byte is basically useless, but we'll keep track of it anyways.
                _data = reader.ReadByte();
            }

            SquareSize = (short)(size & short.MaxValue);
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            Tiles = new NetworkTiles(SquareSize, SquareSize);

            NetworkTile ReadTile() {
                var tile = new NetworkTile();
                var header = (Terraria.BitsByte)reader.ReadByte();
                var header2 = (Terraria.BitsByte)reader.ReadByte();

                tile.HasRedWire = header[4];
                tile.IsBlockHalved = header[5];
                tile.HasActuator = header[6];
                tile.IsBlockActuated = header[7];
                tile.HasBlueWire = header2[0];
                tile.HasGreenWire = header2[1];
                tile.HasYellowWire = header2[7];

                if (header2[2]) {
                    tile.BlockColor = PaintColor.FromId(reader.ReadByte()) ??
                                      throw new PacketException("Paint color is invalid.");
                }

                if (header2[3]) {
                    tile.WallColor = PaintColor.FromId(reader.ReadByte()) ??
                                     throw new PacketException("Paint color is invalid.");
                }

                if (header[0]) {
                    tile.IsBlockActive = true;
                    tile.BlockType = BlockType.FromId(reader.ReadUInt16()) ??
                                     throw new PacketException("Block type is invalid.");
                    if (tile.BlockType.AreFramesImportant) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
                    } else {
                        tile.BlockFrameX = -1;
                        tile.BlockFrameY = -1;
                    }

                    byte slope = 0;
                    if (header2[4]) slope += 1;
                    if (header2[5]) slope += 2;
                    if (header2[6]) slope += 4;
                    tile.SlopeType = SlopeType.FromId(slope) ?? throw new PacketException("Slope type is invalid.");
                }

                if (header[2]) {
                    tile.WallType = WallType.FromId(reader.ReadByte()) ??
                                    throw new PacketException("Wall type is invalid.");
                }

                if (header[3]) {
                    tile.LiquidAmount = reader.ReadByte();
                    tile.LiquidType = LiquidType.FromId(reader.ReadByte()) ??
                                      throw new PacketException("Liquid type is invalid.");
                }

                return tile;
            }

            for (int x = 0; x < SquareSize; ++x) {
                for (int y = 0; y < SquareSize; ++y) {
                    Tiles[x, y] = ReadTile();
                }
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            if (_data > 0) {
                writer.Write((ushort)(SquareSize | 32768));
                writer.Write(_data);
            } else {
                writer.Write(SquareSize);
            }

            writer.Write(TileX);
            writer.Write(TileY);

            void WriteTile(Tile tile) {
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
                header2 = (byte)(header2 + tile.SlopeType.Id << 4);
                header2[7] = tile.HasYellowWire;

                writer.Write(header);
                writer.Write(header2);

                if (header[0]) {
                    writer.Write(tile.BlockType.Id);
                    if (tile.BlockType.AreFramesImportant) {
                        writer.Write(tile.BlockFrameX);
                        writer.Write(tile.BlockFrameY);
                    }
                }

                if (header[2]) {
                    writer.Write(tile.WallType.Id);
                }

                if (header[3]) {
                    writer.Write(tile.LiquidAmount);
                    writer.Write(tile.LiquidType.Id);
                }
            }

            for (int x = 0; x < SquareSize; ++x) {
                for (int y = 0; y < SquareSize; ++y) {
                    WriteTile(Tiles[x, y]);
                }
            }
        }
    }
}
