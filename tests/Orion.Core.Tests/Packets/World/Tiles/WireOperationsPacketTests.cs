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
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class WireOperationsPacketTests
    {
        private readonly byte[] _bytes = { 12, 0, 109, 0, 1, 100, 0, 100, 0, 0, 1, 16 };

        [Fact]
        public void StartX_Set_Get()
        {
            var packet = new WireOperationsPacket();

            packet.StartX = 256;

            Assert.Equal(256, packet.StartX);
        }

        [Fact]
        public void StartY_Set_Get()
        {
            var packet = new WireOperationsPacket();

            packet.StartY = 100;

            Assert.Equal(100, packet.StartY);
        }

        [Fact]
        public void EndX_Set_Get()
        {
            var packet = new WireOperationsPacket();

            packet.EndX = 100;

            Assert.Equal(100, packet.EndX);
        }

        [Fact]
        public void EndY_Set_Get()
        {
            var packet = new WireOperationsPacket();

            packet.EndY = 256;

            Assert.Equal(256, packet.EndY);
        }

        [Fact]
        public void Operations_Set_Get()
        {
            var packet = new WireOperationsPacket();

            packet.Operations = WireOperationsPacket.WireOperations.Actuator;

            Assert.Equal(WireOperationsPacket.WireOperations.Actuator, packet.Operations);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<WireOperationsPacket>(_bytes, PacketContext.Server);

            Assert.Equal(256, packet.StartX);
            Assert.Equal(100, packet.StartY);
            Assert.Equal(100, packet.EndX);
            Assert.Equal(256, packet.EndY);
            Assert.Equal(WireOperationsPacket.WireOperations.Actuator, packet.Operations);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(_bytes, PacketContext.Server);
        }
    }
}
