namespace Orion.Tests.Networking.Packets {
    using System;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class FinishConnectionPacketTests {
        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => FinishConnectionPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
