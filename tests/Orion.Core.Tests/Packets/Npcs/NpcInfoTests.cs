using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcInfoTests
    {
        private readonly byte[] _bytes = 
        { 
            42, 0, 23, 1, 0, 0, 0, 0, 0, 0, 64, 0, 0, 64, 64, 0, 0, 128, 64, 0, 0, 160, 64, 7, 0, 75, 7, 0, 0, 128, 63, 8, 0, 9,
            0, 0, 32, 65, 2, 0, 4, 5
        };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcInfo();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new NpcInfo();

            packet.Position = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void Velocity_Set_Get()
        {
            var packet = new NpcInfo();

            packet.Velocity = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Velocity);
        }

        [Fact]
        public void TargetIndex_Set_Get()
        {
            var packet = new NpcInfo();

            packet.TargetIndex = 1;

            Assert.Equal(1, packet.TargetIndex);
        }

        [Fact]
        public void AdditionalInformation_Set_Get()
        {
            var packet = new NpcInfo();

            packet.AdditionalInformation[0] = 1F;

            Assert.Equal(1F, packet.AdditionalInformation[0]);
        }

        [Fact]
        public void NetId_Set_Get()
        {
            var packet = new NpcInfo();

            packet.NetId = 1;

            Assert.Equal(1, packet.NetId);
        }

        [Fact]
        public void DifficultyScalingOverride_Set_Get()
        {
            var packet = new NpcInfo();

            packet.DifficultyScalingOverride = 2;

            Assert.Equal(2, packet.DifficultyScalingOverride);
        }

        [Fact]
        public void StrengthMultiplierOverride_Set_Get()
        {
            var packet = new NpcInfo();

            packet.StrengthMultiplierOverride = 2;

            Assert.Equal(2, packet.StrengthMultiplierOverride);
        }

        [Fact]
        public void Health_Set_Get()
        {
            var packet = new NpcInfo();

            packet.Health = 100;

            Assert.Equal(100, packet.Health);
        }

        [Fact]
        public void ReleaseOwnerIndex_Set_Get()
        {
            var packet = new NpcInfo();

            packet.ReleaseOwnerIndex = 1;

            Assert.Equal(1, packet.ReleaseOwnerIndex.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSpriteFacingRight_Set_Get(bool value)
        {
            var packet = new NpcInfo();

            packet.IsSpriteFacingRight = value;

            Assert.Equal(value, packet.IsSpriteFacingRight);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsMovingRight_Set_Get(bool value)
        {
            var packet = new NpcInfo();

            packet.IsMovingRight = value;

            Assert.Equal(value, packet.IsMovingRight);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsMovingDown_Set_Get(bool value)
        {
            var packet = new NpcInfo();

            packet.IsMovingDown = value;

            Assert.Equal(value, packet.IsMovingDown);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SpawnedFromStatue_Set_Get(bool value)
        {
            var packet = new NpcInfo();

            packet.SpawnedFromStatue = value;

            Assert.Equal(value, packet.SpawnedFromStatue);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcInfo>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.NpcIndex);
            Assert.Equal(new Vector2f(2, 3), packet.Position);
            Assert.Equal(new Vector2f(4, 5), packet.Velocity);
            Assert.Equal(7, packet.TargetIndex);
            Assert.Equal(1F, packet.AdditionalInformation[1]);
            Assert.Equal(8, packet.NetId);
            Assert.Equal(9, packet.DifficultyScalingOverride);
            Assert.Equal(10F, packet.StrengthMultiplierOverride);
            Assert.Equal(1024, packet.Health);
            Assert.Equal(5, packet.ReleaseOwnerIndex.Value);
        }
    }
}
