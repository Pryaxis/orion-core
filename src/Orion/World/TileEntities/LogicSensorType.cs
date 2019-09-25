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

using System.Diagnostics.CodeAnalysis;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Specifies a logic sensor type.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum LogicSensorType : byte {
        /// <summary>
        /// Indicates nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a daytime sensor.
        /// </summary>
        Daytime = 1,

        /// <summary>
        /// Indicates a nighttime sensor.
        /// </summary>
        Nighttime = 2,

        /// <summary>
        /// Indicates a player above sensor.
        /// </summary>
        PlayerAbove = 3,

        /// <summary>
        /// Indicates a water sensor.
        /// </summary>
        Water = 4,

        /// <summary>
        /// Indicates a lava sensor.
        /// </summary>
        Lava = 5,

        /// <summary>
        /// Indicates a honey sensor.
        /// </summary>
        Honey = 6,

        /// <summary>
        /// Indicates a liquid sensor.
        /// </summary>
        Liquid = 7
    }
}
