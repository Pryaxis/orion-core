using System;
using System.Collections.Generic;
using System.Text;
using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.Projectiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ProjectileCannonTests
    {
        private readonly byte[] _bytes = { 18, 0, 108, 1, 0, 0, 0, 0, 64, 3, 0, 4, 0, 5, 0, 6, 0, 7 };

        [Fact]
        public void Damage_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.Damage = 1;

            Assert.Equal(1, packet.Damage);
        }

        [Fact]
        public void Knockback_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.Knockback = 1;

            Assert.Equal(1, packet.Knockback);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void Angle_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.Angle = 1;

            Assert.Equal(1, packet.Angle);
        }

        [Fact]
        public void Ammo_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.Ammo = 1;

            Assert.Equal(1, packet.Ammo);
        }

        [Fact]
        public void OwnerIndex_Set_Get()
        {
            var packet = new ProjectileCannon();

            packet.OwnerIndex = 1;

            Assert.Equal(1, packet.OwnerIndex);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ProjectileCannon>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.Damage);
            Assert.Equal(2F, packet.Knockback);
            Assert.Equal(3, packet.X);
            Assert.Equal(4, packet.Y);
            Assert.Equal(5, packet.Angle);
            Assert.Equal(6, packet.Ammo);
            Assert.Equal(7, packet.OwnerIndex);
        }
    }
}
