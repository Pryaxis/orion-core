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
using Destructurama.Attributed;

namespace Orion.Core.World.Tiles
{
    /// <summary>
    /// Represents an optimized Terraria tile.
    /// </summary>
    /// <remarks>
    /// This structure is not thread-safe.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 13)]
    public struct Tile
    {
        // The shifts for the tile header.
        private const int BlockColorShift = 0;
        private const int SlopeShift = 12;
        private const int WallColorShift = 16;
        private const int LiquidTypeShift = 21;

        // The masks for the tile header.
        private const uint BlockColorMask /*       */ = 0b_00000000_00000000_00000000_00011111;
        private const uint IsBlockActiveMask /*    */ = 0b_00000000_00000000_00000000_00100000;
        private const uint IsBlockActuatedMask /*  */ = 0b_00000000_00000000_00000000_01000000;
        private const uint HasRedWireMask /*       */ = 0b_00000000_00000000_00000000_10000000;
        private const uint HasBlueWireMask /*      */ = 0b_00000000_00000000_00000001_00000000;
        private const uint HasGreenWireMask /*     */ = 0b_00000000_00000000_00000010_00000000;
        private const uint IsBlockHalvedMask /*    */ = 0b_00000000_00000000_00000100_00000000;
        private const uint HasActuatorMask /*      */ = 0b_00000000_00000000_00001000_00000000;
        private const uint SlopeMask /*            */ = 0b_00000000_00000000_01110000_00000000;
        private const uint WallColorMask /*        */ = 0b_00000000_00011111_00000000_00000000;
        private const uint LiquidTypeMask /*       */ = 0b_00000000_01100000_00000000_00000000;
        private const uint HasYellowWireMask /*    */ = 0b_00000000_10000000_00000000_00000000;

        [FieldOffset(4)] private byte _liquidAmount;

        /// <summary>
        /// Gets or sets the tile's block ID.
        /// </summary>
        /// <value>The tile's block ID.</value>
        [field: FieldOffset(0)] public BlockId BlockId { get; set; }

        /// <summary>
        /// Gets or sets the tile's wall ID.
        /// </summary>
        /// <value>The tile's wall ID.</value>
        [field: FieldOffset(2)] public WallId WallId { get; set; }

        /// <summary>
        /// Gets or sets the tile's liquid.
        /// </summary>
        /// <value>The tile's liquid.</value>
        public Liquid Liquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new Liquid((LiquidType)((Header & LiquidTypeMask) >> LiquidTypeShift), _liquidAmount);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Header = (Header & ~LiquidTypeMask) | (((uint)value.Type << LiquidTypeShift) & LiquidTypeMask);
                _liquidAmount = value.Amount;
            }
        }

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
        [NotLogged]
        [field: FieldOffset(9)] public uint Header { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block is active.
        /// </summary>
        /// <value><see langword="true"/> if the block is active; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActive
        {
            readonly get => GetFlag(IsBlockActiveMask);
            set => SetFlag(IsBlockActiveMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is actuated.
        /// </summary>
        /// <value><see langword="true"/> if the block is actuated; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActuated
        {
            readonly get => GetFlag(IsBlockActuatedMask);
            set => SetFlag(IsBlockActuatedMask, value);
        }

        /// <summary>
        /// Gets or sets the block's color.
        /// </summary>
        /// <value>The block's color.</value>
        public PaintColor BlockColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header & BlockColorMask) >> BlockColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~BlockColorMask) | (((uint)value << BlockColorShift) & BlockColorMask);
        }

        /// <summary>
        /// Gets or sets the block's shape.
        /// </summary>
        /// <value>The block's shape.</value>
        public BlockShape BlockShape
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (GetFlag(IsBlockHalvedMask))
                {
                    return BlockShape.Halved;
                }

                var slope = Header & SlopeMask;
                return slope > 0 ? (BlockShape)((slope >> SlopeShift) + 1) : BlockShape.Normal;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Header &= ~(IsBlockHalvedMask | SlopeMask);

                if (value == BlockShape.Normal)
                {
                    return;
                }
                else if (value == BlockShape.Halved)
                {
                    Header |= IsBlockHalvedMask;
                }
                else
                {
                    Header = (Header & ~SlopeMask) | ((uint)(value - 1) << SlopeShift & SlopeMask);
                }
            }
        }

        /// <summary>
        /// Gets or sets the wall's color.
        /// </summary>
        /// <value>The wall's color.</value>
        public PaintColor WallColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header & WallColorMask) >> WallColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Header = (Header & ~WallColorMask) | (((uint)value << WallColorShift) & WallColorMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has red wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has red wire; otherwise, <see langword="false"/>.</value>
        public bool HasRedWire
        {
            readonly get => GetFlag(HasRedWireMask);
            set => SetFlag(HasRedWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has blue wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has blue wire; otherwise, <see langword="false"/>.</value>
        public bool HasBlueWire
        {
            readonly get => GetFlag(HasBlueWireMask);
            set => SetFlag(HasBlueWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public bool HasGreenWire
        {
            readonly get => GetFlag(HasGreenWireMask);
            set => SetFlag(HasGreenWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has yellow wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has yellow wire; otherwise, <see langword="false"/>.</value>
        public bool HasYellowWire
        {
            readonly get => GetFlag(HasYellowWireMask);
            set => SetFlag(HasYellowWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has an actuator.
        /// </summary>
        /// <value><see langword="true"/> if the tile has an actuator; otherwise, <see langword="false"/>.</value>
        public bool HasActuator
        {
            readonly get => GetFlag(HasActuatorMask);
            set => SetFlag(HasActuatorMask, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly bool GetFlag(uint mask) => (Header & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetFlag(uint mask, bool value)
        {
            if (value)
            {
                Header |= mask;
            }
            else
            {
                Header &= ~mask;
            }
        }
    }
}
