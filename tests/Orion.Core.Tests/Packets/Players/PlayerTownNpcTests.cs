using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class PlayerTownNpcTests
    {
        private readonly byte[] _bytes = { 6, 0, 40, 15, 100, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerTownNpc();

            packet.PlayerIndex = 250;

            Assert.Equal(250, packet.PlayerIndex);
        }

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new PlayerTownNpc();

            packet.NpcIndex = 50;

            Assert.Equal(50, packet.NpcIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerTownNpc>(_bytes, PacketContext.Server);

            Assert.Equal(15, packet.PlayerIndex);
            Assert.Equal(100, packet.NpcIndex);
        }
    }
}
