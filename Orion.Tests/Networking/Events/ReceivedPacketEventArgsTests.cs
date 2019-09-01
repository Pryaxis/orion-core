namespace Orion.Tests.Networking.Events {
    using System;
    using FluentAssertions;
    using Orion.Networking.Events;
    using Orion.Networking.Packets;
    using Xunit;

    public class ReceivedPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new RequestConnectionPacket {Version = "test"};
            Func<ReceivedPacketEventArgs> func = () => new ReceivedPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Terraria.RemoteClient();
            Func<ReceivedPacketEventArgs> func = () => new ReceivedPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
