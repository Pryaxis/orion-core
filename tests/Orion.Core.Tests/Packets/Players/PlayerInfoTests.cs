using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class PlayerInfoTests
    {
        private readonly byte[] _bytes = 
        { 
            38, 0, 13, 130, 0b_00011010, 0b00000110, 0b01011100, 0, 5, 121, 233, 246, 66, 254, 100, 228, 67, 
            0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 200, 66, 0, 0, 22, 67, 0, 192, 10, 68, 225,170, 38, 68
        };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.PlayerIndex = 15;

            Assert.Equal(15, packet.PlayerIndex);
        }

        [Fact]
        public void SelectedItemSlot_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.SelectedItemSlot = 5;

            Assert.Equal(5, packet.SelectedItemSlot);
        }

        [Fact]
        public void PositionX_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.PositionX = 666.67F;

            Assert.Equal(666.67F, packet.PositionX);
        }

        [Fact]
        public void PositionY_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.PositionY = 666.67F;

            Assert.Equal(666.67F, packet.PositionY);
        }

        [Fact]
        public void VelocityX_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.VelocityX = 666.67F;

            Assert.Equal(666.67F, packet.VelocityX);
        }

        [Fact]
        public void VelocityY_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.VelocityY = 666.67F;

            Assert.Equal(666.67F, packet.VelocityY);
        }

        [Fact]
        public void PotionOfReturnOriginX_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.PotionOfReturnOriginX = 666.67F;

            Assert.Equal(666.67F, packet.PotionOfReturnOriginX);
        }

        [Fact]
        public void PotionOfReturnOriginY_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.PotionOfReturnOriginY = 666.67F;

            Assert.Equal(666.67F, packet.PotionOfReturnOriginY);
        }

        [Fact]
        public void HomePositionX_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.HomePositionX = 666.67F;

            Assert.Equal(666.67F, packet.HomePositionX);
        }

        [Fact]
        public void HomePositionY_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.HomePositionY = 666.67F;

            Assert.Equal(666.67F, packet.HomePositionY);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlUp_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlUp = value;

            Assert.Equal(value, packet.IsControlUp);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlDown_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlDown = value;

            Assert.Equal(value, packet.IsControlDown);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlLeft_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlLeft = value;

            Assert.Equal(value, packet.IsControlLeft);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlRight_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlRight = value;

            Assert.Equal(value, packet.IsControlRight);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlJump_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlJump = value;

            Assert.Equal(value, packet.IsControlJump);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsControlUseItem_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsControlUseItem = value;

            Assert.Equal(value, packet.IsControlUseItem);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSleeping_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsSleeping = value;

            Assert.Equal(value, packet.IsSleeping);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPulleyEnabled_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsPulleyEnabled = value;

            Assert.Equal(value, packet.IsPulleyEnabled);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDirectionRight_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsDirectionRight = value;

            Assert.Equal(value, packet.IsDirectionRight);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldUpdateVelocity_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.ShouldUpdateVelocity = value;

            Assert.Equal(value, packet.ShouldUpdateVelocity);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsVortexStealthActive_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsVortexStealthActive = value;

            Assert.Equal(value, packet.IsVortexStealthActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsGravityInverted_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsGravityInverted = value;

            Assert.Equal(value, packet.IsGravityInverted);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsShieldRaised_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsShieldRaised = value;

            Assert.Equal(value, packet.IsShieldRaised);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHoveringUp_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsHoveringUp = value;

            Assert.Equal(value, packet.IsHoveringUp);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsHoveringDown_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsHoveringDown = value;

            Assert.Equal(value, packet.IsHoveringDown);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsVoidVaultEnabled_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsVoidVaultEnabled = value;

            Assert.Equal(value, packet.IsVoidVaultEnabled);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsSitting_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsSitting = value;

            Assert.Equal(value, packet.IsSitting);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasCompletedDD2Event_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.HasCompletedDD2Event = value;

            Assert.Equal(value, packet.HasCompletedDD2Event);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPettingAnimal_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsPettingAnimal = value;

            Assert.Equal(value, packet.IsPettingAnimal);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsPettingSmallAnimal_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.IsPettingSmallAnimal = value;

            Assert.Equal(value, packet.IsPettingSmallAnimal);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasUsedPotionOfReturn_Set_Get(bool value)
        {
            var packet = new PlayerInfo();

            packet.HasUsedPotionOfReturn = value;

            Assert.Equal(value, packet.HasUsedPotionOfReturn);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerInfo>(_bytes, PacketContext.Server);

            Assert.Equal(130, packet.PlayerIndex);
            Assert.Equal(5, packet.SelectedItemSlot);
            Assert.Equal(123.456F, packet.PositionX);
            Assert.Equal(456.789F, packet.PositionY);
            Assert.Equal(2F, packet.VelocityX);
            Assert.Equal(2F, packet.VelocityY);
            Assert.Equal(100F, packet.PotionOfReturnOriginX);
            Assert.Equal(150F, packet.PotionOfReturnOriginY);
            Assert.Equal(555F, packet.HomePositionX);
            Assert.Equal(666.67F, packet.HomePositionY);
            Assert.False(packet.IsControlUp);
            Assert.True(packet.IsControlDown);
            Assert.False(packet.IsControlLeft);
            Assert.True(packet.IsControlRight);
            Assert.True(packet.IsControlJump);
            Assert.False(packet.IsPulleyEnabled);
            Assert.True(packet.IsDirectionRight);
            Assert.True(packet.ShouldUpdateVelocity);
            Assert.False(packet.IsVortexStealthActive);
            Assert.True(packet.IsGravityInverted);
            Assert.False(packet.IsShieldRaised);
            Assert.False(packet.IsHoveringUp);
            Assert.False(packet.IsVoidVaultEnabled);
            Assert.True(packet.IsSitting);
            Assert.True(packet.HasCompletedDD2Event);
            Assert.True(packet.IsPettingAnimal);
            Assert.False(packet.IsPettingSmallAnimal);
            Assert.True(packet.HasUsedPotionOfReturn);
            Assert.False(packet.IsHoveringDown);
        }
    }
}
