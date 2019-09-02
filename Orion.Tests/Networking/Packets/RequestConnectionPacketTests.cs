using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class RequestConnectionPacketTests {
        [Fact]
        public void SetVersion_Null_ThrowsArgumentNullException() {
            var packet = new RequestConnectionPacket();
            Action action = () => packet.Version = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
