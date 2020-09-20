using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    public sealed class PlayerPortalTests
    {
        private readonly byte[] _bytes = { 22, 0, 96, 1, 2, 0, 0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 0, 64 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void PortalColorIndex_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.PortalColorIndex = 1;

            Assert.Equal(1, packet.PortalColorIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void VelocityX_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.VelocityX = 1;

            Assert.Equal(1, packet.VelocityX);
        }

        [Fact]
        public void VelocityY_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.VelocityY = 1;

            Assert.Equal(1, packet.VelocityY);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerPortal>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.PortalColorIndex);
            Assert.Equal(2, packet.X);
            Assert.Equal(2, packet.Y);
            Assert.Equal(2, packet.VelocityX);
            Assert.Equal(2, packet.VelocityY);
        }
    }
}
