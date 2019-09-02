using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class DisconnectPlayerPacketTests {
        [Fact]
        public void SetReason_NullValue_ThrowsArgumentNullException() {
            var packet = new DisconnectPlayerPacket();
            Action action = () => packet.Reason = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
