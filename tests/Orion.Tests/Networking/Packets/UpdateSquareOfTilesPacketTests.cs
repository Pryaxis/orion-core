using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateSquareOfTilesPacketTests {
        [Fact]
        public void SetTiles_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateSquareOfTilesPacket();
            Action action = () => packet.Tiles = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
