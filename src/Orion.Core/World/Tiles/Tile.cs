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
using System.Diagnostics;
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
    [DebuggerStepThrough]
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct Tile : IEquatable<Tile>
    {
        private const int BlockColorShift = 0;
        private const int BlockShapeShift = 5;

        private const int LiquidTypeShift = 6;

        private const int WallColorShift = 0;

        private const byte BlockColorMask /*      */ = 0b_00011111;
        private const byte BlockShapeMask /*      */ = 0b_11100000;

        private const byte HasRedWireMask /*      */ = 0b_00000001;
        private const byte HasBlueWireMask /*     */ = 0b_00000010;
        private const byte HasGreenWireMask /*    */ = 0b_00000100;
        private const byte HasYellowWireMask /*   */ = 0b_00001000;
        private const byte HasActuatorMask /*     */ = 0b_00010000;
        private const byte IsBlockActuatedMask /* */ = 0b_00100000;
        private const byte LiquidTypeMask /*      */ = 0b_11000000;

        private const byte WallColorMask /*       */ = 0b_00011111;

        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(2)] private ushort _wallId;
        [FieldOffset(4)] private byte _liquidAmount;
        [FieldOffset(8)] private byte _bytes2;  // Used to obtain an interior reference.

        /// <summary>
        /// Gets or sets the tile's block ID.
        /// </summary>
        /// <value>The tile's block ID.</value>
        [field: FieldOffset(0)] public BlockId BlockId { get; set; }

        /// <summary>
        /// Gets or sets the tile's wall ID.
        /// </summary>
        /// <value>The tile's wall ID.</value>
        public WallId WallId
        {
            // We purposefully ignore the top bit of the wall ID, to allow a bit of state to be stored there for other
            // purposes. This cuts down on the usable number of wall IDs by a factor of two, but it should be fine.

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (WallId)(_wallId & 0x7fff);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert((ushort)value <= 0x7fff);

                _wallId = (ushort)((_wallId & 0x8000) | (ushort)value);
            }
        }

        /// <summary>
        /// Gets or sets the tile's liquid.
        /// </summary>
        /// <value>The tile's liquid.</value>
        public Liquid Liquid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => new Liquid((LiquidType)((Header2 & LiquidTypeMask) >> LiquidTypeShift), _liquidAmount);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert((byte)value.Type <= 0x03);

                Header2 = (byte)((Header2 & ~LiquidTypeMask) | ((byte)value.Type << LiquidTypeShift));
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
        /// Gets or sets the tile's first header.
        /// </summary>
        /// <value>The tile's first header.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotLogged]
        [field: FieldOffset(9)] public byte Header { get; set; }

        /// <summary>
        /// Gets or sets the tile's second header.
        /// </summary>
        /// <value>The tile's second header.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotLogged]
        [field: FieldOffset(10)] public byte Header2 { get; set; }

        /// <summary>
        /// Gets or sets the tile's third header.
        /// </summary>
        /// <value>The tile's third header.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotLogged]
        [field: FieldOffset(11)] public byte Header3 { get; set; }

        /// <summary>
        /// Gets or sets the block's color.
        /// </summary>
        /// <value>The block's color.</value>
        public PaintColor BlockColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header & BlockColorMask) >> BlockColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert((byte)value <= 0x1f);

                Header = (byte)((Header & ~BlockColorMask) | ((byte)value << BlockColorShift));
            }
        }

        /// <summary>
        /// Gets or sets the block's shape.
        /// </summary>
        /// <value>The block's shape.</value>
        public BlockShape BlockShape
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (BlockShape)((Header & BlockShapeMask) >> BlockShapeShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert((byte)value <= 0x07);

                Header = (byte)((Header & ~BlockShapeMask) | ((byte)value << BlockShapeShift));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has red wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has red wire; otherwise, <see langword="false"/>.</value>
        public bool HasRedWire
        {
            readonly get => GetWiringFlag(HasRedWireMask);
            set => SetWiringFlag(HasRedWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has blue wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has blue wire; otherwise, <see langword="false"/>.</value>
        public bool HasBlueWire
        {
            readonly get => GetWiringFlag(HasBlueWireMask);
            set => SetWiringFlag(HasBlueWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has green wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has green wire; otherwise, <see langword="false"/>.</value>
        public bool HasGreenWire
        {
            readonly get => GetWiringFlag(HasGreenWireMask);
            set => SetWiringFlag(HasGreenWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has yellow wire.
        /// </summary>
        /// <value><see langword="true"/> if the tile has yellow wire; otherwise, <see langword="false"/>.</value>
        public bool HasYellowWire
        {
            readonly get => GetWiringFlag(HasYellowWireMask);
            set => SetWiringFlag(HasYellowWireMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tile has an actuator.
        /// </summary>
        /// <value><see langword="true"/> if the tile has an actuator; otherwise, <see langword="false"/>.</value>
        public bool HasActuator
        {
            readonly get => GetWiringFlag(HasActuatorMask);
            set => SetWiringFlag(HasActuatorMask, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the block is actuated.
        /// </summary>
        /// <value><see langword="true"/> if the block is actuated; otherwise, <see langword="false"/>.</value>
        public bool IsBlockActuated
        {
            readonly get => GetWiringFlag(IsBlockActuatedMask);
            set => SetWiringFlag(IsBlockActuatedMask, value);
        }

        /// <summary>
        /// Gets or sets the wall's color.
        /// </summary>
        /// <value>The wall's color.</value>
        public PaintColor WallColor
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => (PaintColor)((Header3 & WallColorMask) >> WallColorShift);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Debug.Assert((byte)value <= 0x1f);

                Header3 = (byte)((Header3 & ~WallColorMask) | ((byte)value << WallColorShift));
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Tile other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Tile other)
        {
            var hasFrames = BlockId.HasFrames();

            // Two tiles are compared by checking the tiles' bytes directly. The first set of 8 bytes are checked,
            // followed by the second set of 4 bytes.
            //
            // The first set of 8 bytes are:
            // | yyyyyyyy | xxxxxxxx | xxxxxxxx | llllllll | ?wwwwwww | wwwwwwww | ?bbbbbbb | bbbbbbbb |

            var mask = hasFrames
                ? 0b_11111111_11111111_11111111_11111111_01111111_11111111_01111111_11111111UL
                : 0b_00000000_00000000_00000000_11111111_01111111_11111111_01111111_11111111UL;
            if ((Unsafe.As<byte, ulong>(ref _bytes) & mask) != (Unsafe.As<byte, ulong>(ref other._bytes) & mask))
            {
                return false;
            }

            // The second set of 4 bytes are:
            // | ???hhhhh | hhhhhhhh | hhhhhhhh | yyyyyyyy |
            //
            // Note that the liquid type should not be checked if the liquid amount is 0.

            var mask2 = hasFrames
                ? 0b_00011111_11111111_11111111_11111111U
                : 0b_00011111_11111111_11111111_00000000U;
            if (_liquidAmount == 0)
            {
                mask2 &= 0b_11111111_00111111_11111111_11111111U;
            }

            return (Unsafe.As<byte, uint>(ref _bytes2) & mask2) == (Unsafe.As<byte, uint>(ref other._bytes2) & mask2);
        }

        /// <summary>
        /// Returns the hash code of the tile.
        /// </summary>
        /// <returns>The hash code of the tile.</returns>
        public override int GetHashCode()
        {
            var hasFrames = BlockId.HasFrames();

            // The hash code is calculated similarly as above.

            var mask = hasFrames
                ? 0b_11111111_11111111_11111111_11111111_01111111_11111111_01111111_11111111UL
                : 0b_00000000_00000000_00000000_11111111_01111111_11111111_01111111_11111111UL;

            var mask2 = hasFrames
                ? 0b_00011111_11111111_11111111_11111111U
                : 0b_00011111_11111111_11111111_00000000U;
            if (_liquidAmount == 0)
            {
                mask2 &= 0b_11111111_00111111_11111111_11111111U;
            }

            return HashCode.Combine(
                Unsafe.As<byte, ulong>(ref _bytes) & mask, Unsafe.As<byte, uint>(ref _bytes2) & mask2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly bool GetWiringFlag(byte mask) => (Header2 & mask) != 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWiringFlag(byte mask, bool value)
        {
            if (value)
            {
                Header2 |= mask;
            }
            else
            {
                Header2 &= (byte)~mask;
            }
        }
    }
}
