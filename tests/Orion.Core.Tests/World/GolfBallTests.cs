using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Packets;
using Orion.Core.Packets.World;
using Xunit;

namespace Orion.Core.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class GolfBallTests
    {
        private readonly byte[] _bytes = { 12, 0, 128, 1, 2, 0, 3, 0, 4, 0, 5, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new GolfBall();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new GolfBall();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new GolfBall();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void NumberOfHits_Set_Get()
        {
            var packet = new GolfBall();

            packet.NumberOfHits = 1;

            Assert.Equal(1, packet.NumberOfHits);
        }

        [Fact]
        public void ProjectileId_Set_Get()
        {
            var packet = new GolfBall();

            packet.ProjectileId = 1;

            Assert.Equal(1, packet.ProjectileId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<GolfBall>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.X);
            Assert.Equal(3, packet.Y);
            Assert.Equal(4, packet.NumberOfHits);
            Assert.Equal(5, packet.ProjectileId);
        }
    }
}
