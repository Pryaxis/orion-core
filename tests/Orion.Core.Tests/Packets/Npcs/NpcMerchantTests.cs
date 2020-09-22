using ReLogic.Content;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    public sealed class NpcMerchantTests
    {
        private readonly byte[] _bytes =
        {
            83, 0, 72, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 8, 0, 9, 0, 10, 0, 11, 0, 12, 0, 13, 0, 14, 0, 15,
            0, 16, 0, 17, 0, 18, 0, 19, 0, 20, 0, 21, 0, 22, 0, 23, 0, 24, 0, 25, 0, 26, 0, 27, 0, 28, 0, 29, 0, 30,
            0, 31, 0, 32, 0, 33, 0, 34, 0, 35, 0, 36, 0, 37, 0, 38, 0, 39, 0, 40, 0
        };

        [Fact]
        public void ShopItems_Set_Get()
        {
            var packet = new NpcMerchant();

            packet.ShopItems[0] = 0;
            packet.ShopItems[1] = 1;
            
            Assert.Equal(0, packet.ShopItems[0]);
            Assert.Equal(1, packet.ShopItems[1]);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcMerchant>(_bytes, PacketContext.Server);

            for (int i = 0; i < packet.ShopItems.Length; i++)
            {
                Assert.Equal(i + 1, packet.ShopItems[i]);
            }
        }
    }
}
