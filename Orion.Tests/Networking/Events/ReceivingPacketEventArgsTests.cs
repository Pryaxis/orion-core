using System;
using FluentAssertions;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class ReceivingPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new RequestConnectionPacket {Version = "test"};
            Func<ReceivingPacketEventArgs> func = () => new ReceivingPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var sender = new Terraria.RemoteClient();
            Func<ReceivingPacketEventArgs> func = () => new ReceivingPacketEventArgs(sender, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_StartsOffNotDirty() {
            var args = new ReceivingPacketEventArgs(
                new Terraria.RemoteClient(),
                new RequestConnectionPacket {Version = "test"});

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var args = new ReceivingPacketEventArgs(
                new Terraria.RemoteClient(),
                new RequestConnectionPacket {Version = "test"});
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var args = new ReceivingPacketEventArgs(
                new Terraria.RemoteClient(),
                new RequestConnectionPacket {Version = "test"});
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
