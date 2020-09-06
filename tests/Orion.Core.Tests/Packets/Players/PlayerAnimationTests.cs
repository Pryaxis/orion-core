using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class PlayerAnimationTests
    {
        private readonly byte[] _bytes = { 10, 0, 41, 200, 0, 0, 0, 63, 50, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerAnimation();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void ItemRotation_Set_Get()
        {
            var packet = new PlayerAnimation();

            packet.ItemRotation = 0.5F;

            Assert.Equal(0.5F, packet.ItemRotation);
        }

        [Fact]
        public void AnimationTime_Set_Get()
        {
            var packet = new PlayerAnimation();

            packet.AnimationTime = 10;

            Assert.Equal(10, packet.AnimationTime);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerAnimation>(_bytes, PacketContext.Server);

            Assert.Equal(200, packet.PlayerIndex);
            Assert.Equal(0.5F, packet.ItemRotation);
            Assert.Equal(50, packet.AnimationTime);
        }
    }
}
