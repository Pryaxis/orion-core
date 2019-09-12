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

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Specifies an invasion type that is transmitted over the network.
    /// </summary>
    public enum NetworkInvasionType : short {
        /// <summary>
        /// Indicates the Goblins.
        /// </summary>
        Goblins = -1,

        /// <summary>
        /// Indicates the Frost Legion.
        /// </summary>
        FrostLegion = -2,

        /// <summary>
        /// Indicates the Pirates.
        /// </summary>
        Pirates = -3,

        /// <summary>
        /// Indicates a pumpkin moon.
        /// </summary>
        PumpkinMoon = -4,

        /// <summary>
        /// Indicates a frost moon.
        /// </summary>
        FrostMoon = -5,

        /// <summary>
        /// Indicates a solar eclipse.
        /// </summary>
        Eclipse = -6,

        /// <summary>
        /// Indicates the Martians.
        /// </summary>
        Martians = -7,

        /// <summary>
        /// Indicates the Moon Lord.
        /// </summary>
        MoonLord = -8
    }
}
