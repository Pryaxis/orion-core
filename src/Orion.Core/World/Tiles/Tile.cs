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

        /// <summary>
        /// The <see cref="BlockColor"/> mask for the tile header..
        /// </summary>
        public const uint BlockColorMask /*       */ = 0b_00000000_00000000_00000000_00011111;

        /// <summary>
        /// The <see cref="IsBlockActive"/> mask for the tile header..
        /// </summary>
        public const uint IsBlockActiveMask /*    */ = 0b_00000000_00000000_00000000_00100000;

        /// <summary>
        /// The <see cref="IsBlockActuated"/> mask for the tile header..
        /// </summary>
        public const uint IsBlockActuatedMask /*  */ = 0b_00000000_00000000_00000000_01000000;

        /// <summary>
        /// The <see cref="HasRedWire"/> mask for the tile header..
        /// </summary>
        public const uint HasRedWireMask /*       */ = 0b_00000000_00000000_00000000_10000000;

        /// <summary>
        /// The <see cref="HasBlueWire"/> mask for the tile header..
        /// </summary>
        public const uint HasBlueWireMask /*      */ = 0b_00000000_00000000_00000001_00000000;

        /// <summary>
        /// The <see cref="HasGreenWire"/> mask for the tile header..
        /// </summary>
        public const uint HasGreenWireMask /*     */ = 0b_00000000_00000000_00000010_00000000;

        /// <summary>
        /// The <see cref="IsBlockHalved"/> mask for the tile header..
        /// </summary>
        public const uint IsBlockHalvedMask /*    */ = 0b_00000000_00000000_00000100_00000000;

        /// <summary>
        /// The <see cref="HasActuator"/> mask for the tile header..
        /// </summary>
        public const uint HasActuatorMask /*      */ = 0b_00000000_00000000_00001000_00000000;

        /// <summary>
        /// The <see cref="Slope"/> mask for the tile header..
        /// </summary>
        public const uint SlopeMask /*            */ = 0b_00000000_00000000_01110000_00000000;

        /// <summary>
        /// The <see cref="WallColor"/> mask for the tile header..
        /// </summary>
        public const uint WallColorMask /*        */ = 0b_00000000_00011111_00000000_00000000;

        /// <summary>
        /// The <see cref="Liquid"/> mask for the tile header..
        /// </summary>
        public const uint LiquidMask /*           */ = 0b_00000000_01100000_00000000_00000000;

        /// <summary>
        /// The <see cref="HasYellowWire"/> mask for the tile header..
        /// </summary>
        public const uint HasYellowWireMask /*    */ = 0b_00000000_10000000_00000000_00000000;

        /// <summary>
        /// The <see cref="BlockFrameNumber"/> mask for the tile header..
        /// </summary>
        public const uint BlockFrameNumberMask /* */ = 0b_00001111_00000000_00000000_00000000;

        /// <summary>
        /// The <see cref="IsCheckingLiquidMask"/> mask for the tile header..
        /// </summary>
        public const uint IsCheckingLiquidMask /* */ = 0b_00010000_00000000_00000000_00000000;

        /// <summary>
        /// The <see cref="ShouldSkipLiquidMask"/> mask for the tile header..
        /// </summary>
        public const uint ShouldSkipLiquidMask /* */ = 0b_00100000_00000000_00000000_00000000;

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
        /// Gets or sets the tile's header.
        /// </summary>
        /// <value>The tile's header.</value>
        [field: FieldOffset(9)] public uint Header { get; set; }

        /// <summary>
        /// Gets or sets the block's frames. <i>This is a combination of <see cref="BlockFrameX"/> and
        /// <see cref="BlockFrameY"/>!</i>
        /// </summary>
        /// <value>The block's frames.</value>
        [field: FieldOffset(5)] public int BlockFrames { get; set; }

        /// <summary>
        /// Gets or sets the tile's first header part. <i>This is a portion of <see cref="Header"/>!</i>
        /// </summary>
        /// <value>The tile's first header part.</value>
        [field: FieldOffset(9)] public short HeaderPart { get; set; }

        /// <summary>
        /// Gets or sets the tile's second header part. <i>This is a portion of <see cref="Header"/>!</i>
        /// </summary>
        /// <value>The tile's second header part.</value>
        [field: FieldOffset(11)] public byte HeaderPart2 { get; set; }

        /// <summary>
        /// Gets or sets the tile's third header part. <i>This is a portion of <see cref="Header"/>!</i>
        /// </summary>
        /// <value>The tile's third header part.</value>
        [field: FieldOffset(12)] public byte HeaderPart3 { get; set; }

        /// <summary>
        /// Gets or sets the block color.
        /// </summary>
        /// <value>The block color.</value>
        public PaintColor BlockColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header & BlockColorMask) >> BlockColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~BlockColorMask) | (((uint)value << BlockColorShift) & BlockColorMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is active.
        /// </summary>
        /// <value><see langword="true"/> if the block is active; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Header & IsBlockActiveMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= IsBlockActiveMask;
                }
                else
                {
                    Header &= ~IsBlockActiveMask;
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
            readonly get => (Header & IsBlockActuatedMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= IsBlockActuatedMask;
                }
                else
                {
                    Header &= ~IsBlockActuatedMask;
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
            readonly get => (Header & HasRedWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= HasRedWireMask;
                }
                else
                {
                    Header &= ~HasRedWireMask;
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
            readonly get => (Header & HasBlueWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= HasBlueWireMask;
                }
                else
                {
                    Header &= ~HasBlueWireMask;
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
            readonly get => (Header & HasGreenWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= HasGreenWireMask;
                }
                else
                {
                    Header &= ~HasGreenWireMask;
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
            readonly get => (Header & IsBlockHalvedMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= IsBlockHalvedMask;
                }
                else
                {
                    Header &= ~IsBlockHalvedMask;
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
            readonly get => (Header & HasActuatorMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= HasActuatorMask;
                }
                else
                {
                    Header &= ~HasActuatorMask;
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
            readonly get => (Slope)((Header & SlopeMask) >> SlopeShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~SlopeMask) | (((uint)value << SlopeShift) & SlopeMask);
        }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        /// <value>The wall color.</value>
        public PaintColor WallColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header & WallColorMask) >> WallColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~WallColorMask) | (((uint)value << WallColorShift) & WallColorMask);
        }

        /// <summary>
        /// Gets or sets the liquid.
        /// </summary>
        /// <value>The liquid.</value>
        public Liquid Liquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Liquid)((Header & LiquidMask) >> LiquidShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~LiquidMask) | (((uint)value << LiquidShift) & LiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public bool HasYellowWire
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Header & HasYellowWireMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= HasYellowWireMask;
                }
                else
                {
                    Header &= ~HasYellowWireMask;
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
            readonly get => (byte)((Header & BlockFrameNumberMask) >> BlockFrameNumberShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Header =
                    (Header & ~BlockFrameNumberMask) | (((uint)value << BlockFrameNumberShift) & BlockFrameNumberMask);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile is checking liquid.
        /// </summary>
        /// <value><see langword="true"/> if the tile is checking liquid; otherwise, <see langword="false"/>.</value>
        public bool IsCheckingLiquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (Header & IsCheckingLiquidMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= IsCheckingLiquidMask;
                }
                else
                {
                    Header &= ~IsCheckingLiquidMask;
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
            readonly get => (Header & ShouldSkipLiquidMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    Header |= ShouldSkipLiquidMask;
                }
                else
                {
                    Header &= ~ShouldSkipLiquidMask;
                }
            }
        }
    }
}
