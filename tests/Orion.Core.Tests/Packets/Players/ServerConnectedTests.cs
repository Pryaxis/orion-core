using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class ServerConnectedTests
    {
        private readonly byte[] _bytes = { 3, 0, 129 };

        [Fact]
        public void Read()
        {
            var _ = TestUtils.ReadPacket<ServerConnected>(_bytes, PacketContext.Server);
        }
    }
}
