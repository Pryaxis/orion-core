using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class NpcEmoteTests
    {
        private readonly byte[] _bytes = { 13, 0, 91, 1, 0, 0, 0, 0, 2, 0, 3, 0, 4 };

        [Fact]
        public void EmoteBubbleId_Set_Get()
        {
            var packet = new NpcEmote();

            packet.EmoteBubbleId = 1;

            Assert.Equal(1, packet.EmoteBubbleId);
        }

        [Theory]
        [InlineData(WorldUiAnchorType.Npc)]
        [InlineData(WorldUiAnchorType.Player)]
        [InlineData(WorldUiAnchorType.Projectile)]
        public void UiAnchorType_Set_Get(WorldUiAnchorType value)
        {
            var packet = new NpcEmote();

            packet.UiAnchorType = value;

            Assert.Equal(value, packet.UiAnchorType);
        }

        [Fact]
        public void AnchorEntityIndex_Set_Get()
        {
            var packet = new NpcEmote();

            packet.AnchorEntityIndex = 1;

            Assert.Equal(1, packet.AnchorEntityIndex);
        }

        [Fact]
        public void LifeTime_Set_Get()
        {
            var packet = new NpcEmote();

            packet.LifeTime = 1;

            Assert.Equal(1, packet.LifeTime);
        }

        [Fact]
        public void EmoteId_Set_Get()
        {
            var packet = new NpcEmote();

            packet.EmoteId = 1;

            Assert.Equal(1, packet.EmoteId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcEmote>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.EmoteBubbleId);
            Assert.Equal(WorldUiAnchorType.Npc, packet.UiAnchorType);
            Assert.Equal(2, packet.AnchorEntityIndex);
            Assert.Equal(3, packet.LifeTime);
            Assert.Equal(4, packet.EmoteId);
        }
    }
}
