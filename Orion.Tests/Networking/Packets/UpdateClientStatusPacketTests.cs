using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateClientStatusPacketTests {
        [Fact]
        public void SetStatusText_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateClientStatusPacket();
            Action action = () => packet.StatusText = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
