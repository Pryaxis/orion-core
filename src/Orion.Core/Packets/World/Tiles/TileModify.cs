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
using System.Runtime.InteropServices;
using Orion.Core.Utils;
using Orion.Core.World.Tiles;

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// A packet sent to modify a tile.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct TileModify : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(5)] private ushort _data;
        [FieldOffset(7)] private byte _data2;

        /// <summary>
        /// Gets or sets the modification.
        /// </summary>
        /// <value>The modification.</value>
        [field: FieldOffset(0)] public TileModification Modification { get; set; }

        /// <summary>
        /// Gets or sets the tile's X coordinate.
        /// </summary>
        /// <value>The tile's X coordinate.</value>
        [field: FieldOffset(1)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the tile's Y coordinate.
        /// </summary>
        /// <value>The tile's Y coordinate.</value>
        [field: FieldOffset(3)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the block ID. <i>This is only applicable if <see cref="Modification"/> involves placing a
        /// block!</i>
        /// </summary>
        /// <value>The block ID.</value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Modification"/> does not involve placing a block.
        /// </exception>
        public BlockId BlockId
        {
            get
            {
                if (Modification != TileModification.PlaceBlock && Modification != TileModification.ReplaceBlock)
                {
                    throw new InvalidOperationException("Modification does not involve placing a block");
                }

                return (BlockId)_data;
            }

            set
            {
                if (Modification != TileModification.PlaceBlock && Modification != TileModification.ReplaceBlock)
                {
                    throw new InvalidOperationException("Modification does not involve placing a block");
                }

                _data = (ushort)value;
            }
        }

        /// <summary>
        /// Gets or sets the block style. <i>This is only applicable if <see cref="Modification"/> involves placing a
        /// block!</i>
        /// </summary>
        /// <value>The block style.</value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Modification"/> does not involve placing a block.
        /// </exception>
        public byte BlockStyle
        {
            get
            {
                if (Modification != TileModification.PlaceBlock && Modification != TileModification.ReplaceBlock)
                {
                    throw new InvalidOperationException("Modification does not involve placing a block");
                }

                return _data2;
            }

            set
            {
                if (Modification != TileModification.PlaceBlock && Modification != TileModification.ReplaceBlock)
                {
                    throw new InvalidOperationException("Modification does not involve placing a block");
                }

                _data2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the wall ID. <i>This is only applicable if <see cref="Modification"/> involves placing a
        /// wall!</i>
        /// </summary>
        /// <value>The wall ID.</value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Modification"/> does not involve placing a wall.
        /// </exception>
        public WallId WallId
        {
            get
            {
                if (Modification != TileModification.PlaceWall && Modification != TileModification.ReplaceWall)
                {
                    throw new InvalidOperationException("Modification does not involve placing a wall");
                }

                return (WallId)_data;
            }

            set
            {
                if (Modification != TileModification.PlaceWall && Modification != TileModification.ReplaceWall)
                {
                    throw new InvalidOperationException("Modification does not involve placing a wall");
                }

                _data = (ushort)value;
            }
        }

        /// <summary>
        /// Gets or sets the block shape. <i>This is only applicable if <see cref="Modification"/> involves sloping a
        /// block!</i>
        /// </summary>
        /// <value>The slope.</value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Modification"/> does not involve sloping a block.
        /// </exception>
        public BlockShape BlockShape
        {
            get
            {
                if (Modification != TileModification.SlopeBlock && Modification != TileModification.SlopeAndHammerBlock)
                {
                    throw new InvalidOperationException("Modification does not involve sloping a block");
                }

                return _data > 0 ? (BlockShape)(_data + 1) : BlockShape.Normal;
            }

            set
            {
                if (Modification != TileModification.SlopeBlock && Modification != TileModification.SlopeAndHammerBlock)
                {
                    throw new InvalidOperationException("Modification does not involve sloping a block");
                }

                if (value != BlockShape.Normal)
                {
                    _data = (ushort)(value - 1);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the modification is a failure. <i>This is only applicable if
        /// <see cref="Modification"/> involves breaking a block or wall!</i>
        /// </summary>
        /// <value><see langword="true"/> if the modification is a failure; otherwise, <see langword="false"/>.</value>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Modification"/> does not involve breaking a block or wall.
        /// </exception>
        public bool IsFailure
        {
            get
            {
                if (Modification != TileModification.BreakBlock && Modification != TileModification.BreakWall &&
                    Modification != TileModification.BreakBlockItemless &&
                    Modification != TileModification.BreakBlockRequest)
                {
                    throw new InvalidOperationException("Modification does not involve breaking a block or wall");
                }

                return _data == 1;
            }

            set
            {
                if (Modification != TileModification.BreakBlock && Modification != TileModification.BreakWall &&
                    Modification != TileModification.BreakBlockItemless &&
                    Modification != TileModification.BreakBlockRequest)
                {
                    throw new InvalidOperationException("Modification does not involve breaking a block or wall");
                }

                _data = (ushort)(value ? 1 : 0);
            }
        }

        PacketId IPacket.Id => PacketId.TileModify;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 8);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 8);

        /// <summary>
        /// Specifies the tile modification in a <see cref="TileModify"/>.
        /// </summary>
        public enum TileModification : byte
        {
            /// <summary>
            /// Indicates that a block should be broken.
            /// </summary>
            BreakBlock = 0,

            /// <summary>
            /// Indicates that a block should be placed.
            /// </summary>
            PlaceBlock = 1,

            /// <summary>
            /// Indicates that a wall should be broken.
            /// </summary>
            BreakWall = 2,

            /// <summary>
            /// Indicates that a wall should be placed.
            /// </summary>
            PlaceWall = 3,

            /// <summary>
            /// Indicates that a block should be broken itemlessly.
            /// </summary>
            BreakBlockItemless = 4,

            /// <summary>
            /// Indicates that red wire should be placed.
            /// </summary>
            PlaceRedWire = 5,

            /// <summary>
            /// Indicates that red wire should be broken.
            /// </summary>
            BreakRedWire = 6,

            /// <summary>
            /// Indicates that a block should be hammered.
            /// </summary>
            HammerBlock = 7,

            /// <summary>
            /// Indicates that an actuator should be placed.
            /// </summary>
            PlaceActuator = 8,

            /// <summary>
            /// Indicates that an actuator should be broken.
            /// </summary>
            BreakActuator = 9,

            /// <summary>
            /// Indicates that blue wire should be placed.
            /// </summary>
            PlaceBlueWire = 10,

            /// <summary>
            /// Indicates that blue wire should be broken.
            /// </summary>
            BreakBlueWire = 11,

            /// <summary>
            /// Indicates that green wire should be placed.
            /// </summary>
            PlaceGreenWire = 12,

            /// <summary>
            /// Indicates that green wire should be broken.
            /// </summary>
            BreakGreenWire = 13,

            /// <summary>
            /// Indicates that a block should be sloped.
            /// </summary>
            SlopeBlock = 14,

            /// <summary>
            /// Indicates that a track should be modified.
            /// </summary>
            ModifyTrack = 15,

            /// <summary>
            /// Indicates that yellow wire should be placed.
            /// </summary>
            PlaceYellowWire = 16,

            /// <summary>
            /// Indicates that yellow wire should be broken.
            /// </summary>
            BreakYellowWire = 17,

            /// <summary>
            /// Indicates that a logic gate should be modified.
            /// </summary>
            ModifyLogicGate = 18,

            /// <summary>
            /// Indicates that a block should be actuated.
            /// </summary>
            ActuateBlock = 19,

            /// <summary>
            /// Indicates that a block should be broken as a request.
            /// </summary>
            BreakBlockRequest = 20,

            /// <summary>
            /// Indicates that a block should be replaced.
            /// </summary>
            ReplaceBlock = 21,

            /// <summary>
            /// Indicates that a wall should be replaced.
            /// </summary>
            ReplaceWall = 22,

            /// <summary>
            /// Indicates that a block should be sloped and then hammered.
            /// </summary>
            SlopeAndHammerBlock = 23
        }
    }
}
