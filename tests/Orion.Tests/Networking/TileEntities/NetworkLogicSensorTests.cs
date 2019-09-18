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
using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Networking.TileEntities {
    public class NetworkLogicSensorTests {
        [Fact]
        public void SetSensorType_MarksAsDirty() {
            var logicSensor = new NetworkLogicSensor();
            logicSensor.SensorType = LogicSensorType.Day;

            logicSensor.ShouldBeDirty();
        }

        [Fact]
        public void SetSensorType_NullValue_ThrowsArgumentNullException() {
            var logicSensor = new NetworkLogicSensor();
            Action action = () => logicSensor.SensorType = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetIsActivated_MarksAsDirty() {
            var logicSensor = new NetworkLogicSensor();
            logicSensor.IsActivated = false;

            logicSensor.ShouldBeDirty();
        }
    }
}
