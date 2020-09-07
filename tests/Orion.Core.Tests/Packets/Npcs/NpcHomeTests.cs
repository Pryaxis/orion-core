using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    public sealed class NpcHomeTests
    {
        private readonly byte[] _bytes = { 10, 0, 60, 150, 0, 50, 0, 100, 0, 1 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcHome();

            packet.NpcIndex = 199;

            Assert.Equal(199, packet.NpcIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new NpcHome();

            packet.X = 100;

            Assert.Equal(100, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new NpcHome();

            packet.Y = 100;

            Assert.Equal(100, packet.Y);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHomeless_Set_Get(bool isHomeless)
        {
            var packet = new NpcHome();

            packet.IsHomeless = isHomeless;

            Assert.Equal(isHomeless, packet.IsHomeless);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcHome>(_bytes, PacketContext.Server);

            Assert.Equal(150, packet.NpcIndex);
            Assert.Equal(50, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.True(packet.IsHomeless);
        }
    }
}
