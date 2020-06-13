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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.World.Tiles;

namespace Orion.Core.Packets.World.Tiles {
    /// <summary>
    /// A packet sent to set a square of tiles.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct TileSquarePacket : IPacket {
        // The shifts for the tile header.
        private const int SlopeShift = 12;

        // The masks for the tile header.
        private const ushort IsBlockActiveMask   /* */ = 0b00000000_00000001;
        private const ushort HasWallMask         /* */ = 0b00000000_00000100;
        private const ushort HasLiquidMask       /* */ = 0b00000000_00001000;
        private const ushort HasRedWireMask      /* */ = 0b00000000_00010000;
        private const ushort IsBlockHalvedMask   /* */ = 0b00000000_00100000;
        private const ushort HasActuatorMask     /* */ = 0b00000000_01000000;
        private const ushort IsBlockActuatedMask /* */ = 0b00000000_10000000;
        private const ushort HasBlueWireMask     /* */ = 0b00000001_00000000;
        private const ushort HasGreenWireMask    /* */ = 0b00000010_00000000;
        private const ushort HasBlockColorMask   /* */ = 0b00000100_00000000;
        private const ushort HasWallColorMask    /* */ = 0b00001000_00000000;
        private const ushort SlopeMask           /* */ = 0b01110000_00000000;
        private const ushort HasYellowWireMask   /* */ = 0b10000000_00000000;

        // Store a single empty `TileSlice` so that there is only one allocation.
        private static readonly TileSlice _emptyTiles = new TileSlice(0, 0);

        [FieldOffset(0)] private byte _data;
        [FieldOffset(8)] private ITileSlice? _tiles;

        /// <summary>
        /// Gets or sets the top-left tile's X coordinate.
        /// </summary>
        /// <value>The top-left tile's X coordinate.</value>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the top-left tile's Y coordinate.
        /// </summary>
        /// <value>The top-left tile's Y coordinate.</value>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the square of tiles.
        /// </summary>
        /// <value>The square of tiles.</value>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a square.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public ITileSlice Tiles {
            get => _tiles ?? _emptyTiles;

            set {
                if (value is null) {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!value.IsSquare()) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException("Value is not a square", nameof(value));
                }

                _tiles = value;
            }
        }

        PacketId IPacket.Id => PacketId.TileSquare;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) {
            var index = 2;
            var size = Unsafe.ReadUnaligned<short>(ref span[0]);
            if (size < 0) {
                _data = span[index++];
                size &= short.MaxValue;
            }

            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(1), ref span[index], 4);
            index += 4;

            _tiles = new TileSlice(size, size);
            for (var i = 0; i < size; ++i) {
                for (var j = 0; j < size; ++j) {
                    index += ReadTile(span[index..], ref _tiles[i, j]);
                }
            }
            return index;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) {
            var tiles = Tiles;

            var index = 2;
            var size = (short)tiles.Width;
            if (_data > 0) {
                span[index++] = _data;
                size |= ~short.MaxValue;
            }

            Unsafe.WriteUnaligned(ref span[0], size);
            Unsafe.CopyBlockUnaligned(ref span[index], ref this.AsRefByte(1), 4);
            index += 4;

            for (var i = 0; i < tiles.Width; ++i) {
                for (var j = 0; j < tiles.Height; ++j) {
                    index += WriteTile(span[index..], ref tiles[i, j]);
                }
            }
            return index;
        }

        private int ReadTile(Span<byte> span, ref Tile tile) {
            var index = 2;
            var header = Unsafe.ReadUnaligned<ushort>(ref span[0]);

            tile.IsBlockActive        /* */ = (header & IsBlockActiveMask) != 0;
            var hasWall               /* */ = (header & HasWallMask) != 0;
            var hasLiquid             /* */ = (header & HasLiquidMask) != 0;
            tile.HasRedWire           /* */ = (header & HasRedWireMask) != 0;
            tile.IsBlockHalved        /* */ = (header & IsBlockHalvedMask) != 0;
            tile.HasActuator          /* */ = (header & HasActuatorMask) != 0;
            tile.IsBlockActuated      /* */ = (header & IsBlockActuatedMask) != 0;
            tile.HasBlueWire          /* */ = (header & HasBlueWireMask) != 0;
            tile.HasGreenWire         /* */ = (header & HasGreenWireMask) != 0;
            var hasBlockColor         /* */ = (header & HasBlockColorMask) != 0;
            var hasWallColor          /* */ = (header & HasWallColorMask) != 0;
            tile.HasYellowWire        /* */ = (header & HasYellowWireMask) != 0;

            if (hasBlockColor) {
                tile.BlockColor = (PaintColor)span[index++];
            }

            if (hasWallColor) {
                tile.WallColor = (PaintColor)span[index++];
            }

            if (tile.IsBlockActive) {
                tile.BlockId = Unsafe.ReadUnaligned<BlockId>(ref span[index]);
                index += 2;

                if (tile.BlockId.HasFrames()) {
                    Unsafe.CopyBlockUnaligned(ref tile.AsRefByte(5), ref span[index], 4);
                    index += 4;
                }

                tile.Slope = (Slope)((header & SlopeMask) >> SlopeShift);
            }

            if (hasWall) {
                tile.WallId = Unsafe.ReadUnaligned<WallId>(ref span[index]);
                index += 2;
            }

            if (hasLiquid) {
                tile.LiquidAmount = span[index++];
                tile.Liquid = (Liquid)span[index++];
            }

            return index;
        }

        private int WriteTile(Span<byte> span, ref Tile tile) {
            var hasWall = tile.WallId != WallId.None;
            var hasLiquid = tile.LiquidAmount != 0;
            var hasBlockColor = tile.BlockColor != PaintColor.None;
            var hasWallColor = tile.WallColor != PaintColor.None;

            var index = 2;
            ushort header = 0;

            if (tile.IsBlockActive)   /* */ header |= IsBlockActiveMask;
            if (hasWall)              /* */ header |= HasWallMask;
            if (hasLiquid)            /* */ header |= HasLiquidMask;
            if (tile.HasRedWire)      /* */ header |= HasRedWireMask;
            if (tile.IsBlockHalved)   /* */ header |= IsBlockHalvedMask;
            if (tile.HasActuator)     /* */ header |= HasActuatorMask;
            if (tile.IsBlockActuated) /* */ header |= IsBlockActuatedMask;
            if (tile.HasBlueWire)     /* */ header |= HasBlueWireMask;
            if (tile.HasGreenWire)    /* */ header |= HasGreenWireMask;
            if (hasBlockColor)        /* */ header |= HasBlockColorMask;
            if (hasWallColor)         /* */ header |= HasWallColorMask;
            if (tile.HasYellowWire)   /* */ header |= HasYellowWireMask;

            if (hasBlockColor) {
                span[index++] = (byte)tile.BlockColor;
            }

            if (hasWallColor) {
                span[index++] = (byte)tile.WallColor;
            }

            if (tile.IsBlockActive) {
                Unsafe.WriteUnaligned(ref span[index], tile.BlockId);
                index += 2;

                if (tile.BlockId.HasFrames()) {
                    Unsafe.CopyBlockUnaligned(ref span[index], ref tile.AsRefByte(5), 4);
                    index += 4;
                }

                header |= (ushort)((int)(tile.Slope) << SlopeShift);
            }

            if (hasWall) {
                Unsafe.WriteUnaligned(ref span[index], tile.WallId);
                index += 2;
            }

            if (hasLiquid) {
                span[index++] = tile.LiquidAmount;
                span[index++] = (byte)tile.Liquid;
            }

            Unsafe.WriteUnaligned(ref span[0], header);
            return index;
        }
    }
}
