using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class EventOccurredTests
    {
        private readonly byte[] _bytes = { 5, 0, 98, 1, 0 };

        [Fact]
        public void EventId_Set_Get()
        {
            var packet = new EventOccurred();

            packet.EventId = 1;

            Assert.Equal(1, packet.EventId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<EventOccurred>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.EventId);
        }
    }
}
