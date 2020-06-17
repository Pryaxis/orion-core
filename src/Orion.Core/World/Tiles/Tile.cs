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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Orion.Core.World.Tiles
{
    /// <summary>
    /// Represents an optimized Terraria tile.
    /// </summary>
    /// <remarks>
    /// This structure is not thread-safe.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct Tile
    {
        // The shifts for the tile header.
        private const int BlockColorShift = 0;
        private const int SlopeShift = 12;
        private const int WallColorShift = 16;
        private const int LiquidShift = 21;
        private const int BlockFrameNumberShift = 24;

        // The masks for the tile header. These follow roughly the same layout in Terraria's `Tile` class.
        internal const uint BlockColorMask /*       */ = 0b_00000000_00000000_00000000_00011111;
        internal const uint IsBlockActiveMask /*    */ = 0b_00000000_00000000_00000000_00100000;
        internal const uint IsBlockActuatedMask /*  */ = 0b_00000000_00000000_00000000_01000000;
        internal const uint HasRedWireMask /*       */ = 0b_00000000_00000000_00000000_10000000;
        internal const uint HasBlueWireMask /*      */ = 0b_00000000_00000000_00000001_00000000;
        internal const uint HasGreenWireMask /*     */ = 0b_00000000_00000000_00000010_00000000;
        internal const uint IsBlockHalvedMask /*    */ = 0b_00000000_00000000_00000100_00000000;
        internal const uint HasActuatorMask /*      */ = 0b_00000000_00000000_00001000_00000000;
        internal const uint SlopeMask /*            */ = 0b_00000000_00000000_01110000_00000000;
        internal const uint WallColorMask /*        */ = 0b_00000000_00011111_00000000_00000000;
        internal const uint LiquidMask /*           */ = 0b_00000000_01100000_00000000_00000000;
        internal const uint HasYellowWireMask /*    */ = 0b_00000000_10000000_00000000_00000000;
        internal const uint BlockFrameNumberMask /* */ = 0b_00001111_00000000_00000000_00000000;
        internal const uint IsCheckingLiquidMask /* */ = 0b_00010000_00000000_00000000_00000000;
        internal const uint ShouldSkipLiquidMask /* */ = 0b_00100000_00000000_00000000_00000000;

        // Provide `internal` field access with type punning so that we can implement an `OTAPI.Tile.ITile` adapter
        // efficiently.
        [FieldOffset(5)] internal int _blockFrames;
        [FieldOffset(9)] internal uint _header;
        [FieldOffset(9)] internal short _sTileHeader;
        [FieldOffset(11)] internal byte _bTileHeader;
        [FieldOffset(12)] internal byte _bTileHeader3;

        /// <summary>
        /// Gets or sets the block ID.
        /// </summary>
        /// <value>The block ID.</value>
        [field: FieldOffset(0)] public BlockId BlockId { get; set; }

        /// <summary>
        /// Gets or sets the wall ID.
        /// </summary>
        /// <value>The wall ID.</value>
        [field: FieldOffset(2)] public WallId WallId { get; set; }

        /// <summary>
        /// Gets or sets the liquid amount.
        /// </summary>
        /// <value>The liquid amount.</value>
        [field: FieldOffset(4)] public byte LiquidAmount { get; set; }

        /// <summary>
        /// Gets or sets the block's X frame.
        /// </summary>
        /// <value>The block's X frame.</value>
        [field: FieldOffset(5)] public short BlockFrameX { get; set; }

        /// <summary>
        /// Gets or sets the block's Y frame.
        /// </summary>
        /// <value>The block's Y frame.</value>
        [field: FieldOffset(7)] public short BlockFrameY { get; set; }

        /// <summary>
        /// Gets or sets the block color.
        /// </summary>
        /// <value>The block color.</value>
        public PaintColor BlockColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((_header & BlockColorMask) >> BlockColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _header = (_header & ~BlockColorMask) | (((uint)value << BlockColorShift) & BlockColorMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is active.
        /// </summary>
        /// <value><see langword="true"/> if the block is active; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & IsBlockActiveMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= IsBlockActiveMask;
                }
                else
                {
                    _header &= ~IsBlockActiveMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is actuated.
        /// </summary>
        /// <value><see langword="true"/> if the block is actuated; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActuated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & IsBlockActuatedMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= IsBlockActuatedMask;
                }
                else
                {
                    _header &= ~IsBlockActuatedMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has red wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has red wire; otherwise, <see langword="false"/>.</value>
        public bool HasRedWire
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & HasRedWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= HasRedWireMask;
                }
                else
                {
                    _header &= ~HasRedWireMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has blue wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has blue wire; otherwise, <see langword="false"/>.</value>
        public bool HasBlueWire
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & HasBlueWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= HasBlueWireMask;
                }
                else
                {
                    _header &= ~HasBlueWireMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public bool HasGreenWire
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & HasGreenWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= HasGreenWireMask;
                }
                else
                {
                    _header &= ~HasGreenWireMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is halved.
        /// </summary>
        /// <value><see langword="true"/> if the block is halved; otherwise, <see langword="false"/>.</value>
        public bool IsBlockHalved
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & IsBlockHalvedMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= IsBlockHalvedMask;
                }
                else
                {
                    _header &= ~IsBlockHalvedMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has an actuator.
        /// </summary>
        /// <value><see langword="true"/> if the tile has an actuator; otherwise, <see langword="false"/>.</value>
        public bool HasActuator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & HasActuatorMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= HasActuatorMask;
                }
                else
                {
                    _header &= ~HasActuatorMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets the slope.
        /// </summary>
        /// <value>The slope.</value>
        public Slope Slope
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Slope)((_header & SlopeMask) >> SlopeShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _header = (_header & ~SlopeMask) | (((uint)value << SlopeShift) & SlopeMask);
        }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        /// <value>The wall color.</value>
        public PaintColor WallColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((_header & WallColorMask) >> WallColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _header = (_header & ~WallColorMask) | (((uint)value << WallColorShift) & WallColorMask);
        }

        /// <summary>
        /// Gets or sets the liquid.
        /// </summary>
        /// <value>The liquid.</value>
        public Liquid Liquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Liquid)((_header & LiquidMask) >> LiquidShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _header = (_header & ~LiquidMask) | (((uint)value << LiquidShift) & LiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public bool HasYellowWire
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & HasYellowWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= HasYellowWireMask;
                }
                else
                {
                    _header &= ~HasYellowWireMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets the block's frame number.
        /// </summary>
        /// <value>The block's frame number.</value>
        public byte BlockFrameNumber
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (byte)((_header & BlockFrameNumberMask) >> BlockFrameNumberShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _header =
                    (_header & ~BlockFrameNumberMask) | (((uint)value << BlockFrameNumberShift) & BlockFrameNumberMask);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile is checking liquid.
        /// </summary>
        /// <value><see langword="true"/> if the tile is checking liquid; otherwise, <see langword="false"/>.</value>
        public bool IsCheckingLiquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & IsCheckingLiquidMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= IsCheckingLiquidMask;
                }
                else
                {
                    _header &= ~IsCheckingLiquidMask;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile should skip liquids.
        /// </summary>
        /// <value><see langword="true"/> if the tile should skip liquids; otherwise, <see langword="false"/>.</value>
        public bool ShouldSkipLiquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (_header & ShouldSkipLiquidMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _header |= ShouldSkipLiquidMask;
                }
                else
                {
                    _header &= ~ShouldSkipLiquidMask;
                }
            }
        }
    }
}
