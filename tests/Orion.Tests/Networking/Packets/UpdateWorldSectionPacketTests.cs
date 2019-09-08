using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateWorldSectionPacketTests {
        [Fact]
        public void SetTiles_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateWorldSectionPacket();
            Action action = () => packet.Tiles = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
