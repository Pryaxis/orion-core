// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Packets;
using Orion.Players;
using Xunit;

namespace Orion.Events.Packets {
    public class PacketReceiveEventArgsTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new Mock<Packet>().Object;
            Func<PacketReceiveEventArgs> func = () => new PacketReceiveEventArgs(null, packet);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Player_Get() {
            var sender = new Mock<IPlayer>().Object;
            var packet = new Mock<Packet>().Object;
            var args = new PacketReceiveEventArgs(sender, packet);

            args.Sender.Should().BeSameAs(sender);
        }
    }
}
