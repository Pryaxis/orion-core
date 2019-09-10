using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Orion.Networking.Events;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class ReceivedPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new StartConnectingPacket {Version = "test"};
            Func<ReceivedPacketEventArgs> func = () => new ReceivedPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Mock<IClient>().Object;
            Func<ReceivedPacketEventArgs> func = () => new ReceivedPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetSender_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivedPacketEventArgs(sender, packet);

            args.Sender.Should().BeSameAs(sender);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivedPacketEventArgs(sender, packet);

            args.Packet.Should().BeSameAs(packet);
        }
    }
}
