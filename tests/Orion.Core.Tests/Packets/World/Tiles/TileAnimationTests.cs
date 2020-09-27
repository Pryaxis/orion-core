using System;
using System.Collections.Generic;
using System.Text;
using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class TileAnimationTests
    {
        private readonly byte[] _bytes = { 11, 0, 77, 1, 0, 2, 0, 3, 0, 4, 0 };

        [Fact]
        public void AnimationType_Set_Get()
        {
            var packet = new TileAnimation();

            packet.AnimationType = 1;

            Assert.Equal(1, packet.AnimationType);
        }

        [Fact]
        public void TileType_Set_Get()
        {
            var packet = new TileAnimation();

            packet.TileType = 1;

            Assert.Equal(1, packet.TileType);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new TileAnimation();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new TileAnimation();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<TileAnimation>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.AnimationType);
            Assert.Equal(2, packet.TileType);
            Assert.Equal(3, packet.X);
            Assert.Equal(4, packet.Y);
        }
    }
}
