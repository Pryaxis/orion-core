// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using JetBrains.Annotations;
using Orion.Networking.Packets.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Specifies a tile modification in a <see cref="TileModificationPacket"/>.
    /// </summary>
    [PublicAPI]
    public enum TileModification : byte {
#pragma warning disable 1591
        BreakBlock = 0,
        PlaceBlock = 1,
        BreakWall = 2,
        PlaceWall = 3,
        BreakBlockNoItems = 4,
        PlaceRedWire = 5,
        RemoveRedWire = 6,
        HalveBlock = 7,
        PlaceActuator = 8,
        RemoveActuator = 9,
        PlaceBlueWire = 10,
        RemoveBlueWire = 11,
        PlaceGreenWire = 12,
        RemoveGreenWire = 13,
        SlopeBlock = 14,
        FrameTrack = 15,
        PlaceYellowWire = 16,
        RemoveYellowWire = 17,
        PokeLogicGate = 18,
        ActuateBlock = 19
#pragma warning restore 1591
    }
}
