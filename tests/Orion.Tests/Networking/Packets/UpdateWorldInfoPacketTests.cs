using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.World;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateWorldInfoPacketTests {
        [Fact]
        public void SetWorldName_NullValue_ThrowsArgumentNullException() {
            var packet = new WorldInfoPacket();
            Action action = () => packet.WorldName = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
