using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class TerrariaPacketTests {
        [Fact]
        public void FromStream_NullStream_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => TerrariaPacket.FromStream(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
