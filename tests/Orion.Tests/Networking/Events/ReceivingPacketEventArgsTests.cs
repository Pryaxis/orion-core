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
    }
}
