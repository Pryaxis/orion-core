using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Orion.Networking.Events;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class SentPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new StartConnectingPacket {Version = "test"};
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var receiver = new Mock<IClient>().Object;
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(receiver, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetReceiver_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SentPacketEventArgs(receiver, packet);

            args.Receiver.Should().BeSameAs(receiver);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SentPacketEventArgs(receiver, packet);

            args.Packet.Should().BeSameAs(packet);
        }
    }
}
