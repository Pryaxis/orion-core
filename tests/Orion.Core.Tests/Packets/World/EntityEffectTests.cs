using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    public sealed class EntityEffectTests
    {
        private readonly byte[] _bytes = { 5, 0, 51, 250, 2 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new EntityEffect();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Theory]
        [InlineData(EntityEffectAction.PlaySound)]
        [InlineData(EntityEffectAction.SpawnSkeletron)]
        public void Action_Set_Get(EntityEffectAction action)
        {
            var packet = new EntityEffect();

            packet.Action = action;

            Assert.Equal(action, packet.Action);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<EntityEffect>(_bytes, PacketContext.Server);

            Assert.Equal(250, packet.PlayerIndex);
            Assert.Equal(EntityEffectAction.PlaySound, packet.Action);
        }
    }
}
