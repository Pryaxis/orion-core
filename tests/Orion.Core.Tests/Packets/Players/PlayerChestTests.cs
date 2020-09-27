using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerChestTests
    {
        private readonly byte[] _bytes = { 6, 0, 80, 1, 2, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerChest();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void ChestIndex_Set_Get()
        {
            var packet = new PlayerChest();

            packet.ChestIndex = 1;

            Assert.Equal(1, packet.ChestIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerChest>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.ChestIndex);
        }
    }
}
