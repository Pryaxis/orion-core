namespace Orion.Tests.Networking.Events {
    using System;
    using FluentAssertions;
    using Orion.Networking.Events;
    using Orion.Networking.Packets;
    using Xunit;

    public class SendingPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new ConnectionRequestPacket {Version = "test"};
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
            var args = new SendingPacketEventArgs(
                new Terraria.RemoteClient(),
                new ConnectionRequestPacket {Version = "test"});

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var args = new SendingPacketEventArgs(
                new Terraria.RemoteClient(),
                new ConnectionRequestPacket {Version = "test"});
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var args = new SendingPacketEventArgs(
                new Terraria.RemoteClient(),
                new ConnectionRequestPacket {Version = "test"});
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
