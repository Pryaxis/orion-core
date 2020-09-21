using Xunit;

namespace Orion.Core.Packets.Npcs
{
    public sealed class NpcCavernMonstersTests
    {
        private readonly byte[] _bytes = {9, 0, 136, 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0};

        [Fact]
        public void CavernMonsterTypes_Set_Get()
        {
            var packet = new NpcCavernMonsters();

            packet.CavernMonsterTypes[0, 0] = 1;
            packet.CavernMonsterTypes[1, 0] = 2;
            packet.CavernMonsterTypes[1, 1] = 3;
            
            Assert.Equal(1, packet.CavernMonsterTypes[0, 0]);
            Assert.Equal(2, packet.CavernMonsterTypes[1, 0]);
            Assert.Equal(3, packet.CavernMonsterTypes[1, 1]);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcCavernMonsters>(_bytes, PacketContext.Server);

            var index = 0;
            for (int i = 0; i < packet.CavernMonsterTypes.GetUpperBound(0); i++)
            {
                for (int j = 0; j < packet.CavernMonsterTypes.GetUpperBound(1); j++)
                {
                    Assert.Equal(index++, packet.CavernMonsterTypes[i, j]);
                }
            }
        }
    }
}
