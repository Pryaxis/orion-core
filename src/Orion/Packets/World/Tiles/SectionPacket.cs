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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Text;
using Orion.Packets.World.TileEntities;
using Orion.Utils;
using Orion.World.TileEntities;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Packet sent from the server to the client to set a section of the world.
    /// </summary>
    /// <remarks>This is sent in response to a <see cref="SectionRequestPacket"/>.</remarks>
    public sealed class SectionPacket : Packet {
        private bool _isCompressed;
        private int _startX;
        private int _startY;
        private short _width;
        private short _height;
        private NetworkTiles _tiles = new NetworkTiles(0, 0);
        private DirtiableList<NetworkTileEntity> _tileEntities = new DirtiableList<NetworkTileEntity>();

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _tiles.IsDirty || _tileEntities.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.Section;

        /// <summary>
        /// Gets or sets a value indicating whether the section is compressed.
        /// </summary>
        /// <value><see langword="true"/> if the section is compressed; otherwise, <see langword="false"/>.</value>
        public bool IsCompressed {
            get => _isCompressed;
            set {
                _isCompressed = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting tile's X coordinate.
        /// </summary>
        /// <value>The starting tile's X coordinate.</value>
        public int StartX {
            get => _startX;
            set {
                _startX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting tile's Y coordinate.
        /// </summary>
        /// <value>The starting tile's Y coordinate.</value>
        public int StartY {
            get => _startY;
            set {
                _startY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's width.
        /// </summary>
        /// <value>The section's width.</value>
        public short Width {
            get => _width;
            set {
                _width = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's height.
        /// </summary>
        /// <value>The section's height.</value>
        public short Height {
            get => _height;
            set {
                _height = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's tiles.
        /// </summary>
        /// <value>The section's tiles.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkTiles Tiles {
            get => _tiles;
            set {
                _tiles = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the section's tile entities.
        /// </summary>
        /// <value>The section's tile entities.</value>
        public IList<NetworkTileEntity> TileEntities => _tileEntities;

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _tiles.Clean();
            _tileEntities.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _isCompressed = reader.ReadByte() == 1;
            if (!_isCompressed) {
                ReadFromReaderImpl(reader);
                return;
            }

            using var deflateStream = new DeflateStream(reader.BaseStream, CompressionMode.Decompress, true);
            ReadFromReaderImpl(new BinaryReader(deflateStream, Encoding.UTF8, true));
        }

        [SuppressMessage("Code Quality", "IDE0068:Use recommended dispose pattern",
            Justification = "BinaryWriter does not need to be disposed")]
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope",
            Justification = "BinaryWriter does not need to be disposed")]
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_isCompressed);
            if (!_isCompressed) {
                WriteToWriterImpl(writer);
                return;
            }

            var stream = new MemoryStream();
            WriteToWriterImpl(new BinaryWriter(stream, Encoding.UTF8, true));

            var compressedStream = new MemoryStream();
            var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true);
            stream.Position = 0;
            stream.CopyTo(deflateStream);
            deflateStream.Close();

            compressedStream.Position = 0;
            compressedStream.CopyTo(writer.BaseStream);
        }

        private void ReadFromReaderImpl(BinaryReader reader) {
            _startX = reader.ReadInt32();
            _startY = reader.ReadInt32();
            _width = reader.ReadInt16();
            _height = reader.ReadInt16();
            _tiles = new NetworkTiles(Width, Height);

            ReadTilesFromReaderImpl(reader);
            ReadTileEntitiesFromReaderImpl(reader);
        }

        private void ReadTilesFromReaderImpl(BinaryReader reader) {
            void ReadTile(ref Tile tile, byte header, byte header2, byte header3) {
                if ((header & 2) == 2) {
                    tile.IsBlockActive = true;

                    tile.BlockType = (BlockType)((header & 32) == 32 ? reader.ReadUInt16() : reader.ReadByte());
                    if (tile.BlockType.AreFramesImportant()) {
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
                if (blockShape != 0) {
                    if (blockShape == 1) {
                        tile.IsBlockHalved = true;
                    } else {
                        tile.Slope = (Slope)(blockShape - 1);
                    }
                }

                tile.HasActuator = (header3 & 2) == 2;
                tile.IsBlockActuated = (header3 & 4) == 4;
                tile.HasYellowWire = (header3 & 32) == 32;
            }

            int ReadRunLength(byte header) => ((header & 192) >> 6) switch {
                0 => 0,
                1 => reader.ReadByte(),
                2 => reader.ReadInt16(),
                _ => throw new InvalidOperationException()
            };

            var previousTile = new Tile();
            var runLength = 0;
            for (var y = 0; y < Height; ++y) {
                for (var x = 0; x < Width; ++x) {
                    if (runLength > 0) {
                        --runLength;
                        _tiles[x, y] = previousTile;
                        continue;
                    }

                    var header = reader.ReadByte();
                    var header2 = (header & 1) == 1 ? reader.ReadByte() : (byte)0;
                    var header3 = (header2 & 1) == 1 ? reader.ReadByte() : (byte)0;

                    ReadTile(ref _tiles[x, y], header, header2, header3);
                    runLength = ReadRunLength(header);
                    previousTile = _tiles[x, y];
                }
            }

            _tiles.Clean();
        }

        private void ReadTileEntitiesFromReaderImpl(BinaryReader reader) {
            var sectionTileEntities = new List<NetworkTileEntity>();

            void ReadTileEntities(TileEntityType? typeHint = null) {
                var number = reader.ReadInt16();
                for (var i = 0; i < number; ++i) {
                    sectionTileEntities.Add(NetworkTileEntity.ReadFromReader(reader, null, typeHint));
                }
            }

            ReadTileEntities(TileEntityType.Chest);
            ReadTileEntities(TileEntityType.Sign);
            ReadTileEntities();

            _tileEntities = new DirtiableList<NetworkTileEntity>(sectionTileEntities);
        }

        private void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write(_startX);
            writer.Write(_startY);
            writer.Write(_width);
            writer.Write(_height);

            WriteTilesToWriterImpl(writer);
            WriteTileEntitiesToWriterImpl(writer);
        }

        private void WriteTilesToWriterImpl(BinaryWriter writer) {
            var buffer = new byte[15];
            var headerIndex = 0;
            byte header = 0;
            var bodyIndex = 0;
            Tile? previousTile = null;
            var runLength = 0;

            void WritePartialTile(ref Tile tile) {
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

                    if (tile.BlockType.AreFramesImportant()) {
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
                    header |= (byte)(((byte)tile.LiquidType + 1) << 3);
                    buffer[bodyIndex++] = tile.LiquidAmount;
                }

                header2 |= (byte)(tile.HasRedWire ? 2 : 0);
                header2 |= (byte)(tile.HasGreenWire ? 4 : 0);
                header2 |= (byte)(tile.HasBlueWire ? 8 : 0);

                if (tile.IsBlockHalved) {
                    header2 |= 16;
                } else if (tile.Slope != Slope.None) {
                    header2 |= (byte)(((byte)tile.Slope + 1) << 4);
                }

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

            for (var y = 0; y < _height; ++y) {
                for (var x = 0; x < _width; ++x) {
                    ref var tile = ref _tiles[x, y];
                    if (previousTile != null && tile.IsTheSameAs(previousTile.Value)) {
                        ++runLength;
                        continue;
                    }

                    if (previousTile != null) {
                        WriteRunLength();
                    }

                    WritePartialTile(ref tile);
                    previousTile = tile;
                }
            }

            WriteRunLength();
        }

        private void WriteTileEntitiesToWriterImpl(BinaryWriter writer) {
            var chests = new List<NetworkTileEntity>();
            var signs = new List<NetworkTileEntity>();
            var notChestsOrSigns = new List<NetworkTileEntity>();
            foreach (var tileEntity in _tileEntities) {
                switch (tileEntity) {
                case NetworkChest _:
                    chests.Add(tileEntity);
                    break;
                case NetworkSign _:
                    signs.Add(tileEntity);
                    break;
                default:
                    notChestsOrSigns.Add(tileEntity);
                    break;
                }
            }

            void WriteTileEntities(ICollection<NetworkTileEntity> tileEntities) {
                writer.Write((short)tileEntities.Count);
                foreach (var tileEntity in tileEntities) {
                    tileEntity.WriteToWriter(writer, true);
                }
            }

            WriteTileEntities(chests);
            WriteTileEntities(signs);
            WriteTileEntities(notChestsOrSigns);
        }
    }
}
