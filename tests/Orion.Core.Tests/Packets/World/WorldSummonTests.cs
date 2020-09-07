using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class WorldSummonTests
    {
        private readonly byte[] _bytes = { 7, 0, 61, 5, 0, 0, 1  };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new WorldSummon();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void NpcId_Set_Get()
        {
            var packet = new WorldSummon();

            packet.NpcId = 600;

            Assert.Equal(600, packet.NpcId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<WorldSummon>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(256, packet.NpcId);
        }
    }
}
