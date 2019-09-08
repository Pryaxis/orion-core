using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateSignPacketTests {
        [Fact]
        public void SetSignText_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateSignPacket();
            Action action = () => packet.SignText = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
