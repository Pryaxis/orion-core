namespace Orion.Tests.Networking.Events {
    using System;
    using FluentAssertions;
    using Orion.Networking.Events;
    using Orion.Networking.Packets;
    using Xunit;

    public class SentPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new ConnectionRequestPacket {Version = "test"};
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Terraria.RemoteClient();
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
