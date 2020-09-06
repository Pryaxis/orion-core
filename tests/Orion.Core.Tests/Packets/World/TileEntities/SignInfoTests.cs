using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    public sealed class SignInfoTests
    {
        private readonly byte[] _bytes = { 21, 0, 47, 5, 100, 0, 100, 0, 11, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 250, 0 };

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<SignInfo>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.SignIndex);
            Assert.Equal(100, packet.X);
            Assert.Equal(100, packet.Y);
            Assert.Equal("ABCDEFGHIJK", packet.Text);
            Assert.Equal(250, packet.PlayerIndex);
        }
    }
}
