using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to set a square of tiles.
    /// </summary>
    public sealed class SquareTilesPacket : Packet {
        private NetworkTile[,] _tiles;

        /// <summary>
        /// Gets or sets the size of the square.
        /// </summary>
        public short SquareSize { get; set; }

        /// <summary>
        /// Gets or sets the liquid change type.
        /// </summary>
        public LiquidChangeType LiquidChangeType { get; set; }

        /// <summary>
        /// Gets or sets the top-left tile's X coordinate.
        /// </summary>
        public short TileX { get; set; }

        /// <summary>
        /// Gets or sets the top-left tile's Y coordinate.
        /// </summary>
        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the tiles.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkTile[,] Tiles {
            get => _tiles;
            set => _tiles = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override PacketType Type => PacketType.SquareTiles;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{nameof(PacketType.SquareTiles)}[X={TileX}, Y={TileY}, S={SquareSize}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            var size = reader.ReadUInt16();
            if ((size & 32768) == 32768) {
                LiquidChangeType = (LiquidChangeType)reader.ReadByte();
            }

            SquareSize = (short)(size & short.MaxValue);
            TileX = reader.ReadInt16();
            TileY = reader.ReadInt16();
            Tiles = new NetworkTile[SquareSize, SquareSize];

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
                    tile.BlockColor = (PaintColor)reader.ReadByte();
                }

                if (header2[3]) {
                    tile.WallColor = (PaintColor)reader.ReadByte();
                }

                if (header[0]) {
                    tile.IsBlockActive = true;
                    tile.BlockType = (BlockType)reader.ReadUInt16();
                    if (Terraria.Main.tileFrameImportant[(int)tile.BlockType]) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
                    } else {
                        tile.BlockFrameX = -1;
                        tile.BlockFrameY = -1;
                    }

                    int slope = 0;
                    if (header2[4]) slope += 1;
                    if (header2[5]) slope += 2;
                    if (header2[6]) slope += 4;
                    tile.SlopeType = (SlopeType)slope;
                }

                if (header[2]) {
                    tile.WallType = (WallType)reader.ReadByte();
                }

                if (header[3]) {
                    tile.LiquidAmount = reader.ReadByte();
                    tile.LiquidType = (LiquidType)reader.ReadByte();
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
            if (LiquidChangeType != LiquidChangeType.None) {
                writer.Write((ushort)(SquareSize | 32768));
                writer.Write((byte)LiquidChangeType);
            } else {
                writer.Write(SquareSize);
            }

            writer.Write(TileX);
            writer.Write(TileY);

            void WriteTile(NetworkTile tile) {
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
                header2 = (byte)(header2 + (int)tile.SlopeType << 4);
                header2[7] = tile.HasYellowWire;

                writer.Write(header);
                writer.Write(header2);

                if (header[0]) {
                    writer.Write((ushort)tile.BlockType);
                    if (Terraria.Main.tileFrameImportant[(int)tile.BlockType]) {
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

            for (int x = 0; x < SquareSize; ++x) {
                for (int y = 0; y < SquareSize; ++y) {
                    WriteTile(Tiles[x, y]);
                }
            }
        }
    }
}
