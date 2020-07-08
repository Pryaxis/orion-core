// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Packets.DataStructures.TileEntities;
using Orion.Core.Utils;
using Orion.Core.World;
using Orion.Core.World.Tiles;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to the client to set a section's information.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 40)]
    public sealed class SectionInfo : IPacket
    {
        // The shifts for the tile headers.
        private const int LiquidTypeShift = 3;
        private const int CopyLengthBytesShift = 6;
        private const int BlockShapeShift = 4;

        // The masks for the tile headers.
        private const byte HasHeader2Mask /*      */ = 0b_00000001;
        private const byte HasBlockMask /*        */ = 0b_00000010;
        private const byte HasWallMask /*         */ = 0b_00000100;
        private const byte LiquidTypeMask /*      */ = 0b_00011000;
        private const byte BlockIdTwoBytesMask /* */ = 0b_00100000;
        private const byte CopyLengthBytesMask /* */ = 0b_11000000;

        private const byte HasHeader3Mask /*      */ = 0b_00000001;
        private const byte HasRedWireMask /*      */ = 0b_00000010;
        private const byte HasBlueWireMask /*     */ = 0b_00000100;
        private const byte HasGreenWireMask /*    */ = 0b_00001000;
        private const byte BlockShapeMask /*      */ = 0b_01110000;

        private const byte HasActuatorMask /*     */ = 0b_00000010;
        private const byte IsBlockActuatedMask /* */ = 0b_00000100;
        private const byte HasBlockColorMask /*   */ = 0b_00001000;
        private const byte HasWallColorMask /*    */ = 0b_00010000;
        private const byte HasYellowWireMask /*   */ = 0b_00100000;
        private const byte WallIdTwoBytesMask /*  */ = 0b_01000000;

        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(8)] private ITileSlice _tiles = NetworkTileSlice.Empty;
        [FieldOffset(16)] private IList<SerializableChest> _chests = new List<SerializableChest>();
        [FieldOffset(24)] private IList<SerializableSign> _signs = new List<SerializableSign>();
        [FieldOffset(32)] private IList<SerializableTileEntity> _tileEntities = new List<SerializableTileEntity>();

        /// <summary>
        /// Gets or sets the section's starting X coordinate.
        /// </summary>
        /// <value>The section's starting X coordinate.</value>
        [field: FieldOffset(0)] public int StartX { get; set; }

        /// <summary>
        /// Gets or sets the section's starting Y coordinate.
        /// </summary>
        /// <value>The section's starting Y coordinate.</value>
        [field: FieldOffset(4)] public int StartY { get; set; }

        /// <summary>
        /// Gets or sets the section's tiles.
        /// </summary>
        /// <value>The section's tiles.</value>
        public ITileSlice Tiles
        {
            get => _tiles;
            set => _tiles = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets the section's chests.
        /// </summary>
        /// <value>The section's chests.</value>
        public IList<SerializableChest> Chests => _chests;

        /// <summary>
        /// Gets the section's signs.
        /// </summary>
        /// <value>The section's signs.</value>
        public IList<SerializableSign> Signs => _signs;

        /// <summary>
        /// Gets the section's tile entities.
        /// </summary>
        /// <value>The section's tile entities.</value>
        public IList<SerializableTileEntity> TileEntities => _tileEntities;

        PacketId IPacket.Id => PacketId.SectionInfo;

        unsafe int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            if (span.At(0) != 0)
            {
                var pool = ArrayPool<byte>.Shared;
                var buffer = pool.Rent(524288);  // 524288 should be long enough for a decompression buffer.

                try
                {
                    var decompressedLength = 0;

                    fixed (byte* spanBytes = span[1..])
                    {
                        using var inputStream = new UnmanagedMemoryStream(spanBytes, span.Length - 1);
                        var deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress, true);
                        decompressedLength = deflateStream.Read(buffer, 0, buffer.Length);
                        deflateStream.Close();
                    }

                    _ = Read(buffer.AsSpan(0..decompressedLength));
                }
                finally
                {
                    pool.Return(buffer);
                }
            }
            else
            {
                _ = Read(span[1..]);
            }

            return span.Length;
        }

        unsafe int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            span.At(0) = 1;

            var pool = ArrayPool<byte>.Shared;
            var buffer = pool.Rent(524288);  // 524288 should be long enough for a compression buffer.

            try
            {
                var decompressedLength = Write(buffer);

                fixed (byte* spanBytes = span[1..])
                {
                    using var outputStream =
                        new UnmanagedMemoryStream(spanBytes, span.Length - 1, span.Length - 1, FileAccess.Write);
                    var deflateStream = new DeflateStream(outputStream, CompressionLevel.Fastest, true);
                    deflateStream.Write(buffer, 0, decompressedLength);
                    deflateStream.Close();

                    return (int)outputStream.Position;
                }
            }
            finally
            {
                pool.Return(buffer);
            }
        }

        private unsafe int Read(Span<byte> span)
        {
            var length = span.Read(ref _bytes, 8);
            length += ReadTiles(span[length..]);
            length += ReadChests(span[length..]);
            length += ReadSigns(span[length..]);
            length += ReadTileEntities(span[length..]);
            return length;

            int ReadTiles(Span<byte> span)
            {
                var width = Unsafe.ReadUnaligned<short>(ref span.At(0));
                var height = Unsafe.ReadUnaligned<short>(ref span.At(2));
                var length = 4;

                var copyLength = 0;
                Tile previousTile = default;

                _tiles = new NetworkTileSlice(width, height);
                for (var j = 0; j < height; ++j)
                {
                    for (var i = 0; i < width; ++i)
                    {
                        if (copyLength > 0)
                        {
                            --copyLength;
                            _tiles[i, j] = previousTile;
                            continue;
                        }

                        length += ReadTile(span[length..], out _tiles[i, j], out copyLength);
                        previousTile = _tiles[i, j];
                    }
                }

                return length;
            }

            int ReadChests(Span<byte> span)
            {
                var numChests = Unsafe.ReadUnaligned<short>(ref span.At(0));
                _chests = new List<SerializableChest>(numChests);

                var length = 2;
                for (var i = 0; i < numChests; ++i)
                {
                    length += SerializableChest.Read(span[length..], out var chest);
                    _chests.Add(chest);
                }

                return length;
            }

            int ReadSigns(Span<byte> span)
            {
                var numSigns = Unsafe.ReadUnaligned<short>(ref span.At(0));
                _signs = new List<SerializableSign>(numSigns);

                var length = 2;
                for (var i = 0; i < numSigns; ++i)
                {
                    length += SerializableSign.Read(span[length..], out var sign);
                    _signs.Add(sign);
                }

                return length;
            }

            int ReadTileEntities(Span<byte> span)
            {
                var numTileEntities = Unsafe.ReadUnaligned<short>(ref span.At(0));
                _tileEntities = new List<SerializableTileEntity>(numTileEntities);

                var length = 2;
                for (var i = 0; i < numTileEntities; ++i)
                {
                    length += SerializableTileEntity.Read(span[length..], true, out var tileEntity);
                    _tileEntities.Add(tileEntity);
                }

                return length;
            }
        }

        private unsafe int Write(Span<byte> span)
        {
            var length = span.Write(ref _bytes, 8);
            length += WriteTiles(span[length..]);
            length += WriteChests(span[length..]);
            length += WriteSigns(span[length..]);
            length += WriteTileEntities(span[length..]);
            return length;

            int WriteTiles(Span<byte> span)
            {
                Unsafe.WriteUnaligned(ref span.At(0), _tiles.Width);
                Unsafe.WriteUnaligned(ref span.At(2), _tiles.Height);
                var length = 4;

                var runLength = 0;
                Tile previousTile = default;

                for (var j = 0; j < _tiles.Height; ++j)
                {
                    for (var i = 0; i < _tiles.Width; ++i)
                    {
                        if (runLength > 0)
                        {
                            if (_tiles[i, j].Equals(previousTile))
                            {
                                ++runLength;
                                continue;
                            }

                            length += WriteTile(span[length..], ref previousTile, runLength - 1);
                        }

                        previousTile = _tiles[i, j];
                        runLength = 1;
                    }
                }

                if (runLength > 0)
                {
                    length += WriteTile(span[length..], ref previousTile, runLength - 1);
                }

                return length;
            }

            int WriteChests(Span<byte> span)
            {
                var numChests = (short)_chests.Count;
                Unsafe.WriteUnaligned(ref span.At(0), numChests);

                var length = 2;
                for (var i = 0; i < numChests; ++i)
                {
                    length += _chests[i].Write(span[length..]);
                }

                return length;
            }

            int WriteSigns(Span<byte> span)
            {
                var numSigns = (short)_signs.Count;
                Unsafe.WriteUnaligned(ref span.At(0), numSigns);

                var length = 2;
                for (var i = 0; i < numSigns; ++i)
                {
                    length += _signs[i].Write(span[length..]);
                }

                return length;
            }

            int WriteTileEntities(Span<byte> span)
            {
                var numTileEntities = (short)_tileEntities.Count;
                Unsafe.WriteUnaligned(ref span.At(0), numTileEntities);

                var length = 2;
                for (var i = 0; i < numTileEntities; ++i)
                {
                    length += _tileEntities[i].Write(span[length..], true);
                }

                return length;
            }
        }

        private int ReadTile(Span<byte> span, out Tile tile, out int copyLength)
        {
            tile = default;

            var length = 1;
            var header = span.At(0);
            var header2 = (header & HasHeader2Mask) != 0 ? span.At(length++) : (byte)0;
            var header3 = (header2 & HasHeader3Mask) != 0 ? span.At(length++) : (byte)0;

            tile.HasRedWire /*      */ = (header2 & HasRedWireMask) != 0;
            tile.HasBlueWire /*     */ = (header2 & HasBlueWireMask) != 0;
            tile.HasGreenWire /*    */ = (header2 & HasGreenWireMask) != 0;
            tile.BlockShape /*      */ = (BlockShape)((header2 & BlockShapeMask) >> BlockShapeShift);

            tile.HasActuator /*     */ = (header3 & HasActuatorMask) != 0;
            tile.IsBlockActuated /* */ = (header3 & IsBlockActuatedMask) != 0;
            tile.HasYellowWire /*   */ = (header3 & HasYellowWireMask) != 0;

            if ((header & HasBlockMask) != 0)
            {
                if ((header & BlockIdTwoBytesMask) != 0)
                {
                    tile.BlockId = Unsafe.ReadUnaligned<BlockId>(ref span.At(length)) + 1;
                    length += 2;
                }
                else
                {
                    tile.BlockId = (BlockId)span.At(length++) + 1;
                }

                if (tile.BlockId.HasFrames())
                {
                    Unsafe.CopyBlockUnaligned(ref Unsafe.Add(ref tile.AsByte(), 4), ref span.At(length), 4);
                    length += 4;
                }

                if ((header3 & HasBlockColorMask) != 0)
                {
                    tile.BlockColor = (PaintColor)span.At(length++);
                }
            }

            if ((header & HasWallMask) != 0)
            {
                tile.WallId = (WallId)span.At(length++);

                if ((header3 & HasWallColorMask) != 0)
                {
                    tile.WallColor = (PaintColor)span.At(length++);
                }
            }

            var liquidType = header & LiquidTypeMask;
            if (liquidType != 0)
            {
                var type = (LiquidType)((liquidType >> LiquidTypeShift) - 1);
                var amount = span.At(length++);
                tile.Liquid = new Liquid(type, amount);
            }

            if ((header3 & WallIdTwoBytesMask) != 0)
            {
                tile.WallId = (WallId)((span.At(length++) << 8) | (ushort)tile.WallId);
            }

            switch ((header & CopyLengthBytesMask) >> CopyLengthBytesShift)
            {
            case 0:
                copyLength = 0;
                break;
            case 1:
                copyLength = span.At(length++);
                break;
            default:
                copyLength = Unsafe.ReadUnaligned<short>(ref span.At(length));
                length += 2;
                break;
            }

            return length;
        }

        private int WriteTile(Span<byte> span, ref Tile tile, int copyLength)
        {
            Span<byte> buffer = stackalloc byte[32];
            var startIndex = 2;
            var endIndex = 3;

            byte header = 0;
            byte header2 = 0;
            byte header3 = 0;

            if (tile.HasRedWire) /*      */ header2 |= HasRedWireMask;
            if (tile.HasBlueWire) /*     */ header2 |= HasBlueWireMask;
            if (tile.HasGreenWire) /*    */ header2 |= HasGreenWireMask;
            header2 |= (byte)((byte)tile.BlockShape << BlockShapeShift);

            if (tile.HasActuator) /*     */ header3 |= HasActuatorMask;
            if (tile.IsBlockActuated) /* */ header3 |= IsBlockActuatedMask;
            if (tile.HasYellowWire) /*   */ header3 |= HasYellowWireMask;

            if (tile.BlockId != BlockId.None)
            {
                // Offset the block ID by 1 since we need to transmit Terraria bytes.
                if ((ushort)(tile.BlockId - 1) > byte.MaxValue)
                {
                    Unsafe.WriteUnaligned(ref buffer.At(endIndex), tile.BlockId - 1);
                    endIndex += 2;

                    header |= BlockIdTwoBytesMask;
                }
                else
                {
                    buffer.At(endIndex++) = (byte)(tile.BlockId - 1);
                }

                if (tile.BlockId.HasFrames())
                {
                    Unsafe.CopyBlockUnaligned(ref buffer.At(endIndex), ref Unsafe.Add(ref tile.AsByte(), 4), 4);
                    endIndex += 4;
                }

                if (tile.BlockColor != PaintColor.None)
                {
                    buffer.At(endIndex++) = (byte)tile.BlockColor;

                    header3 |= HasBlockColorMask;
                }

                header |= HasBlockMask;
            }

            if (tile.WallId != WallId.None)
            {
                buffer.At(endIndex++) = (byte)tile.WallId;

                if (tile.WallColor != PaintColor.None)
                {
                    buffer.At(endIndex++) = (byte)tile.WallColor;

                    header3 |= HasWallColorMask;
                }

                header |= HasWallMask;
            }

            var liquid = tile.Liquid;
            if (liquid.Amount != 0)
            {
                header |= (byte)((byte)(liquid.Type + 1) << LiquidTypeShift);
                buffer.At(endIndex++) = liquid.Amount;
            }

            if ((ushort)tile.WallId > byte.MaxValue)
            {
                buffer.At(endIndex++) = (byte)((ushort)tile.WallId >> 8);

                header3 |= WallIdTwoBytesMask;
            }

            if (header3 != 0)
            {
                buffer.At(startIndex--) = header3;

                header2 |= HasHeader3Mask;
            }

            if (header2 != 0)
            {
                buffer.At(startIndex--) = header2;

                header |= HasHeader2Mask;
            }

            if (copyLength > 0)
            {
                if (copyLength > byte.MaxValue)
                {
                    Unsafe.WriteUnaligned(ref buffer.At(endIndex), (short)copyLength);
                    endIndex += 2;
                    header |= 2 << CopyLengthBytesShift;
                }
                else
                {
                    buffer.At(endIndex++) = (byte)copyLength;
                    header |= 1 << CopyLengthBytesShift;
                }
            }

            buffer.At(startIndex) = header;
            return span.Write(ref buffer.At(startIndex), endIndex - startIndex);
        }
    }
}
