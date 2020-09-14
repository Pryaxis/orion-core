using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    public sealed class NpcRemoveRevengeTests
    {
        private readonly byte[] _bytes = { 7, 0, 127, 1, 0, 0, 0 };

        [Fact]
        public void UniqueId_Set_Get()
        {
            var packet = new NpcRemoveRevenge();

            packet.UniqueId = 1;

            Assert.Equal(1, packet.UniqueId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcRemoveRevenge>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.UniqueId);
        }
    }
}
