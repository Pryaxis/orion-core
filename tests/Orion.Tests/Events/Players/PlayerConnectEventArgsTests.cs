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
using Orion.Entities;
using Orion.Networking.Packets.Players;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerConnectEventArgsTests {
        [Fact]
        public void Ctor_NullPacket_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Func<PlayerConnectEventArgs> func = () => new PlayerConnectEventArgs(player, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPlayerVersionString_IsCorrect() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerConnectPacket {PlayerVersionString = "test"};
            var args = new PlayerConnectEventArgs(player, packet);

            args.PlayerVersionString.Should().Be("test");
        }

        [Fact]
        public void SetPlayerVersionString_NullValue_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerConnectPacket();
            var args = new PlayerConnectEventArgs(player, packet);
            Action action = () => args.PlayerVersionString = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
