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

using Orion.Core.Items;

namespace Orion.Core.World.TileEntities
{
    /// <summary>
    /// Specifies a sensor's type.
    /// </summary>
    public enum SensorType : byte
    {
        /// <summary>
        /// Indicates an <see cref="ItemId.LogicSensorDay"/>.
        /// </summary>
        LogicDay = 1,

        /// <summary>
        /// Indicates an <see cref="ItemId.LogicSensorNight"/>.
        /// </summary>
        LogicNight = 2,

        /// <summary>
        /// Indicates a <see cref="ItemId.LogicSensorPlayerAbove"/>.
        /// </summary>
        LogicPlayerAbove = 3,

        /// <summary>
        /// Indicates a <see cref="ItemId.LiquidSensorWater"/>.
        /// </summary>
        LiquidWater = 4,

        /// <summary>
        /// Indicates a <see cref="ItemId.LiquidSensorLava"/>.
        /// </summary>
        LiquidLava = 5,

        /// <summary>
        /// Indicates a <see cref="ItemId.LiquidSensorHoney"/>.
        /// </summary>
        LiquidHoney = 6,

        /// <summary>
        /// Indicates a <see cref="ItemId.LiquidSensorAny"/>.
        /// </summary>
        LiquidAny = 7
    }
}
