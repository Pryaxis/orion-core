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

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the type of modification in a <see cref="TileModificationPacket"/>.
    /// </summary>
    public enum TileModificationType : byte {
#pragma warning disable 1591
        DestroyBlock = 0,
        PlaceBlock,
        DestroyWall,
        PlaceWall,
        DestroyBlockNoItems,
        PlaceRedWire,
        RemoveRedWire,
        HalveBlock,
        PlaceActuator,
        RemoveActuator,
        PlaceBlueWire,
        RemoveBlueWire,
        PlaceGreenWire,
        RemoveGreenWire,
        SlopeBlock,
        FrameTrack,
        PlaceYellowWire,
        RemoveYellowWire,
        PokeLogicGate,
        ActuateBlock,
#pragma warning restore 1591
    }
}
