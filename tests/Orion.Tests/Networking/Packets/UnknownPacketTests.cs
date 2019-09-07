using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UnknownPacketTests {
        [Fact]
        public void SetPayload_NullValue_ThrowsArgumentNullException() {
            var packet = new UnknownPacket((TerrariaPacketType)255);
            Action action = () => packet.Payload = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
