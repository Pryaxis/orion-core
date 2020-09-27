using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.World
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PillarShieldStrengthsTests
    {
        private readonly byte[] _bytes = { 11, 0, 101, 1, 0, 2, 0, 3, 0, 4, 0 };

        [Fact]
        public void SolarShieldStrength_Set_Get()
        {
            var packet = new PillarShieldStrengths();

            packet.SolarShieldStrength = 1;

            Assert.Equal(1, packet.SolarShieldStrength);
        }

        [Fact]
        public void VortexShieldStrength_Set_Get()
        {
            var packet = new PillarShieldStrengths();

            packet.VortexShieldStrength = 1;

            Assert.Equal(1, packet.VortexShieldStrength);
        }

        [Fact]
        public void NebulaShieldStrength_Set_Get()
        {
            var packet = new PillarShieldStrengths();

            packet.NebulaShieldStrength = 1;

            Assert.Equal(1, packet.NebulaShieldStrength);
        }

        [Fact]
        public void StardustShieldStrength_Set_Get()
        {
            var packet = new PillarShieldStrengths();

            packet.StardustShieldStrength = 1;

            Assert.Equal(1, packet.StardustShieldStrength);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PillarShieldStrengths>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.SolarShieldStrength);
            Assert.Equal(2, packet.VortexShieldStrength);
            Assert.Equal(3, packet.NebulaShieldStrength);
            Assert.Equal(4, packet.StardustShieldStrength);
        }
    }
}
