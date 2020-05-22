// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Moq;
using Orion.Packets.Players;
using Orion.Players;
using Xunit;

namespace Orion.Events.Players {
    public class PlayerPvpEventTests {
        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var packet = new PlayerPvpPacket();

            Assert.Throws<ArgumentNullException>(() => new PlayerPvpEvent(null!, ref packet));
        }

        [Fact]
        public void IsInPvp_Get() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerPvpPacket { IsInPvp = true };
            var evt = new PlayerPvpEvent(player, ref packet);

            Assert.True(evt.IsInPvp);
        }

        [Fact]
        public void IsInPvp_Set() {
            var player = new Mock<IPlayer>().Object;
            var packet = new PlayerPvpPacket();
            var evt = new PlayerPvpEvent(player, ref packet);

            evt.IsInPvp = true;

            Assert.True(packet.IsInPvp);
        }
    }
}
