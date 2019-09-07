using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateWorldSectionPacketTests {
        [Fact]
        public void SetWorldSection_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateWorldSectionPacket();
            Action action = () => packet.WorldSection = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
