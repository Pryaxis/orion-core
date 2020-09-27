using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerEmojiTests
    {
        private readonly byte[] _bytes = { 5, 0, 120, 4, 5 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerEmoji();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void EmojiId_Set_Get()
        {
            var packet = new PlayerEmoji();

            packet.EmojiId = 1;

            Assert.Equal(1, packet.EmojiId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerEmoji>(_bytes, PacketContext.Server);

            Assert.Equal(4, packet.PlayerIndex);
            Assert.Equal(5, packet.EmojiId);
        }
    }
}
