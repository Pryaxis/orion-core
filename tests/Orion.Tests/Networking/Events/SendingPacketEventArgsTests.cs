// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Orion.Networking.Events;
using Orion.Networking.Packets.Connections;
using Xunit;

namespace Orion.Tests.Networking.Events {
    public class SendingPacketEventArgsTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new StartConnectingPacket {Version = "test"};
            Func<SendingPacketEventArgs> func = () => new SendingPacketEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var receiver = new Mock<IClient>().Object;
            Func<SendingPacketEventArgs> func = () => new SendingPacketEventArgs(receiver, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_StartsOffNotDirty() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);

            args.IsPacketDirty.Should().BeFalse();
        }

        [Fact]
        public void GetReceiver_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);

            args.Receiver.Should().BeSameAs(receiver);
        }

        [Fact]
        public void GetPacket_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);

            args.Packet.Should().BeSameAs(packet);
        }

        [Fact]
        public void SetPacket_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);
            var packet2 = new StartConnectingPacket {Version = "test2"};

            args.Packet = packet2;

            args.IsPacketDirty.Should().BeTrue();
            args.Packet.Should().BeSameAs(packet2);
        }

        [Fact]
        public void SetPacket_NullValue_ThrowsArgumentNullException() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);
            Action action = () => args.Packet = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MarkPacketAsDirty_IsCorrect() {
            var receiver = new Mock<IClient>().Object;
            var packet = new StartConnectingPacket {Version = "test"};
            var args = new SendingPacketEventArgs(receiver, packet);
            args.MarkPacketAsDirty();

            args.IsPacketDirty.Should().BeTrue();
        }
    }
}
