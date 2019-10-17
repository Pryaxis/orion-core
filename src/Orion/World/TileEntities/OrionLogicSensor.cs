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

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;

namespace Orion.World.TileEntities {
    internal sealed class OrionLogicSensor : OrionTileEntity<TerrariaLogicSensor>, ILogicSensor {
        public LogicSensorType LogicSensorType {
            get => (LogicSensorType)Wrapped.logicCheck;
            set => Wrapped.logicCheck = (TerrariaLogicSensor.LogicCheckType)value;
        }

        public bool IsActivated {
            get => Wrapped.On;
            set => Wrapped.On = value;
        }

        public OrionLogicSensor(TerrariaLogicSensor terrariaLogicSensor) : base(terrariaLogicSensor) { }
        
        // Not localized because this string is developer-facing.
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => Index >= 0 ? $"#: {Index}" : "logic sensor instance";
    }
}
