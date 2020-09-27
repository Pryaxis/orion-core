using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcStealCoinsTests
    {
        private readonly byte[] _bytes = { 19, 0, 92, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 64, 0, 0, 0, 64 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcStealCoins();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Fact]
        public void Value_Set_Get()
        {
            var packet = new NpcStealCoins();

            packet.Value = 1;

            Assert.Equal(1, packet.Value);
        }

        [Fact]
        public void MoneyPosition_Set_Get()
        {
            var packet = new NpcStealCoins();

            packet.MoneyPosition = new Vector2f(1.23F, 4.56F);

            Assert.Equal(new Vector2f(1.23F, 4.56F), packet.MoneyPosition);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcStealCoins>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.NpcIndex);
            Assert.Equal(2, packet.Value);
            Assert.Equal(new Vector2f(2, 2), packet.MoneyPosition);
        }
    }
}
