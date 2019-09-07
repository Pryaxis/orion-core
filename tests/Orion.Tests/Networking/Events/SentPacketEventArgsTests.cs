using System;
using FluentAssertions;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class SentPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new RequestConnectionPacket {Version = "test"};
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Terraria.RemoteClient();
            Func<SentPacketEventArgs> func = () => new SentPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetReceiver_IsCorrect() {
            var receiver = new Terraria.RemoteClient();
            var packet = new RequestConnectionPacket {Version = "test"};
            var args = new SentPacketEventArgs(receiver, packet);

            args.Receiver.Should().BeSameAs(receiver);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var sender = new Terraria.RemoteClient();
            var packet = new RequestConnectionPacket {Version = "test"};
            var args = new SentPacketEventArgs(sender, packet);

            args.Packet.Should().BeSameAs(packet);
        }
    }
}
