using System;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateNpcNameTests {
        [Fact]
        public void SetNpcName_NullValue_ThrowsArgumentNullException() {
            var packet = new UpdateNpcNamePacket();
            Action action = () => packet.NpcName = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
