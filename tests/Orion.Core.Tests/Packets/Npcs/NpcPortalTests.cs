using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcPortalTests
    {
        private readonly byte[] _bytes = { 23, 0, 100, 1, 0, 2, 0, 0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 0, 64, 0, 0, 0, 64 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcPortal();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Fact]
        public void PortalColorIndex_Set_Get()
        {
            var packet = new NpcPortal();

            packet.PortalColorIndex = 1;

            Assert.Equal(1, packet.PortalColorIndex);
        }

        [Fact]
        public void NewPosition_Set_Get()
        {
            var packet = new NpcPortal();

            packet.NewPosition = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.NewPosition);
        }

        [Fact]
        public void Velocity_Set_Get()
        {
            var packet = new NpcPortal();

            packet.Velocity = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Velocity);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcPortal>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.NpcIndex);
            Assert.Equal(2, packet.PortalColorIndex);
            Assert.Equal(new Vector2f(2F, 2F), packet.NewPosition);
            Assert.Equal(new Vector2f(2F, 2F), packet.Velocity);
        }
    }
}
