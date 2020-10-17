using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class EntityTeleportTests
    {
        private readonly byte[] _bytes = { 19, 0, 65, 14, 1, 0, 164, 112, 157, 63, 133, 235, 145, 64, 2, 3, 0, 0, 0 };

        [Fact]
        public void EntityIndex_Set_Get()
        {
            var packet = new EntityTeleport();

            packet.EntityIndex = 1;

            Assert.Equal(1, packet.EntityIndex);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new EntityTeleport();

            packet.Position = new Utils.Vector2f(1.23F, 4.56F);

            Assert.Equal(new Utils.Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void Style_Set_Get()
        {
            var packet = new EntityTeleport();

            packet.Style = 1;

            Assert.Equal(1, packet.Style);
        }

        [Fact]
        public void ExtraInfo_Set_Get()
        {
            var packet = new EntityTeleport();

            packet.ExtraInfo = 1;

            Assert.Equal(1, packet.ExtraInfo);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UseTargetsPosition_Set_Get(bool value)
        {
            var packet = new EntityTeleport();

            packet.UseTargetsPosition = value;

            Assert.Equal(value, packet.UseTargetsPosition);
        }

        [Theory]
        [InlineData(TeleportationType.Npc)]
        [InlineData(TeleportationType.Player)]
        [InlineData(TeleportationType.PlayerToPlayer)]
        public void TeleportationType_Set_Get(TeleportationType value)
        {
            var packet = new EntityTeleport();

            packet.TeleportationType = value;

            Assert.Equal(value, packet.TeleportationType);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<EntityTeleport>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.EntityIndex);
            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
            Assert.Equal(2, packet.Style);
            Assert.Equal(3, packet.ExtraInfo);
            Assert.Equal(TeleportationType.PlayerToPlayer, packet.TeleportationType);
            Assert.True(packet.UseTargetsPosition);
        }
    }
}
