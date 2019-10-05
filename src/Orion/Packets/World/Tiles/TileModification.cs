// Copyright (c) 2019 Pryaxis & Orion Contributors
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

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Specifies a tile modification in a <see cref="TileModificationPacket"/>.
    /// </summary>
    public enum TileModification : byte {
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
        /// Indicates that a block should be broken with no items dropping.
        /// </summary>
        BreakBlockNoItems = 4,

        /// <summary>
        /// Indicates that red wire should be placed.
        /// </summary>
        PlaceRedWire = 5,

        /// <summary>
        /// Indicates that red wire should be removed.
        /// </summary>
        RemoveRedWire = 6,

        /// <summary>
        /// Indicates that a block should be halved.
        /// </summary>
        HalveBlock = 7,

        /// <summary>
        /// Indicates that an actuator should be placed.
        /// </summary>
        PlaceActuator = 8,

        /// <summary>
        /// Indicates that an actuator should be removed.
        /// </summary>
        RemoveActuator = 9,

        /// <summary>
        /// Indicates that blue wire should be placed.
        /// </summary>
        PlaceBlueWire = 10,

        /// <summary>
        /// Indicates that blue wire should be removed.
        /// </summary>
        RemoveBlueWire = 11,

        /// <summary>
        /// Indicates that green wire should be placed.
        /// </summary>
        PlaceGreenWire = 12,

        /// <summary>
        /// Indicates that green wire should be removed.
        /// </summary>
        RemoveGreenWire = 13,

        /// <summary>
        /// Indicates that a block should be sloped.
        /// </summary>
        SlopeBlock = 14,

        /// <summary>
        /// Indicates that a minecart track should be modified.
        /// </summary>
        ModifyMinecartTrack = 15,

        /// <summary>
        /// Indicates that yellow wire should be placed.
        /// </summary>
        PlaceYellowWire = 16,

        /// <summary>
        /// Indicates that yellow wire should be removed.
        /// </summary>
        RemoveYellowWire = 17,

        /// <summary>
        /// Indicates that a logic gate should be modified.
        /// </summary>
        ModifyLogicGate = 18,

        /// <summary>
        /// Indicates that a block should be actuated.
        /// </summary>
        ActuateBlock = 19
    }
}
