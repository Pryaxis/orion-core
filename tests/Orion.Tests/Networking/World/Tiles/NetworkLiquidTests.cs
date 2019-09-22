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

using System.IO;
using System.Text;
using FluentAssertions;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Networking.World.Tiles {
    public class NetworkLiquidTests {
        [Fact]
        public void SetSimpleProperties_MarkAsDirty() {
            var liquid = new NetworkLiquid();

            liquid.SetSimplePropertiesShouldMarkAsDirty();
        }

        public static byte[] Bytes = {100, 0, 0, 1, 255, 0};

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReadFromReader_IsCorrect(bool shouldSwapXY) {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream, Encoding.UTF8)) {
                var liquid = NetworkLiquid.ReadFromReader(reader, shouldSwapXY);

                liquid.TileX.Should().Be((short)(shouldSwapXY ? 256 : 100));
                liquid.TileY.Should().Be((short)(shouldSwapXY ? 100 : 256));
                liquid.LiquidAmount.Should().Be(255);
                liquid.LiquidType.Should().Be(LiquidType.Water);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeserializeAndSerialize_SameTileLiquid(bool shouldSwapXY) {
            using (var inStream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(inStream, Encoding.UTF8))
            using (var outStream = new MemoryStream())
            using (var writer = new BinaryWriter(outStream, Encoding.UTF8)) {
                var liquid = NetworkLiquid.ReadFromReader(reader, shouldSwapXY);
                liquid.WriteToWriter(writer, shouldSwapXY);

                outStream.ToArray().Should().BeEquivalentTo(Bytes);
            }
        }
    }
}
