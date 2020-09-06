using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class WorldBiomesTests
    {
        private readonly byte[] _bytes = { 6, 0, 57, 10, 20, 30 };

        [Fact]
        public void Good_Set_Get()
        {
            var packet = new WorldBiomes();

            packet.Good = 50;

            Assert.Equal(50, packet.Good);
        }

        [Fact]
        public void Evil_Set_Get()
        {
            var packet = new WorldBiomes();

            packet.Evil = 50;

            Assert.Equal(50, packet.Evil);
        }

        [Fact]
        public void Crimson_Set_Get()
        {
            var packet = new WorldBiomes();

            packet.Crimson = 50;

            Assert.Equal(50, packet.Crimson);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<WorldBiomes>(_bytes, PacketContext.Server);

            Assert.Equal(10, packet.Good);
            Assert.Equal(20, packet.Evil);
            Assert.Equal(30, packet.Crimson);
        }
    }
}
