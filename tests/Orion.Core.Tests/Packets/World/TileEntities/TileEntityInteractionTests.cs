using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.TileEntities
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class TileEntityInteractionTests
    {
        private readonly byte[] _bytes = { 8, 0, 122, 1, 0, 0, 0, 2 };

        [Fact]
        public void TileEntityId_Set_Get()
        {
            var packet = new TileEntityInteraction();

            packet.TileEntityId = 1;

            Assert.Equal(1, packet.TileEntityId);
        }

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new TileEntityInteraction();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<TileEntityInteraction>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.TileEntityId);
            Assert.Equal(2, packet.PlayerIndex);
        }
    }
}
