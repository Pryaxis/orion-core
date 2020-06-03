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

using System.Runtime.InteropServices;

namespace Orion.World.Tiles {
    /// <summary>
    /// Represents a space and speed-optimized Terraria tile.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Tile {
        internal const int BlockColorShift = 0;
        internal const int SlopeShift = 12;
        internal const int WallColorShift = 16;
        internal const int LiquidShift = 21;
        internal const int BlockFrameNumberShift = 24;
        
        // The masks. These follow roughly the same layout in Terraria's `Tile` class.
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
        public PaintColor BlockColor {
            readonly get => (PaintColor)((_header & BlockColorMask) >> BlockColorShift);
            set => _header = (_header & ~BlockColorMask) | (((uint)value << BlockColorShift) & BlockColorMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is active.
        /// </summary>
        /// <value><see langword="true"/> if the block is active; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockActive {
            readonly get => (_header & IsBlockActiveMask) != 0;
            set => _header = (_header & ~IsBlockActiveMask) | (*(uint*)&value * IsBlockActiveMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is actuated.
        /// </summary>
        /// <value><see langword="true"/> if the block is actuated; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockActuated {
            readonly get => (_header & IsBlockActuatedMask) != 0;
            set => _header = (_header & ~IsBlockActuatedMask) | (*(uint*)&value * IsBlockActuatedMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has red wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has red wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasRedWire {
            readonly get => (_header & HasRedWireMask) != 0;
            set => _header = (_header & ~HasRedWireMask) | (*(uint*)&value * HasRedWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has blue wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has blue wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasBlueWire {
            readonly get => (_header & HasBlueWireMask) != 0;
            set => _header = (_header & ~HasBlueWireMask) | (*(uint*)&value * HasBlueWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasGreenWire {
            readonly get => (_header & HasGreenWireMask) != 0;
            set => _header = (_header & ~HasGreenWireMask) | (*(uint*)&value * HasGreenWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is halved.
        /// </summary>
        /// <value><see langword="true"/> if the block is halved; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockHalved {
            readonly get => (_header & IsBlockHalvedMask) != 0;
            set => _header = (_header & ~IsBlockHalvedMask) | (*(uint*)&value * IsBlockHalvedMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has an actuator.
        /// </summary>
        /// <value><see langword="true"/> if the tile has an actuator; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasActuator {
            readonly get => (_header & HasActuatorMask) != 0;
            set => _header = (_header & ~HasActuatorMask) | (*(uint*)&value * HasActuatorMask);
        }

        /// <summary>
        /// Gets or sets the slope.
        /// </summary>
        /// <value>The slope.</value>
        public Slope Slope {
            readonly get => (Slope)((_header & SlopeMask) >> SlopeShift);
            set => _header = (_header & ~SlopeMask) | (((uint)value << SlopeShift) & SlopeMask);
        }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        /// <value>The wall color.</value>
        public PaintColor WallColor {
            readonly get => (PaintColor)((_header & WallColorMask) >> WallColorShift);
            set => _header = (_header & ~WallColorMask) | (((uint)value << WallColorShift) & WallColorMask);
        }

        /// <summary>
        /// Gets or sets the liquid.
        /// </summary>
        /// <value>The liquid.</value>
        public Liquid Liquid {
            readonly get => (Liquid)((_header & LiquidMask) >> LiquidShift);
            set => _header = (_header & ~LiquidMask) | (((uint)value << LiquidShift) & LiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasYellowWire {
            readonly get => (_header & HasYellowWireMask) != 0;
            set => _header = (_header & ~HasYellowWireMask) | (*(uint*)&value * HasYellowWireMask);
        }

        /// <summary>
        /// Gets or sets the block's frame number.
        /// </summary>
        /// <value>The block's frame number.</value>
        public byte BlockFrameNumber {
            readonly get => (byte)((_header & BlockFrameNumberMask) >> BlockFrameNumberShift);
            set {
                _header =
                    (_header & ~BlockFrameNumberMask) | (((uint)value << BlockFrameNumberShift) & BlockFrameNumberMask);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile is checking liquid.
        /// </summary>
        /// <value><see langword="true"/> if the tile is checking liquid; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsCheckingLiquid {
            readonly get => (_header & IsCheckingLiquidMask) != 0;
            set => _header = (_header & ~IsCheckingLiquidMask) | (*(uint*)&value * IsCheckingLiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile should skip liquids.
        /// </summary>
        /// <value><see langword="true"/> if the tile should skip liquids; otherwise, <see langword="false"/>.</value>
        public unsafe bool ShouldSkipLiquid {
            readonly get => (_header & ShouldSkipLiquidMask) != 0;
            set => _header = (_header & ~ShouldSkipLiquidMask) | (*(uint*)&value * ShouldSkipLiquidMask);
        }
    }
}
