using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcRevengeInfoTests
    {
        private readonly byte[] _bytes = 
        { 
            40, 0, 126, 1, 0, 0, 0, 0, 0, 0, 64, 0, 0, 64, 64, 4, 0, 0, 0, 0, 0, 160, 64, 6, 0, 0, 0, 7, 0, 0, 0, 8, 0, 0, 0,
            0, 0, 16, 65, 1 
        };

        [Fact]
        public void UniqueId_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.UniqueId = 1;

            Assert.Equal(1, packet.UniqueId);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.Position = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.Position);
        }

        [Fact]
        public void NetId_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.NetId = 1;

            Assert.Equal(1, packet.NetId);
        }

        [Fact]
        public void HealthPercentage_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.HealthPercentage = 100;

            Assert.Equal(100, packet.HealthPercentage);
        }

        [Fact]
        public void NpcType_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.NpcType = 1;

            Assert.Equal(1, packet.NpcType);
        }

        [Fact]
        public void AiStyle_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.AiStyle = 1;

            Assert.Equal(1, packet.AiStyle);
        }

        [Fact]
        public void CoinStealValue_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.CoinStealValue = 1;

            Assert.Equal(1, packet.CoinStealValue);
        }

        [Fact]
        public void BaseValue_Set_Get()
        {
            var packet = new NpcRevengeInfo();

            packet.BaseValue = 1;

            Assert.Equal(1, packet.BaseValue);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SpawnedFromStatue_Set_Get(bool value)
        {
            var packet = new NpcRevengeInfo();

            packet.SpawnedFromStatue = value;

            Assert.Equal(value, packet.SpawnedFromStatue);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcRevengeInfo>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.UniqueId);
            Assert.Equal(new Vector2f(2, 3), packet.Position);
            Assert.Equal(4, packet.NetId);
            Assert.Equal(5, packet.HealthPercentage);
            Assert.Equal(6, packet.NpcType);
            Assert.Equal(7, packet.AiStyle);
            Assert.Equal(8, packet.CoinStealValue);
            Assert.Equal(9, packet.BaseValue);
        }
    }
}
