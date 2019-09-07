using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdatePlayerInfoPacketTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdatePlayerInfoPacket();
            Action action = () => packet.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
