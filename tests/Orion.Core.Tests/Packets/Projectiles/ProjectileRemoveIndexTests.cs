using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Projectiles
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class ProjectileRemoveIndexTests
    {
        private readonly byte[] _bytes = { 6, 0, 95, 1, 0, 2 };

        [Fact]
        public void OwnerIndex_Set_Get()
        {
            var packet = new ProjectileRemoveIndex();

            packet.OwnerIndex = 1;

            Assert.Equal(1, packet.OwnerIndex);
        }

        [Fact]
        public void PortalForm_Set_Get()
        {
            var packet = new ProjectileRemoveIndex();

            packet.PortalForm = 1;

            Assert.Equal(1, packet.PortalForm);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ProjectileRemoveIndex>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.OwnerIndex);
            Assert.Equal(2, packet.PortalForm);
        }
    }
}
