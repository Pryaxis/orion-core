using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Orion.Core.Packets;
using Orion.Core.Packets.World;
using Xunit;

namespace Orion.Core.Packets.World
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class WorldTimeTests
    {
        private readonly byte[] _bytes = { 11, 0, 18, 1, 180, 0, 0, 0, 25, 0, 0, 0 };

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsDayTime_Set_Get(bool value)
        {
            var packet = new WorldTime();

            packet.IsDayTime = value;

            Assert.Equal(value, packet.IsDayTime);
        }

        [Fact]
        public void Time_Set_Get()
        {
            var packet = new WorldTime();

            packet.Time = 4860;

            Assert.Equal(4860, packet.Time);
        }

        [Fact]
        public void SunOffsetY_Set_Get()
        {
            var packet = new WorldTime();

            packet.SunOffsetY = 25;
            
            Assert.Equal(25, packet.SunOffsetY);
        }

        [Fact]
        public void MoonOffsetY_Set_Get()
        {
            var packet = new WorldTime();

            packet.MoonOffsetY = 25;

            Assert.Equal(25, packet.MoonOffsetY);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<WorldTime>(_bytes, PacketContext.Server);

            Assert.True(packet.IsDayTime);
            Assert.Equal(180, packet.Time);
            Assert.Equal(25, packet.SunOffsetY);
            Assert.Equal(0, packet.MoonOffsetY);
        }
    }
}
