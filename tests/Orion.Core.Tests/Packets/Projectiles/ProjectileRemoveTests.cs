using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Projectiles
{
    public sealed class ProjectileRemoveTests
    {
        private readonly byte[] _bytes = { 6, 0, 29, 25, 0, 250 };

        [Fact]
        public void ProjectileId_Set_Get()
        {
            var packet = new ProjectileRemove();

            packet.ProjectileId = 50;

            Assert.Equal(50, packet.ProjectileId);
        }

        [Fact]
        public void OwnerId_Set_Get()
        {
            var packet = new ProjectileRemove();

            packet.OwnerId = 5;

            Assert.Equal(5, packet.OwnerId);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ProjectileRemove>(_bytes, PacketContext.Server);

            Assert.Equal(25, packet.ProjectileId);
            Assert.Equal(250, packet.OwnerId);
        }
    }
}
