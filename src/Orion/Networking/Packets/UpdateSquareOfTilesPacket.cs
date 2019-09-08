using System;
using System.IO;
using Orion.World.Tiles;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a square of tiles.
    /// </summary>
    public sealed class UpdateSquareOfTilesPacket : Packet {
        private NetTile[,] _tiles;

        /// <summary>
        /// Gets or sets the size of the square.
        /// </summary>
        public short Size { get; set; }

        /// <summary>
        /// Gets or sets the liquid change type.
        /// </summary>
        public Type LiquidChangeType { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the tiles.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetTile[,] Tiles {
            get => _tiles;
            set => _tiles = value ?? throw new ArgumentNullException(nameof(value));
        }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            var size = reader.ReadUInt16();
            if ((size & 32768) == 32768) {
                LiquidChangeType = (Type)reader.ReadByte();
            }

            Size = (short)(size & short.MaxValue);
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Tiles = new NetTile[Size, Size];

            NetTile ReadTile() {
                var tile = new NetTile();
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

            for (int x = 0; x < Size; ++x) {
                for (int y = 0; y < Size; ++y) {
                    Tiles[x, y] = ReadTile();
                }
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            if (LiquidChangeType != Type.None) {
                writer.Write((ushort)(Size | 32768));
                writer.Write((byte)LiquidChangeType);
            } else {
                writer.Write(Size);
            }

            writer.Write(X);
            writer.Write(Y);

            void WriteTile(NetTile tile) {
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

            for (int x = 0; x < Size; ++x) {
                for (int y = 0; y < Size; ++y) {
                    WriteTile(Tiles[x, y]);
                }
            }
        }


        /// <summary>
        /// Specifies the liquid change type.
        /// </summary>
        public enum Type : byte {
#pragma warning disable 1591
            None = 0,
            LavaToWater,
            HoneyToWater,
            HoneyToLava,
#pragma warning restore 1591
        }


        /// <summary>
        /// Represents a tile that is sent in a <see cref="UpdateSquareOfTilesPacket"/>.
        /// </summary>
        public sealed class NetTile : Tile {
            /// <inheritdoc />
            public override BlockType BlockType { get; set; }

            /// <inheritdoc />
            public override WallType WallType { get; set; }

            /// <inheritdoc />
            public override byte LiquidAmount { get; set; }

            /// <inheritdoc />
            public override short TileHeader { get; set; }

            /// <inheritdoc />
            public override byte TileHeader2 { get; set; }

            /// <inheritdoc />
            public override byte TileHeader3 { get; set; }

            /// <inheritdoc />
            public override byte TileHeader4 { get; set; }

            /// <inheritdoc />
            public override short BlockFrameX { get; set; }

            /// <inheritdoc />
            public override short BlockFrameY { get; set; }
        }
    }
}
