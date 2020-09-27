using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcImmunityTests
    {
        private readonly byte[] _bytes = { 12, 0, 131, 5, 0, 1, 60, 0, 0, 0, 0, 0 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcImmunity();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldUpdateImmunity_Set_Get(bool value)
        {
            var packet = new NpcImmunity();

            packet.ShouldUpdateImmunity = value;

            Assert.Equal(value, packet.ShouldUpdateImmunity);
        }

        [Fact]
        public void ImmuneTime_Set_Get()
        {
            var packet = new NpcImmunity();

            packet.ImmuneTime = 1;

            Assert.Equal(1, packet.ImmuneTime);
        }

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new NpcImmunity();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcImmunity>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.NpcIndex);
            Assert.True(packet.ShouldUpdateImmunity);
            Assert.Equal(60, packet.ImmuneTime);
            Assert.Equal(0, packet.PlayerIndex);
        }
    }
}
