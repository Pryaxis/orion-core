using System;
using FluentAssertions;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class SendingPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new ConnectPacket {Version = "test"};
            Func<SendingPacketEventArgs> func = () => new SendingPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Terraria.RemoteClient();
            Func<SendingPacketEventArgs> func = () => new SendingPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_StartsOffNotDirty() {
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(new Terraria.RemoteClient(), packet);

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void GetReceiver_IsCorrect() {
            var receiver = new Terraria.RemoteClient();
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);

            args.Receiver.Should().BeSameAs(receiver);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var sender = new Terraria.RemoteClient();
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(sender, packet);

            args.Packet.Should().BeSameAs(packet);
        }

        [Fact]
        public void SetPacket_IsCorrect() {
            var receiver = new Terraria.RemoteClient();
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);
            var packet2 = new ConnectPacket {Version = "test2"};

            args.Packet = packet2;

            args.IsPacketDirty.Should().BeTrue();
            args.Packet.Should().BeSameAs(packet2);
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(new Terraria.RemoteClient(), packet);
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var packet = new ConnectPacket {Version = "test"};
            var args = new SendingPacketEventArgs(new Terraria.RemoteClient(), packet);
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
