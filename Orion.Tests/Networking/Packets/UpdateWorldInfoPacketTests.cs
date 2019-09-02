using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateWorldInfoPacketTests {
        [Fact]
        public void SetWorldName_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateWorldInfoPacket();
            Action action = () => packet.WorldName = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
