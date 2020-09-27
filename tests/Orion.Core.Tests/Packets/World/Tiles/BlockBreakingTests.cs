using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class BlockBreakingTests
    {
        private readonly byte[] _bytes = { 9, 0, 125, 1, 2, 0, 3, 0, 4 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new BlockBreaking();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new BlockBreaking();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new BlockBreaking();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void PickDamage_Set_Get()
        {
            var packet = new BlockBreaking();

            packet.PickDamage = 1;

            Assert.Equal(1, packet.PickDamage);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<BlockBreaking>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.X);
            Assert.Equal(3, packet.Y);
            Assert.Equal(4, packet.PickDamage);
        }
    }
}
