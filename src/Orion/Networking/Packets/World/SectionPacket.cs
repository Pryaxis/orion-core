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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Orion.Events;
using Orion.Networking.TileEntities;
using Orion.Networking.Tiles;
using Orion.World.TileEntities;
using Orion.World.Tiles;
using OTAPI.Tile;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent from the server to the client to provide a section of the world. This is sent in response to a
    /// <see cref="RequestSectionPacket"/>.
    /// </summary>
    public sealed class SectionPacket : Packet {
        private readonly IList<NetworkTileEntity> _sectionTileEntities = new List<NetworkTileEntity>();
        private bool _isSectionCompressed;
        private int _startTileX;
        private int _startTileY;
        private short _sectionWidth;
        private short _sectionHeight;
        private NetworkTiles _sectionTiles = new NetworkTiles(0, 0);

        /// <inheritdoc />
        public override PacketType Type => PacketType.Section;

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || SectionTiles.IsDirty || SectionTileEntities.IsDirty;

        /// <summary>
        /// Gets or sets a value indicating whether the section is compressed.
        /// </summary>
        public bool IsSectionCompressed {
            get => _isSectionCompressed;
            set {
                _isSectionCompressed = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting tile's X coordinate.
        /// </summary>
        public int StartTileX {
            get => _startTileX;
            set {
                _startTileX = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the starting tile's Y coordinate.
        /// </summary>
        public int StartTileY {
            get => _startTileY;
            set {
                _startTileY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's width.
        /// </summary>
        public short SectionWidth {
            get => _sectionWidth;
            set {
                _sectionWidth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's height.
        /// </summary>
        public short SectionHeight {
            get => _sectionHeight;
            set {
                _sectionHeight = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the section's tiles.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkTiles SectionTiles {
            get => _sectionTiles;
            set {
                _sectionTiles = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the section's tile entities.
        /// </summary>
        public TileEntities SectionTileEntities { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionPacket"/> class.
        /// </summary>
        public SectionPacket() {
            SectionTileEntities = new TileEntities(_sectionTileEntities);
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            SectionTiles.Clean();
            SectionTileEntities.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() =>
            $"{Type}[{SectionWidth}x{SectionHeight} @ ({StartTileX}, {StartTileY}), ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            IsSectionCompressed = reader.ReadByte() == 1;
            if (!IsSectionCompressed) {
                ReadFromReaderImpl(reader);
                return;
            }

            using (var deflateStream = new DeflateStream(reader.BaseStream, CompressionMode.Decompress, true))
            using (var deflateReader = new BinaryReader(deflateStream, Encoding.UTF8, true)) {
                ReadFromReaderImpl(deflateReader);
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(IsSectionCompressed);
            if (!IsSectionCompressed) {
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
            StartTileX = reader.ReadInt32();
            StartTileY = reader.ReadInt32();
            SectionWidth = reader.ReadInt16();
            SectionHeight = reader.ReadInt16();
            SectionTiles = new NetworkTiles(SectionWidth, SectionHeight);

            ReadTilesFromReaderImpl(reader);
            ReadTileEntitiesFromReaderImpl(reader);
        }

        private void ReadTilesFromReaderImpl(BinaryReader reader) {
            NetworkTile ReadTile(byte header, byte header2, byte header3) {
                var tile = new NetworkTile();
                if ((header & 2) == 2) {
                    tile.IsBlockActive = true;

                    tile.BlockType = BlockType.FromId((header & 32) == 32 ? reader.ReadUInt16() : reader.ReadByte()) ??
                                     throw new PacketException("Block type is invalid.");
                    if (tile.BlockType.AreFramesImportant) {
                        tile.BlockFrameX = reader.ReadInt16();
                        tile.BlockFrameY = reader.ReadInt16();
                    } else {
                        tile.BlockFrameX = -1;
                        tile.BlockFrameY = -1;
                    }

                    if ((header3 & 8) == 8) {
                        tile.BlockColor = PaintColor.FromId(reader.ReadByte()) ??
                                          throw new PacketException("Paint color is invalid.");
                    }
                }

                if ((header & 4) == 4) {
                    tile.WallType = WallType.FromId(reader.ReadByte()) ??
                                    throw new PacketException("Wall type is invalid.");
                    if ((header3 & 16) == 16) {
                        tile.WallColor = PaintColor.FromId(reader.ReadByte()) ??
                                         throw new PacketException("Paint color is invalid.");
                    }
                }

                var liquidType = (header & 24) >> 3;
                if (liquidType > 0) {
                    tile.LiquidAmount = reader.ReadByte();
                    tile.LiquidType = LiquidType.FromId((byte)(liquidType - 1)) ??
                                      throw new PacketException("Liquid type is invalid.");
                }

                tile.HasRedWire = (header2 & 2) == 2;
                tile.HasBlueWire = (header2 & 4) == 4;
                tile.HasGreenWire = (header2 & 8) == 8;

                // TODO: there was a tileSolid check here, but it doesn't seem necessary?
                var blockShape = (header2 & 112) >> 4;
                if (blockShape != 0) {
                    if (blockShape == 1) {
                        tile.IsBlockHalved = true;
                    } else {
                        tile.SlopeType = SlopeType.FromId((byte)(blockShape - 1)) ??
                                         throw new PacketException("Slope type is invalid.");
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
            for (int y = 0; y < SectionHeight; ++y) {
                for (int x = 0; x < SectionWidth; ++x) {
                    if (runLength > 0) {
                        --runLength;
                        SectionTiles[x, y] = new NetworkTile();
                        ((ITile)SectionTiles[x, y]).CopyFrom(previousTile);
                        continue;
                    }

                    byte header = reader.ReadByte();
                    byte header2 = (header & 1) == 1 ? reader.ReadByte() : (byte)0;
                    byte header3 = (header2 & 1) == 1 ? reader.ReadByte() : (byte)0;

                    var tile = ReadTile(header, header2, header3);
                    SectionTiles[x, y] = tile;
                    runLength = ReadRunLength(header);
                    previousTile = tile;
                }
            }
        }

        private void ReadTileEntitiesFromReaderImpl(BinaryReader reader) {
            void ReadTileEntities(TileEntityType typeHint = null) {
                var number = reader.ReadInt16();
                for (var i = 0; i < number; ++i) {
                    SectionTileEntities.Add(NetworkTileEntity.FromReader(reader, true, typeHint));
                }
            }

            ReadTileEntities(TileEntityType.Chest);
            ReadTileEntities(TileEntityType.Sign);
            ReadTileEntities();
        }

        private void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write(StartTileX);
            writer.Write(StartTileY);
            writer.Write(SectionWidth);
            writer.Write(SectionHeight);

            WriteTilesToWriterImpl(writer);
            WriteTileEntitiesToWriterImpl(writer);
        }

        private void WriteTilesToWriterImpl(BinaryWriter writer) {
            var buffer = new byte[15];
            var headerIndex = 0;
            byte header = 0;
            var bodyIndex = 0;
            NetworkTile previousTile = null;
            var runLength = 0;

            void WritePartialTile(Tile tile) {
                header = 0;
                byte header2 = 0;
                byte header3 = 0;

                bodyIndex = 3;
                if (tile.IsBlockActive) {
                    header |= 2;

                    var type = tile.BlockType.Id;
                    buffer[bodyIndex++] = (byte)type;
                    if (type > byte.MaxValue) {
                        header |= 32;
                        buffer[bodyIndex++] = (byte)(type >> 8);
                    }

                    if (tile.BlockType.AreFramesImportant) {
                        buffer[bodyIndex++] = (byte)tile.BlockFrameX;
                        buffer[bodyIndex++] = (byte)(tile.BlockFrameX >> 8);
                        buffer[bodyIndex++] = (byte)tile.BlockFrameY;
                        buffer[bodyIndex++] = (byte)(tile.BlockFrameY >> 8);
                    }

                    if (tile.BlockColor != PaintColor.None) {
                        header3 |= 8;
                        buffer[bodyIndex++] = tile.BlockColor.Id;
                    }
                }

                if (tile.WallType != WallType.None) {
                    header |= 4;
                    buffer[bodyIndex++] = tile.WallType.Id;

                    if (tile.WallColor != PaintColor.None) {
                        header3 |= 16;
                        buffer[bodyIndex++] = tile.WallColor.Id;
                    }
                }

                if (tile.LiquidAmount > 0) {
                    header |= (byte)((tile.LiquidType.Id + 1) << 3);
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

            for (int y = 0; y < SectionHeight; ++y) {
                for (int x = 0; x < SectionWidth; ++x) {
                    var tile = SectionTiles[x, y];
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

        private void WriteTileEntitiesToWriterImpl(BinaryWriter writer) {
            var chests = new List<NetworkTileEntity>();
            var signs = new List<NetworkTileEntity>();
            var notChestsOrSigns = new List<NetworkTileEntity>();
            foreach (var tileEntity in SectionTileEntities) {
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

        /// <summary>
        /// Represents the tile entities in a <see cref="SectionPacket"/>.
        /// </summary>
        public class TileEntities : IList<NetworkTileEntity>, IDirtiable {
            private readonly IList<NetworkTileEntity> _tileEntities;
            private bool _isDirty;

            /// <inheritdoc />
            public NetworkTileEntity this[int index] {
                get => _tileEntities[index];
                set {
                    _tileEntities[index] = value ?? throw new ArgumentNullException(nameof(value));
                    _isDirty = true;
                }
            }

            /// <inheritdoc />
            public int Count => _tileEntities.Count;

            /// <inheritdoc />
            public bool IsReadOnly => _tileEntities.IsReadOnly;

            /// <inheritdoc />
            public bool IsDirty => _isDirty || _tileEntities.Any(t => t.IsDirty);

            internal TileEntities(IList<NetworkTileEntity> tileEntities) {
                _tileEntities = tileEntities;
            }

            /// <inheritdoc />
            public IEnumerator<NetworkTileEntity> GetEnumerator() => _tileEntities.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <inheritdoc />
            public void Add(NetworkTileEntity item) {
                _tileEntities.Add(item);
                _isDirty = true;
            }

            /// <inheritdoc />
            public void Clear() {
                _tileEntities.Clear();
                _isDirty = true;
            }

            /// <inheritdoc />
            public bool Contains(NetworkTileEntity item) => _tileEntities.Contains(item);

            /// <inheritdoc />
            public void CopyTo(NetworkTileEntity[] array, int arrayIndex) {
                _tileEntities.CopyTo(array, arrayIndex);
            }

            /// <inheritdoc />
            public bool Remove(NetworkTileEntity item) {
                _isDirty = true;
                return _tileEntities.Remove(item);
            }

            /// <inheritdoc />
            public int IndexOf(NetworkTileEntity item) => _tileEntities.IndexOf(item);

            /// <inheritdoc />
            public void Insert(int index, NetworkTileEntity item) {
                _tileEntities.Insert(index, item);
                _isDirty = true;
            }

            /// <inheritdoc />
            public void RemoveAt(int index) {
                _tileEntities.RemoveAt(index);
                _isDirty = true;
            }

            /// <inheritdoc />
            public void Clean() {
                _isDirty = false;
                foreach (var tileEntity in _tileEntities) {
                    tileEntity.Clean();
                }
            }
        }
    }
}
