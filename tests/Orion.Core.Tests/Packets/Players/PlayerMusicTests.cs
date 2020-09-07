using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class PlayerMusicTests
    {
        private readonly byte[] _bytes = { 8, 0, 58, 250, 0, 0, 0, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerMusic();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void Note_Set_Get()
        {
            var packet = new PlayerMusic();

            packet.Note = 3.33333F;

            Assert.Equal(3.33333F, packet.Note);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerMusic>(_bytes, PacketContext.Server);

            Assert.Equal(250, packet.PlayerIndex);
            Assert.Equal(0, packet.Note);
        }
    }
}
