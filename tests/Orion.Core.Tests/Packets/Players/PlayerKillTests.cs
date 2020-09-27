using Orion.Core.Packets.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerKillTests
    {
        private readonly byte[] _bytes = { 10, 0, 118, 1, 8, 2, 0, 2, 0, 1 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerKill();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void DeathReason_Set_Get()
        {
            var packet = new PlayerKill();

            packet.DeathReason = new PlayerDeathReason() { KillerIndex = 1, ItemType = 2 };

            Assert.Equal(1, packet.DeathReason.KillerIndex.Value);
            Assert.Equal(2, packet.DeathReason.ItemType.Value);
        }

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new PlayerKill();

            packet.Damage = 1;

            Assert.Equal(1, packet.Damage);
        }

        [Fact]
        public void HitDirection_Set_Get()
        {
            var packet = new PlayerKill();

            packet.HitDirection = 1;

            Assert.Equal(1, packet.HitDirection);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPvP_Set_Get(bool value)
        {
            var packet = new PlayerKill();

            packet.IsPvP = value;

            Assert.Equal(value, packet.IsPvP);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerKill>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(CauseOfDeath.LavaDamage, packet.DeathReason.CauseOfDeath);
            Assert.Equal(512, packet.Damage);
            Assert.True(packet.IsPvP);
        }
    }
}
