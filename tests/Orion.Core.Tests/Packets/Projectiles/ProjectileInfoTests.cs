using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Projectiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ProjectileInfoTests
    {
        private readonly byte[] _bytes =
        {
            39, 0, 27, 1, 0, 0, 0, 0, 64, 0, 0, 64, 64, 0, 0, 128, 64, 0, 0, 160, 64, 6, 7, 0, 241, 0, 0, 128, 63, 8, 0, 0, 0, 128, 63, 9, 0, 10, 0
        };


        [Fact]
        public void Identity_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Identity = 5;

            Assert.Equal(5, packet.Identity);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Position = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void Velocity_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Velocity = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Velocity);
        }

        [Fact]
        public void OwnerIndex_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.OwnerIndex = 1;

            Assert.Equal(1, packet.OwnerIndex);
        }

        [Fact]
        public void Type_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Type = 1;

            Assert.Equal(1, packet.Type);
        }

        [Fact]
        public void AdditionalInformation_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.AdditionalInformation[0] = 1;
            packet.AdditionalInformation[1] = 2;

            Assert.Equal(1, packet.AdditionalInformation[0]);
            Assert.Equal(2, packet.AdditionalInformation[1]);
        }

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Damage = 1;

            Assert.Equal(1, packet.Damage);
        }

        [Fact]
        public void Knockback_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Knockback = 1;

            Assert.Equal(1, packet.Knockback);
        }

        [Fact]
        public void OriginalDamage_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.OriginalDamage = 1;

            Assert.Equal(1, packet.OriginalDamage);
        }

        [Fact]
        public void Uuid_Set_Get()
        {
            var packet = new ProjectileInfo();

            packet.Uuid = 1;

            Assert.Equal(1, packet.Uuid);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ProjectileInfo>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.Identity);
            Assert.Equal(new Vector2f(2, 3), packet.Position);
            Assert.Equal(new Vector2f(4, 5), packet.Velocity);
            Assert.Equal(6, packet.OwnerIndex);
            Assert.Equal(7, packet.Type);
            Assert.Equal(1F, packet.AdditionalInformation[0]);
            Assert.Equal(8, packet.Damage);
            Assert.Equal(1F, packet.Knockback);
            Assert.Equal(9, packet.OriginalDamage);
            Assert.Equal(10, packet.Uuid);
        }
    }
}
