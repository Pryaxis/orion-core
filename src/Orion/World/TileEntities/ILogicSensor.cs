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

using Orion.Utils;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a Terraria logic sensor.
    /// </summary>
    /// <remarks>Logic sensors are wiring components that can be tripped depending on the sensor type.</remarks>
    public interface ILogicSensor : ITileEntity, IWrapping<TerrariaLogicSensor> {
        /// <summary>
        /// Gets or sets the logic sensor's type.
        /// </summary>
        /// <remarks>The logic sensor's type.</remarks>
        LogicSensorType SensorType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logic sensor is activated.
        /// </summary>
        /// <remarks>
        /// <see langword="true"/> if the logic sensor is actived; otherwise, <see langword="false"/>.
        /// </remarks>
        bool IsActivated { get; set; }
    }
}
