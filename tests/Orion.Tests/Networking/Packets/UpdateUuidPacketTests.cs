using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateUuidPacketTests {
        [Fact]
        public void SetUuid_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateUuidPacket();
            Action action = () => packet.Uuid = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
