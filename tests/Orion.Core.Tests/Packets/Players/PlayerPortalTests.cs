using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
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
        public void Position_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.Position = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void Velocity_Set_Get()
        {
            var packet = new PlayerPortal();

            packet.Velocity = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Velocity);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerPortal>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.PortalColorIndex);
            Assert.Equal(new Vector2f(2, 2), packet.Position);
            Assert.Equal(new Vector2f(2, 2), packet.Velocity);
        }
    }
}
