using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerQuickStackTests
    {
        private readonly byte[] _bytes = { 4, 0, 85, 1 };

        [Fact]
        public void InventorySlot_Set_Get()
        {
            var packet = new PlayerQuickStack();

            packet.InventorySlot = 1;

            Assert.Equal(1, packet.InventorySlot);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerQuickStack>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.InventorySlot);
        }
    }
}
