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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.World.TileEntities;
using Xunit;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class SensorTests
    {
        private readonly byte[] _bytes = { 2, 10, 0, 0, 0, 0, 1, 100, 0, 1, 1 };

        [Fact]
        public void Id_Get()
        {
            var sensor = new Sensor();

            Assert.Equal(TileEntityId.Sensor, sensor.Id);
        }

        [Fact]
        public void Type_Set_Get()
        {
            var sensor = new Sensor();

            sensor.Type = SensorType.LogicDay;

            Assert.Equal(SensorType.LogicDay, sensor.Type);
        }

        [Fact]
        public void IsActivated_Set_Get()
        {
            var sensor = new Sensor();

            sensor.IsActivated = true;

            Assert.True(sensor.IsActivated);
        }

        [Fact]
        public void Read()
        {
            var sensor = TestUtils.ReadTileEntity<Sensor>(_bytes, true);

            Assert.Equal(10, sensor.Index);
            Assert.Equal(256, sensor.X);
            Assert.Equal(100, sensor.Y);

            Assert.Equal(SensorType.LogicDay, sensor.Type);
            Assert.True(sensor.IsActivated);
        }
    }
}
