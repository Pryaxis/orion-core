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
    /// Represents a space-optimized Terraria tile.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Tile {
        private const int BlockColorShift = 0;
        private const int SlopeShift = 12;
        private const int WallColorShift = 16;
        private const int LiquidShift = 21;
        
        // The masks. These follow the same layout in Terraria's `Tile` class.
        private const int BlockColorMask /*       */ = 0b_00000000_00000000_00000000_00011111;
        private const int IsBlockActiveMask /*    */ = 0b_00000000_00000000_00000000_00100000;
        private const int IsBlockActuatedMask /*  */ = 0b_00000000_00000000_00000000_01000000;
        private const int HasRedWireMask /*       */ = 0b_00000000_00000000_00000000_10000000;
        private const int HasBlueWireMask /*      */ = 0b_00000000_00000000_00000001_00000000;
        private const int HasGreenWireMask /*     */ = 0b_00000000_00000000_00000010_00000000;
        private const int IsBlockHalvedMask /*    */ = 0b_00000000_00000000_00000100_00000000;
        private const int HasActuatorMask /*      */ = 0b_00000000_00000000_00001000_00000000;
        private const int SlopeMask /*            */ = 0b_00000000_00000000_01110000_00000000;
        private const int WallColorMask /*        */ = 0b_00000000_00011111_00000000_00000000;
        private const int LiquidMask /*           */ = 0b_00000000_01100000_00000000_00000000;
        private const int HasYellowWireMask /*    */ = 0b_00000000_10000000_00000000_00000000;
        private const int IsCheckingLiquidMask /* */ = 0b_00001000_00000000_00000000_00000000;
        private const int ShouldSkipLiquidMask /* */ = 0b_00010000_00000000_00000000_00000000;

        [FieldOffset(9)] private int _header;

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
            set => _header = (_header & ~BlockColorMask) | (((int)value << BlockColorShift) & BlockColorMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is active.
        /// </summary>
        /// <value><see langword="true"/> if the block is active; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockActive {
            readonly get => (_header & IsBlockActiveMask) != 0;
            set => _header = (_header & ~IsBlockActiveMask) | (*(int*)&value * IsBlockActiveMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is actuated.
        /// </summary>
        /// <value><see langword="true"/> if the block is actuated; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockActuated {
            readonly get => (_header & IsBlockActuatedMask) != 0;
            set => _header = (_header & ~IsBlockActuatedMask) | (*(int*)&value * IsBlockActuatedMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has red wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has red wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasRedWire {
            readonly get => (_header & HasRedWireMask) != 0;
            set => _header = (_header & ~HasRedWireMask) | (*(int*)&value * HasRedWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has blue wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has blue wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasBlueWire {
            readonly get => (_header & HasBlueWireMask) != 0;
            set => _header = (_header & ~HasBlueWireMask) | (*(int*)&value * HasBlueWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasGreenWire {
            readonly get => (_header & HasGreenWireMask) != 0;
            set => _header = (_header & ~HasGreenWireMask) | (*(int*)&value * HasGreenWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is halved.
        /// </summary>
        /// <value><see langword="true"/> if the block is halved; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsBlockHalved {
            readonly get => (_header & IsBlockHalvedMask) != 0;
            set => _header = (_header & ~IsBlockHalvedMask) | (*(int*)&value * IsBlockHalvedMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has an actuator.
        /// </summary>
        /// <value><see langword="true"/> if the tile has an actuator; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasActuator {
            readonly get => (_header & HasActuatorMask) != 0;
            set => _header = (_header & ~HasActuatorMask) | (*(int*)&value * HasActuatorMask);
        }

        /// <summary>
        /// Gets or sets the slope.
        /// </summary>
        /// <value>The slope.</value>
        public Slope Slope {
            readonly get => (Slope)((_header & SlopeMask) >> SlopeShift);
            set => _header = (_header & ~SlopeMask) | (((int)value << SlopeShift) & SlopeMask);
        }

        /// <summary>
        /// Gets or sets the wall color.
        /// </summary>
        /// <value>The wall color.</value>
        public PaintColor WallColor {
            readonly get => (PaintColor)((_header & WallColorMask) >> WallColorShift);
            set => _header = (_header & ~WallColorMask) | (((int)value << WallColorShift) & WallColorMask);
        }

        /// <summary>
        /// Gets or sets the liquid.
        /// </summary>
        /// <value>The liquid.</value>
        public Liquid Liquid {
            readonly get => (Liquid)((_header & LiquidMask) >> LiquidShift);
            set => _header = (_header & ~LiquidMask) | (((int)value << LiquidShift) & LiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public unsafe bool HasYellowWire {
            readonly get => (_header & HasYellowWireMask) != 0;
            set => _header = (_header & ~HasYellowWireMask) | (*(int*)&value * HasYellowWireMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile is checking liquid.
        /// </summary>
        /// <value><see langword="true"/> if the tile is checking liquid; otherwise, <see langword="false"/>.</value>
        public unsafe bool IsCheckingLiquid {
            readonly get => (_header & IsCheckingLiquidMask) != 0;
            set => _header = (_header & ~IsCheckingLiquidMask) | (*(int*)&value * IsCheckingLiquidMask);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile should skip liquids.
        /// </summary>
        /// <value><see langword="true"/> if the tile should skip liquids; otherwise, <see langword="false"/>.</value>
        public unsafe bool ShouldSkipLiquid {
            readonly get => (_header & ShouldSkipLiquidMask) != 0;
            set => _header = (_header & ~ShouldSkipLiquidMask) | (*(int*)&value * ShouldSkipLiquidMask);
        }
    }
}
