using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Orion.Networking.Events;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class ReceivingPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new StartConnectingPacket {Version = "test"};
            Func<ReceivingPacketEventArgs> func = () => new ReceivingPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Mock<IClient>().Object;
            Func<ReceivingPacketEventArgs> func = () => new ReceivingPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_StartsOffNotDirty() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void GetSender_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);

            args.Sender.Should().BeSameAs(sender);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);

            args.Packet.Should().BeSameAs(packet);
        }

        [Fact]
        public void SetPacket_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);
            var packet2 = new StartConnectingPacket {Version = "test2"};

            args.Packet = packet2;

            args.IsPacketDirty.Should().BeTrue();
            args.Packet.Should().BeSameAs(packet2);
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var sender = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(sender, packet);
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
