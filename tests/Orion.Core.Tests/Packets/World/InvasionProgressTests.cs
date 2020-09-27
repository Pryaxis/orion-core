using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class InvasionProgressTests
    {
        private readonly byte[] _bytes = { 13, 0, 78, 5, 0, 0, 0, 10, 0, 0, 0, 0, 5 };

        [Fact]
        public void CurrentProgress_Set_Get()
        {
            var packet = new InvasionProgress();

            packet.CurrentProgress = 1;

            Assert.Equal(1, packet.CurrentProgress);
        }

        [Fact]
        public void MaxProgress_Set_Get()
        {
            var packet = new InvasionProgress();

            packet.MaxProgress = 1;

            Assert.Equal(1, packet.MaxProgress);
        }

        [Fact]
        public void Icon_Set_Get()
        {
            var packet = new InvasionProgress();

            packet.Icon = 1;

            Assert.Equal(1, packet.Icon);
        }

        [Fact]
        public void CurrentWave_Set_Get()
        {
            var packet = new InvasionProgress();

            packet.CurrentWave = 1;

            Assert.Equal(1, packet.CurrentWave);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<InvasionProgress>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.CurrentProgress);
            Assert.Equal(10, packet.MaxProgress);
            Assert.Equal(0, packet.Icon);
            Assert.Equal(5, packet.CurrentWave);
        }
    }
}
