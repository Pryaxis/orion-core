using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Orion.Items;
using Orion.World.TileEntities;
using Orion.World.Tiles;
using OTAPI.Tile;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to provide a world section.
    /// </summary>
    public sealed class UpdateWorldSectionPacket : TerrariaPacket {
        /// <inheritdoc />
        public override bool IsSentToClient => true;
        
        /// <inheritdoc />
        public override bool IsSentToServer => false;
        
        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdateWorldSection;

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
        /// Gets the width.
        /// </summary>
        public short Width { get; private set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public short Height { get; private set; }

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        public NetTile[,] Tiles { get; private set; }

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
        public IList<NetTileEntity> TileEntities { get; } = new List<NetTileEntity>();

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
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

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write((byte)(IsCompressed ? 1 : 0));
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
            Tiles = new NetTile[Width, Height];

            ReadTilesFromReaderImpl(reader);
            ReadChestsFromReaderImpl(reader);
            ReadSignsFromReaderImpl(reader);
            ReadTileEntitiesFromReaderImpl(reader);
        }

        private void ReadTilesFromReaderImpl(BinaryReader reader) {
            NetTile ReadTile(byte header, byte header2, byte header3) {
                var tile = new NetTile();
                if ((header & 2) == 2) {
                    tile.IsBlockActive = true;

                    tile.BlockType = (BlockType)((header & 32) == 32 ? reader.ReadUInt16() : reader.ReadByte());
                    if (Terraria.Main.tileFrameImportant[(int)tile.BlockType]) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
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

            NetTile previousTile = null;
            var runLength = 0;
            for (int y = 0; y < Height; ++y) {
                for (int x = 0; x < Width; ++x) {
                    if (runLength > 0) {
                        --runLength;
                        Tiles[x, y] = new NetTile();
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
                var type = reader.ReadByte();
                var index = reader.ReadInt32();
                var x = reader.ReadInt16();
                var y = reader.ReadInt16();

                switch (type) {
                case 0:
                    TileEntities.Add(new NetTargetDummy(index, x, y) {
                        NpcIndex = reader.ReadInt16(),
                    });
                    break;
                case 1:
                    TileEntities.Add(new NetItemFrame(index, x, y) {
                        ItemType = (ItemType)reader.ReadInt16(),
                        ItemPrefix = (ItemPrefix)reader.ReadByte(),
                        ItemStackSize = reader.ReadInt16(),
                    });
                    break;
                case 2:
                    TileEntities.Add(new NetLogicSensor(index, x, y) {
                        Type = (LogicSensorType)reader.ReadByte(),
                        IsActivated = reader.ReadBoolean(),
                    });
                    break;
                default:
                    throw new InvalidOperationException();
                }
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
            NetTile previousTile = null;
            var runLength = 0;

            void WritePartialTile(NetTile tile) {
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
                case NetTargetDummy targetDummy:
                    writer.Write((byte)0);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((short)targetDummy.NpcIndex);
                    break;
                case NetItemFrame itemFrame:
                    writer.Write((byte)1);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((short)itemFrame.ItemType);
                    writer.Write((byte)itemFrame.ItemPrefix);
                    writer.Write((short)itemFrame.ItemStackSize);
                    break;
                case NetLogicSensor logicSensor:
                    writer.Write((byte)2);
                    writer.Write(tileEntity.Index);
                    writer.Write((short)tileEntity.X);
                    writer.Write((short)tileEntity.Y);
                    writer.Write((byte)logicSensor.Type);
                    writer.Write(logicSensor.IsActivated);
                    break;
                }
            }
        }


        /// <summary>
        /// Represents a tile that is sent in a <see cref="UpdateWorldSectionPacket"/>.
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


        /// <summary>
        /// Represents a chest that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public sealed class NetChest : AnnotatableObject, IChest {
            private string _name;

            /// <inheritdoc />
            public int Index { get; set; }

            /// <inheritdoc />
            public int X { get; set; }

            /// <inheritdoc />
            public int Y { get; set; }

            /// <inheritdoc />
            public string Name {
                get => _name;
                set => _name = value ?? throw new ArgumentNullException(nameof(value));
            }

            /// <inheritdoc />
            public IItemList Items => null;
        }


        /// <summary>
        /// Represents a sign that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public sealed class NetSign : AnnotatableObject, ISign {
            private string _name;

            /// <inheritdoc />
            public int Index { get; set; }

            /// <inheritdoc />
            public int X { get; set; }

            /// <inheritdoc />
            public int Y { get; set; }

            /// <inheritdoc />
            public string Text {
                get => _name;
                set => _name = value ?? throw new ArgumentNullException(nameof(value));
            }
        }


        /// <summary>
        /// Represents a tile entity that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public abstract class NetTileEntity : AnnotatableObject, ITileEntity {
            /// <inheritdoc />
            public int Index { get; set; }

            /// <inheritdoc />
            public int X { get; set; }

            /// <inheritdoc />
            public int Y { get; set; }

            private protected NetTileEntity(int index, int x, int y) {
                Index = index;
                X = x;
                Y = y;
            }
        }


        /// <summary>
        /// Represents a target dummy that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public sealed class NetTargetDummy : NetTileEntity, ITargetDummy {
            /// <inheritdoc />
            public int NpcIndex { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="NetTargetDummy"/> class with the specified index and
            /// coordinates.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="x">The X coordinate.</param>
            /// <param name="y">The Y coordinate.</param>
            public NetTargetDummy(int index, int x, int y) : base(index, x, y) { }
        }


        /// <summary>
        /// Represents an item frame that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public sealed class NetItemFrame : NetTileEntity, IItemFrame {
            /// <inheritdoc />
            public ItemType ItemType { get; set; }

            /// <inheritdoc />
            public int ItemStackSize { get; set; }

            /// <inheritdoc />
            public ItemPrefix ItemPrefix { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="NetTargetDummy"/> class with the specified index and
            /// coordinates.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="x">The X coordinate.</param>
            /// <param name="y">The Y coordinate.</param>
            public NetItemFrame(int index, int x, int y) : base(index, x, y) { }
        }


        /// <summary>
        /// Represents a logic sensor that is sent in a <see cref="UpdateWorldSectionPacket"/>.
        /// </summary>
        public sealed class NetLogicSensor : NetTileEntity, ILogicSensor {
            /// <inheritdoc />
            public LogicSensorType Type { get; set; }

            /// <inheritdoc />
            public bool IsActivated { get; set; }

            /// <inheritdoc />
            public int Data { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="NetTargetDummy"/> class with the specified index and
            /// coordinates.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <param name="x">The X coordinate.</param>
            /// <param name="y">The Y coordinate.</param>
            public NetLogicSensor(int index, int x, int y) : base(index, x, y) { }
        }
    }
}
