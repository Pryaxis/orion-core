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

using System;
using Orion.Networking.Packets.World.Tiles;

namespace Orion.Networking.World.Tiles {
    /// <summary>
    /// Specifies the mass wire operations in a <see cref="MassWireOperationPacket"/>.
    /// </summary>
    [Flags]
    public enum MassWireOperations : byte {
        /// <summary>
        /// Indicates nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that red wires should be modified.
        /// </summary>
        RedWire = 1,

        /// <summary>
        /// Indicates that green wires should be modified.
        /// </summary>
        GreenWire = 2,

        /// <summary>
        /// Indicates that blue wires should be modified.
        /// </summary>
        BlueWire = 4,

        /// <summary>
        /// Indicates that yellow wires should be modified.
        /// </summary>
        YellowWire = 8,

        /// <summary>
        /// Indicates that actuators should be modified.
        /// </summary>
        Actuator = 16,

        /// <summary>
        /// Indicates that components should be removed.
        /// </summary>
        Remove = 32
    }
}
