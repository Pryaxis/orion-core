using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Items
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ItemTweakTests
    {
        private readonly byte[] _bytes =
        {
            42, 0, 88, 1, 0, 0, 0, 255, 0, 128, 255, 0, 2, 0, 0, 0, 0, 64, 3, 0, 4, 0, 5, 0, 0, 0, 0, 64, 255, 6, 0, 7, 0,
            0, 0, 0, 64, 8, 0, 9, 0, 1
        };

        [Fact]
        public void ItemIndex_Set_Get()
        {
            var packet = new ItemTweak();

            packet.ItemIndex = 1;

            Assert.Equal(1, packet.ItemIndex);
        }

        [Fact]
        public void Color_SetNullValue()
        {
            var packet = new ItemTweak();

            packet.Color = null;

            Assert.Null(packet.Color);
        }

        [Fact]
        public void Color_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Color = new Color3(128, 128, 128);

            Assert.Equal(new Color3(128, 128, 128), packet.Color);
        }

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Damage = 1;

            Assert.Equal(1, packet.Damage.Value);
        }

        [Fact]
        public void Knockback_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Knockback = 1;

            Assert.Equal(1, packet.Knockback.Value);
        }

        [Fact]
        public void AnimationTime_Set_Get()
        {
            var packet = new ItemTweak();

            packet.AnimationTime = 1;

            Assert.Equal(1, packet.AnimationTime.Value);
        }

        [Fact]
        public void UseTime_Set_Get()
        {
            var packet = new ItemTweak();

            packet.UseTime = 1;

            Assert.Equal(1, packet.UseTime.Value);
        }

        [Fact]
        public void ShootProjectileType_Set_Get()
        {
            var packet = new ItemTweak();

            packet.ShootProjectileType = 1;

            Assert.Equal(1, packet.ShootProjectileType.Value);
        }

        [Fact]
        public void ShootSpeed_Set_Get()
        {
            var packet = new ItemTweak();

            packet.ShootSpeed = 1;

            Assert.Equal(1, packet.ShootSpeed.Value);
        }

        [Fact]
        public void Width_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Width = 1;

            Assert.Equal(1, packet.Width.Value);
        }

        [Fact]
        public void Height_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Height = 1;

            Assert.Equal(1, packet.Height.Value);
        }

        [Fact]
        public void Scale_Set_Get()
        {
            var packet = new ItemTweak();

            packet.Scale = 1;

            Assert.Equal(1, packet.Scale.Value);
        }

        [Fact]
        public void AmmoType_Set_Get()
        {
            var packet = new ItemTweak();

            packet.AmmoType = 1;

            Assert.Equal(1, packet.AmmoType.Value);
        }

        [Fact]
        public void AmmoRequiredToUseItem_Set_Get()
        {
            var packet = new ItemTweak();

            packet.AmmoRequiredToUseItem = 1;

            Assert.Equal(1, packet.AmmoRequiredToUseItem.Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void NotRealAmmo_Set_Get(bool value)
        {
            var packet = new ItemTweak();

            packet.NotRealAmmo = value;

            Assert.Equal(value, packet.NotRealAmmo);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ItemTweak>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.ItemIndex);
            Assert.Equal(new Color3(255, 128, 0), packet.Color!.Value);
            Assert.Equal(2, packet.Damage!.Value);
            Assert.Equal(2F, packet.Knockback!.Value);
            Assert.Equal(3, packet.AnimationTime!.Value);
            Assert.Equal(4, packet.UseTime!.Value);
            Assert.Equal(5, packet.ShootProjectileType!.Value);
            Assert.Equal(2F, packet.ShootSpeed!.Value);
            Assert.Equal(6, packet.Width!.Value);
            Assert.Equal(7, packet.Height!.Value);
            Assert.Equal(2F, packet.Scale!.Value);
            Assert.Equal(8, packet.AmmoType!.Value);
            Assert.Equal(9, packet.AmmoRequiredToUseItem!.Value);
            Assert.True(packet.NotRealAmmo!.Value);
        }
    }
}
