using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Orion.Networking.Packets.World.TileEntities;
using Orion.World.Tiles;
using OTAPI.Tile;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to provide a section of the world. This is sent in response to a
    /// <see cref="RequestSectionPacket"/>.
    /// </summary>
    public sealed class SectionPacket : Packet {
        private NetworkTile[,] _tiles;

        /// <summary>
        /// Gets or sets a value indicating whether the section data is compressed.
        /// </summary>
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Gets or sets the starting X coordinate.
        /// </summary>
        public int StartX { get; set; }

        /// <summary>
        /// Gets or sets the starting Y coordinate.
        /// </summary>
        public int StartY { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public short Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public short Height { get; set; }

        /// <summary>
        /// Gets or sets the tiles.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkTile[,] Tiles {
            get => _tiles;
            set => _tiles = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the chests.
        /// </summary>
        public IList<NetChest> Chests { get; } = new List<NetChest>();

        /// <summary>
        /// Gets the signs.
        /// </summary>
        public IList<NetSign> Signs { get; } = new List<NetSign>();

        /// <summary>
        /// Gets the tile entities.
        /// </summary>
        public IList<NetworkTileEntity> TileEntities { get; } = new List<NetworkTileEntity>();

        private protected override PacketType Type => PacketType.Section;

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            IsCompressed = reader.ReadByte() == 1;
            if (!IsCompressed) {
                ReadFromReaderImpl(reader);
                return;
            }

            using (var deflateStream = new DeflateStream(reader.BaseStream, CompressionMode.Decompress, true))
            using (var deflateReader = new BinaryReader(deflateStream, Encoding.UTF8, true)) {
                ReadFromReaderImpl(deflateReader);
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(IsCompressed);
            if (!IsCompressed) {
                WriteToWriterImpl(writer);
                return;
            }

            var stream = new MemoryStream();
            WriteToWriterImpl(new BinaryWriter(stream, Encoding.UTF8, true));

            var compressedStream = new MemoryStream();
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true)) {
                stream.Position = 0;
                stream.CopyTo(deflateStream);
                deflateStream.Close();
            }

            compressedStream.Position = 0;
            compressedStream.CopyTo(writer.BaseStream);
        }

        private void ReadFromReaderImpl(BinaryReader reader) {
            StartX = reader.ReadInt32();
            StartY = reader.ReadInt32();
            Width = reader.ReadInt16();
            Height = reader.ReadInt16();
            Tiles = new NetworkTile[Width, Height];

            ReadTilesFromReaderImpl(reader);
            ReadChestsFromReaderImpl(reader);
            ReadSignsFromReaderImpl(reader);
            ReadTileEntitiesFromReaderImpl(reader);
        }

        private void ReadTilesFromReaderImpl(BinaryReader reader) {
            NetworkTile ReadTile(byte header, byte header2, byte header3) {
                var tile = new NetworkTile();
                if ((header & 2) == 2) {
                    tile.IsBlockActive = true;

                    tile.BlockType = (BlockType)((header & 32) == 32 ? reader.ReadUInt16() : reader.ReadByte());
                    if (Terraria.Main.tileFrameImportant[(int)tile.BlockType]) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
                    } else {
                        tile.BlockFrameX = -1;
                        tile.BlockFrameY = -1;
                    }

                    if ((header3 & 8) == 8) {
                        tile.BlockColor = (PaintColor)reader.ReadByte();
                    }
                }

                if ((header & 4) == 4) {
                    tile.WallType = (WallType)reader.ReadByte();
                    if ((header3 & 16) == 16) {
                        tile.WallColor = (PaintColor)reader.ReadByte();
                    }
                }

                var liquidType = (header & 24) >> 3;
                if (liquidType > 0) {
                    tile.LiquidAmount = reader.ReadByte();
                    tile.LiquidType = (LiquidType)(liquidType - 1);
                }

                tile.HasRedWire = (header2 & 2) == 2;
                tile.HasBlueWire = (header2 & 4) == 4;
                tile.HasGreenWire = (header2 & 8) == 8;

                var blockShape = (header2 & 112) >> 4;
                if (blockShape != 0 && Terraria.Main.tileSolid[(int)tile.BlockType]) {
                    if (blockShape == 1) {
                        tile.IsBlockHalved = true;
                    } else {
                        tile.SlopeType = (SlopeType)(blockShape - 1);
                    }
                }

                tile.HasActuator = (header3 & 2) == 2;
                tile.IsBlockActuated = (header3 & 4) == 4;
                tile.HasYellowWire = (header3 & 32) == 32;
                return tile;
            }

            int ReadRunLength(byte header) {
                switch ((header & 192) >> 6) {
                case 0: return 0;
                case 1: return reader.ReadByte();
                case 2: return reader.ReadInt16();
                default: throw new InvalidOperationException();
                }
            }

            NetworkTile previousTile = null;
            var runLength = 0;
            for (int y = 0; y < Height; ++y) {
                for (int x = 0; x < Width; ++x) {
                    if (runLength > 0) {
                        --runLength;
                        Tiles[x, y] = new NetworkTile();
                        ((ITile)Tiles[x, y]).CopyFrom(previousTile);
                        continue;
                    }

                    byte header = reader.ReadByte();
                    byte header2 = (header & 1) == 1 ? reader.ReadByte() : (byte)0;
                    byte header3 = (header2 & 1) == 1 ? reader.ReadByte() : (byte)0;

                    var tile = ReadTile(header, header2, header3);
                    Tiles[x, y] = tile;
                    runLength = ReadRunLength(header);
                    previousTile = tile;
                }
            }
        }

        private void ReadChestsFromReaderImpl(BinaryReader reader) {
            var numberOfChests = reader.ReadInt16();
            for (var i = 0; i < numberOfChests; ++i) {
                Chests.Add(new NetChest {
                    Index = reader.ReadInt16(),
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    Name = reader.ReadString(),
                });
            }
        }

        private void ReadSignsFromReaderImpl(BinaryReader reader) {
            var numberOfSigns = reader.ReadInt16();
            for (var i = 0; i < numberOfSigns; ++i) {
                Signs.Add(new NetSign {
                    Index = reader.ReadInt16(),
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    Text = reader.ReadString(),
                });
            }
        }

        private void ReadTileEntitiesFromReaderImpl(BinaryReader reader) {
            var numberOfTileEntities = reader.ReadInt16();
            for (var i = 0; i < numberOfTileEntities; ++i) {
                TileEntities.Add(NetworkTileEntity.FromReader(reader, true));
            }
        }

        private void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write(StartX);
            writer.Write(StartY);
            writer.Write(Width);
            writer.Write(Height);

            WriteTilesToWriterImpl(writer);
            WriteChestsToWriterImpl(writer);
            WriteSignsToWriterImpl(writer);
            WriteTileEntitiesToWriterImpl(writer);
        }

        private void WriteTilesToWriterImpl(BinaryWriter writer) {
            var buffer = new byte[15];
            var headerIndex = 0;
            byte header = 0;
            var bodyIndex = 0;
            NetworkTile previousTile = null;
            var runLength = 0;

            void WritePartialTile(NetworkTile tile) {
                header = 0;
                byte header2 = 0;
                byte header3 = 0;

                bodyIndex = 3;
                if (tile.IsBlockActive) {
                    header |= 2;

                    var type = (ushort)tile.BlockType;
                    buffer[bodyIndex++] = (byte)type;
                    if (type > byte.MaxValue) {
                        header |= 32;
                        buffer[bodyIndex++] = (byte)(type >> 8);
                    }

                    if (Terraria.Main.tileFrameImportant[type]) {
                        buffer[bodyIndex++] = (byte)tile.BlockFrameX;
                        buffer[bodyIndex++] = (byte)(tile.BlockFrameX >> 8);
                        buffer[bodyIndex++] = (byte)tile.BlockFrameY;
                        buffer[bodyIndex++] = (byte)(tile.BlockFrameY >> 8);
                    }

                    if (tile.BlockColor != PaintColor.None) {
                        header3 |= 8;
                        buffer[bodyIndex++] = (byte)tile.BlockColor;
                    }
                }

                if (tile.WallType != WallType.None) {
                    header |= 4;
                    buffer[bodyIndex++] = (byte)tile.WallType;

                    if (tile.WallColor != PaintColor.None) {
                        header3 |= 16;
                        buffer[bodyIndex++] = (byte)tile.WallColor;
                    }
                }

                if (tile.LiquidAmount > 0) {
                    header |= (byte)(((int)tile.LiquidType + 1) << 3);
                    buffer[bodyIndex++] = tile.LiquidAmount;
                }

                header2 |= (byte)(tile.HasRedWire ? 2 : 0);
                header2 |= (byte)(tile.HasGreenWire ? 4 : 0);
                header2 |= (byte)(tile.HasBlueWire ? 8 : 0);
                header2 |= (byte)(((ITile)tile).blockType() << 4);

                header3 |= (byte)(tile.HasActuator ? 2 : 0);
                header3 |= (byte)(tile.IsBlockActuated ? 4 : 0);
                header3 |= (byte)(tile.HasYellowWire ? 32 : 0);

                headerIndex = 2;
                if (header3 > 0) {
                    header2 |= 1;
                    buffer[headerIndex--] = header3;
                }

                if (header2 > 0) {
                    header |= 1;
                    buffer[headerIndex--] = header2;
                }
            }

            void WriteRunLength() {
                if (runLength > 0) {
                    buffer[bodyIndex++] = (byte)runLength;

                    if (runLength > byte.MaxValue) {
                        header |= 128;
                        buffer[bodyIndex++] = (byte)(runLength >> 8);
                    } else {
                        header |= 64;
                    }
                }

                buffer[headerIndex] = header;
                writer.Write(buffer, headerIndex, bodyIndex - headerIndex);
                runLength = 0;
            }

            for (int y = 0; y < Height; ++y) {
                for (int x = 0; x < Width; ++x) {
                    var tile = Tiles[x, y];
                    if (((ITile)tile).isTheSameAs(previousTile)) {
                        ++runLength;
                        continue;
                    }

                    if (previousTile != null) {
                        WriteRunLength();
                    }

                    WritePartialTile(tile);
                    previousTile = tile;
                }
            }

            WriteRunLength();
        }

        private void WriteChestsToWriterImpl(BinaryWriter writer) {
            writer.Write((short)Chests.Count);
            foreach (var chest in Chests) {
                writer.Write((short)chest.Index);
                writer.Write((short)chest.X);
                writer.Write((short)chest.Y);
                writer.Write(chest.Name);
            }
        }

        private void WriteSignsToWriterImpl(BinaryWriter writer) {
            writer.Write((short)Signs.Count);
            foreach (var sign in Signs) {
                writer.Write((short)sign.Index);
                writer.Write((short)sign.X);
                writer.Write((short)sign.Y);
                writer.Write(sign.Text);
            }
        }

        private void WriteTileEntitiesToWriterImpl(BinaryWriter writer) {
            writer.Write((short)TileEntities.Count);
            foreach (var tileEntity in TileEntities) {
                switch (tileEntity) {
                case NetworkTargetDummy targetDummy:
                    writer.Write((byte)0);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((short)targetDummy.NpcIndex);
                    break;
                case NetworkItemFrame itemFrame:
                    writer.Write((byte)1);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((short)itemFrame.ItemType);
                    writer.Write((byte)itemFrame.ItemPrefix);
                    writer.Write((short)itemFrame.ItemStackSize);
                    break;
                case NetworkLogicSensor logicSensor:
                    writer.Write((byte)2);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((byte)logicSensor.SensorType);
                    writer.Write(logicSensor.IsActivated);
                    break;
                }
            }
        }
    }
}
