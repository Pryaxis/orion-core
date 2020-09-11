using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class PlayerEnterTests
    {
        private readonly byte[] _bytes = { 3, 0, 49 };

        [Fact]
        public void Read()
        {
            _ = TestUtils.ReadPacket<PlayerEnter>(_bytes, PacketContext.Server);
        }
    }
}
