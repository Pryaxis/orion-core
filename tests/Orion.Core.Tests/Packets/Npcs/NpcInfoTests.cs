using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    public sealed class NpcInfoTests
    {
        private readonly byte[] _bytes = 
        { 
            42, 0, 23, 1, 0, 0, 0, 0, 0, 0, 64, 0, 0, 64, 64, 0, 0, 128, 64, 0, 0, 160, 64, 7, 0, 75, 7, 0, 0, 128, 63, 8, 0, 9,
            0, 0, 32, 65, 2, 0, 4, 5
        };

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcInfo>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.NpcIndex);
            Assert.Equal(2, packet.X);
            Assert.Equal(3, packet.Y);
            Assert.Equal(4, packet.VelocityX);
            Assert.Equal(5, packet.VelocityY);
            Assert.Equal(7, packet.TargetIndex);
            Assert.Equal(1F, packet.AdditionalInformation[1]);
            Assert.Equal(8, packet.NetId);
            Assert.Equal(9, packet.DifficultyScalingOverride);
            Assert.Equal(10F, packet.StrengthMultiplierOverride);
            Assert.Equal(1024, packet.Health);
            Assert.Equal(5, packet.ReleaseOwnerIndex);
        }
    }
}
