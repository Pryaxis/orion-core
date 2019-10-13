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
using FluentAssertions;
using Xunit;
using TerrariaLogicSensor = Terraria.GameContent.Tile_Entities.TELogicSensor;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionLogicSensorTests {
        [Fact]
        public void LogicSensorType_Get() {
            var terrariaLogicSensor = new TerrariaLogicSensor {
                logicCheck = TerrariaLogicSensor.LogicCheckType.Water
            };
            var logicSensor = new OrionLogicSensor(terrariaLogicSensor);

            logicSensor.LogicSensorType.Should().Be(LogicSensorType.Water);
        }

        [Fact]
        public void LogicSensorType_Set() {
            var terrariaLogicSensor = new TerrariaLogicSensor();
            var logicSensor = new OrionLogicSensor(terrariaLogicSensor);

            logicSensor.LogicSensorType = LogicSensorType.Water;

            terrariaLogicSensor.logicCheck.Should().Be(TerrariaLogicSensor.LogicCheckType.Water);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsActivated_Get(bool isActivated) {
            var terrariaLogicSensor = new TerrariaLogicSensor { On = isActivated };
            var logicSensor = new OrionLogicSensor(terrariaLogicSensor);

            logicSensor.IsActivated.Should().Be(isActivated);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsActivated_Set(bool isActivated) {
            var terrariaLogicSensor = new TerrariaLogicSensor();
            var logicSensor = new OrionLogicSensor(terrariaLogicSensor);

            logicSensor.IsActivated = isActivated;

            terrariaLogicSensor.On.Should().Be(isActivated);
        }
    }
}
