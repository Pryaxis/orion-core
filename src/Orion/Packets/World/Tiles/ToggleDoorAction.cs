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
    /// Specifies the toggle door action in a <see cref="ToggleDoorPacket"/>.
    /// </summary>
    public enum ToggleDoorAction : byte {
        /// <summary>
        /// Indicates that a door should be opened.
        /// </summary>
        OpenDoor = 0,

        /// <summary>
        /// Indicates that a door should be closed.
        /// </summary>
        CloseDoor = 1,

        /// <summary>
        /// Indicates that a trapdoor should be opened.
        /// </summary>
        OpenTrapdoor = 2,

        /// <summary>
        /// Indicates that a trapdoor should be closed.
        /// </summary>
        CloseTrapdoor = 3,

        /// <summary>
        /// Indicates that a tall gate should be opened.
        /// </summary>
        OpenTallGate = 4,

        /// <summary>
        /// Indicates that a tall gate should be closed.
        /// </summary>
        CloseTallGate = 5
    }
}
