using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class AmbientEffectTests
    {
        private readonly byte[] _bytes = { 15, 0, 112, 1, 2, 0, 0, 0, 3, 0, 0, 0, 4, 5, 0 };

        [Theory]
        [InlineData(AmbientEffectType.TreeGrow)]
        [InlineData(AmbientEffectType.FairyParticles)]
        public void EffectType_Set_Get(AmbientEffectType value)
        {
            var packet = new AmbientEffect();

            packet.EffectType = value;

            Assert.Equal(value, packet.EffectType);
        }

        [Fact]
        public void X_Set_Get()
        {
            var packet = new AmbientEffect();

            packet.X = 1;

            Assert.Equal(1, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new AmbientEffect();

            packet.Y = 1;

            Assert.Equal(1, packet.Y);
        }

        [Fact]
        public void TreeHeightOrParticleType_Set_Get()
        {
            var packet = new AmbientEffect();

            packet.TreeHeightOrParticleType = 1;

            Assert.Equal(1, packet.TreeHeightOrParticleType);
        }

        [Fact]
        public void TreeGore_Set_Get()
        {
            var packet = new AmbientEffect();

            packet.TreeGore = 1;

            Assert.Equal(1, packet.TreeGore);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<AmbientEffect>(_bytes, PacketContext.Server);

            Assert.Equal(AmbientEffectType.TreeGrow, packet.EffectType);
            Assert.Equal(2, packet.X);
            Assert.Equal(3, packet.Y);
            Assert.Equal(4, packet.TreeHeightOrParticleType);
            Assert.Equal(5, packet.TreeGore);
        }
    }
}
