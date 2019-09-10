using System;
using FluentAssertions;
using Orion.Networking.Packets.Modules;
using Xunit;

namespace Orion.Tests.Networking.Packets.Modules {
    public class ModulePacketTests {
        [Fact]
        public void SetModule_NullValue_ThrowsArgumentNullException() {
            var packet = new ModulePacket();
            Action action = () => packet.Module = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
