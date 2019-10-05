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

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a (generalized) tile entity type.
    /// </summary>
    public enum TileEntityType : short {
        /// <summary>
        /// Indicates a target dummy.
        /// </summary>
        TargetDummy = 0,

        /// <summary>
        /// Indicates an item frame.
        /// </summary>
        ItemFrame = 1,

        /// <summary>
        /// Indicates a logic sensor.
        /// </summary>
        LogicSensor = 2,

        // Use large values to represent chests and signs, as they are not *actually* tile entities in Terraria.

        /// <summary>
        /// Indicates a chest.
        /// </summary>
        Chest = short.MaxValue - 1,

        /// <summary>
        /// Indicates a sign.
        /// </summary>
        Sign = short.MaxValue
    }
}
