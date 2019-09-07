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
            var packet = new RequestConnectionPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(new Terraria.RemoteClient(), packet);

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var packet = new RequestConnectionPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(new Terraria.RemoteClient(), packet);
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var packet = new RequestConnectionPacket {Version = "test"};
            var args = new ReceivingPacketEventArgs(new Terraria.RemoteClient(), packet);
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
