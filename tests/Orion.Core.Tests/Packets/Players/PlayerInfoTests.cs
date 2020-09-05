using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class PlayerInfoTests
    {
        private readonly byte[] _bytes = { 38, 0, 13, 130, 0b_00011010, 0b00000010, 0b00011100, 0, 5, 121, 233, 246, 66, 254, 100, 228, 67, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 66, 0, 0, 22, 67, 0, 192, 10, 68, 225, 170, 38, 68 };

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

        [Fact]
        public void IsControlUp_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlUp = true;

            Assert.True(packet.IsControlUp);
        }

        [Fact]
        public void IsControlDown_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlDown = true;

            Assert.True(packet.IsControlDown);
        }

        [Fact]
        public void IsControlLeft_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlLeft = true;

            Assert.True(packet.IsControlLeft);
        }

        [Fact]
        public void IsControlRight_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlRight = true;

            Assert.True(packet.IsControlRight);
        }

        [Fact]
        public void IsControlJump_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlJump = true;

            Assert.True(packet.IsControlJump);
        }

        [Fact]
        public void IsControlUseItem_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsControlUseItem = true;

            Assert.True(packet.IsControlUseItem);
        }

        [Fact]
        public void IsSleeping_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsSleeping = true;

            Assert.True(packet.IsSleeping);
        }

        [Fact]
        public void IsPulleyEnabled_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsPulleyEnabled = true;

            Assert.True(packet.IsPulleyEnabled);
        }

        [Fact]
        public void IsDirectionRight_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsDirectionRight = true;

            Assert.True(packet.IsDirectionRight);
        }

        [Fact]
        public void ShouldUpdateVelocity_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.ShouldUpdateVelocity = true;

            Assert.True(packet.ShouldUpdateVelocity);
        }

        [Fact]
        public void IsVortexStealthActive_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsVortexStealthActive = true;

            Assert.True(packet.IsVortexStealthActive);
        }

        [Fact]
        public void IsGravityInverted_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsGravityInverted = true;

            Assert.True(packet.IsGravityInverted);
        }

        [Fact]
        public void IsShieldRaised_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsShieldRaised = true;

            Assert.True(packet.IsShieldRaised);
        }

        [Fact]
        public void IsHoveringUp_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsHoveringUp = true;

            Assert.True(packet.IsHoveringUp);
        }

        [Fact]
        public void IsHoveringDown_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsHoveringDown = true;

            Assert.True(packet.IsHoveringDown);
        }

        [Fact]
        public void IsVoidVaultEnabled_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsVoidVaultEnabled = true;

            Assert.True(packet.IsVoidVaultEnabled);
        }

        [Fact]
        public void IsSitting_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsSitting = true;

            Assert.True(packet.IsSitting);
        }

        [Fact]
        public void HasCompletedDD2Event_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.HasCompletedDD2Event = true;

            Assert.True(packet.HasCompletedDD2Event);
        }

        [Fact]
        public void IsPettingAnimal_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsPettingAnimal = true;

            Assert.True(packet.IsPettingAnimal);
        }

        [Fact]
        public void IsPettingSmallAnimal_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.IsPettingSmallAnimal = true;

            Assert.True(packet.IsPettingSmallAnimal);
        }

        [Fact]
        public void HasUsedPotionOfReturn_Set_Get()
        {
            var packet = new PlayerInfo();

            packet.HasUsedPotionOfReturn = true;

            Assert.True(packet.HasUsedPotionOfReturn);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerInfo>(_bytes, PacketContext.Server);

            Assert.Equal(130, packet.PlayerIndex);
            Assert.Equal(5, packet.SelectedItemSlot);
            Assert.Equal(123.456F, packet.PositionX);
            Assert.Equal(456.789F, packet.PositionY);
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
            Assert.False(packet.ShouldUpdateVelocity);
            Assert.False(packet.IsVortexStealthActive);
            Assert.True(packet.IsGravityInverted);
            Assert.False(packet.IsShieldRaised);
            Assert.False(packet.IsHoveringUp);
            Assert.False(packet.IsVoidVaultEnabled);
            Assert.True(packet.IsSitting);
            Assert.True(packet.HasCompletedDD2Event);
            Assert.True(packet.IsPettingAnimal);
            Assert.False(packet.IsPettingSmallAnimal);
            Assert.False(packet.HasUsedPotionOfReturn);
            Assert.False(packet.IsHoveringDown);
        }
    }
}
