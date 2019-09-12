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

using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionLogicSensor : OrionTileEntity<TGCTE.TELogicSensor>, ILogicSensor {
        public LogicSensorType SensorType {
            get => (LogicSensorType)Wrapped.logicCheck;
            set => Wrapped.logicCheck = (TGCTE.TELogicSensor.LogicCheckType)value;
        }

        public bool IsActivated {
            get => Wrapped.On;
            set => Wrapped.On = value;
        }

        public int Data {
            get => Wrapped.CountedData;
            set => Wrapped.CountedData = value;
        }

        public OrionLogicSensor(TGCTE.TELogicSensor logicSensor) : base(logicSensor) { }
    }
}
