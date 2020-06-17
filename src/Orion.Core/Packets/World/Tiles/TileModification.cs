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

namespace Orion.Core.Packets.World.Tiles
{
    /// <summary>
    /// Specifies the modification in a <see cref="TileModifyPacket"/>.
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
        /// Indicates that a container should be broken.
        /// </summary>
        BreakContainer = 20,

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
