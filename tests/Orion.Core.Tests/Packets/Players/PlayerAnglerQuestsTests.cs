using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class PlayerAnglerQuestsTests
    {
        private readonly byte[] _bytes = { 12, 0, 76, 1, 2, 0, 0, 0, 3, 0, 0, 0 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerAnglerQuests();

            packet.PlayerIndex = 1;

            Assert.Equal(1, packet.PlayerIndex);
        }

        [Fact]
        public void AnglerQuestsFinished_Set_Get()
        {
            var packet = new PlayerAnglerQuests();

            packet.AnglerQuestsFinished = 1;

            Assert.Equal(1, packet.AnglerQuestsFinished);
        }

        [Fact]
        public void GolferScoreAccumulated_Set_Get()
        {
            var packet = new PlayerAnglerQuests();

            packet.GolferScoreAccumulated = 1;

            Assert.Equal(1, packet.GolferScoreAccumulated);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerAnglerQuests>(_bytes, PacketContext.Server);

            Assert.Equal(1, packet.PlayerIndex);
            Assert.Equal(2, packet.AnglerQuestsFinished);
            Assert.Equal(3, packet.GolferScoreAccumulated);
        }
    }
}
