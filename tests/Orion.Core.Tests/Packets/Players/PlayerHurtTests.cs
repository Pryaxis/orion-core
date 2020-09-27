using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerHurtTests
    {
        private readonly byte[] _bytes = { 15, 0, 117, 1, 97, 50, 0, 0, 4, 5, 2, 0, 3, 0, 4 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerHurt();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void Context_Set_Get()
        {
            var packet = new PlayerHurt();

            packet.Context = new PlayerDeathReason() { KillingNpcIndex = 5 };

            Assert.Equal(5, packet.Context.KillingNpcIndex.Value);
        }

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new PlayerHurt();

            packet.Damage = 123;

            Assert.Equal(123, packet.Damage);
        }

        [Fact]
        public void HitDirection_Set_Get()
        {
            var packet = new PlayerHurt();

            packet.HitDirection = 1;

            Assert.Equal(1, packet.HitDirection);
        }

        [Fact]
        public void CooldownCounter_Set_Get()
        {
            var packet = new PlayerHurt();

            packet.CooldownCounter = 1;

            Assert.Equal(1, packet.CooldownCounter);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsCritical_Set_Get(bool value)
        {
            var packet = new PlayerHurt();

            packet.IsCritical = value;

            Assert.Equal(value, packet.IsCritical);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPvP_Set_Get(bool value)
        {
            var packet = new PlayerHurt();

            packet.IsPvP = value;

            Assert.Equal(value, packet.IsPvP);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerHurt>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(50, packet.Context.KillerIndex!.Value);
            Assert.Equal(1024, packet.Context.ItemType!.Value);
            Assert.Equal(5, packet.Context.ItemPrefix!.Value);
            Assert.Equal(2, packet.Damage);
            Assert.Equal(3, packet.HitDirection);
            Assert.Equal(4, packet.CooldownCounter);
        }
    }
}
