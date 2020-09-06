using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World.Tiles
{
    public sealed class DoorToggleTests
    {
        private readonly byte[] _bytes = { 9, 0, 19, 5, 0, 4, 150, 0, 0 };

        [Theory]
        [InlineData(DoorAction.Open)]
        [InlineData(DoorAction.Close)]
        [InlineData(DoorAction.TrapdoorOpen)]
        public void Action_Set_Get(DoorAction action)
        {
            var packet = new DoorToggle();

            packet.Action = action;

            Assert.Equal(action, packet.Action);
        }

        [Fact]
        public void TileX_Set_Get()
        {
            var packet = new DoorToggle();

            packet.TileX = 100;

            Assert.Equal(100, packet.TileX);
        }

        [Fact]
        public void TileY_Set_Get()
        {
            var packet = new DoorToggle();

            packet.TileY = 100;

            Assert.Equal(100, packet.TileY);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Direction_Set_Get(bool isRightDirection)
        {
            var packet = new DoorToggle();

            packet.IsRightDirectionOpen = isRightDirection;

            Assert.Equal(isRightDirection, packet.IsRightDirectionOpen);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<DoorToggle>(_bytes, PacketContext.Server);

            Assert.Equal(DoorAction.TallGateClose, packet.Action);
            Assert.Equal(1024, packet.TileX);
            Assert.Equal(150, packet.TileY);
            Assert.False(packet.IsRightDirectionOpen);
        }
    }
}
