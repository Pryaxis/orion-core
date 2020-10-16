using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Packets.Misc
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class CreditsRollTests
    {
        private readonly byte[] _bytes = { 5, 0, 140, 0, 0, 1, 0, 0 };

        [Fact]
        public void RemainingTime_Set_Get()
        {
            var packet = new CreditsRoll();

            packet.RemainingTime = 1;

            Assert.Equal(1, packet.RemainingTime);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<CreditsRoll>(_bytes, PacketContext.Server);

            Assert.Equal(256, packet.RemainingTime);
        }
    }
}
