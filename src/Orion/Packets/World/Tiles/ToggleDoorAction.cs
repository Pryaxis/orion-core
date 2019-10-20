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

using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Specifies the toggle door action in a <see cref="ToggleDoorPacket"/>.
    /// </summary>
    public enum ToggleDoorAction : byte {
        /// <summary>
        /// Indicates that <see cref="BlockType.ClosedDoors"/> should be closed.
        /// </summary>
        OpenDoor = 0,

        /// <summary>
        /// Indicates that <see cref="BlockType.OpenDoors"/> should be closed.
        /// </summary>
        CloseDoor = 1,

        /// <summary>
        /// Indicates that a <see cref="BlockType.ClosedTrapDoor"/> should be opened.
        /// </summary>
        OpenTrapDoor = 2,

        /// <summary>
        /// Indicates that an <see cref="BlockType.OpenTrapDoor"/> should be closed.
        /// </summary>
        CloseTrapDoor = 3,

        /// <summary>
        /// Indicates that a <see cref="BlockType.ClosedTallGate"/> should be opened.
        /// </summary>
        OpenTallGate = 4,

        /// <summary>
        /// Indicates that an <see cref="BlockType.OpenTallGate"/> should be closed.
        /// </summary>
        CloseTallGate = 5
    }
}
