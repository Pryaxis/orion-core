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
using Orion.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Events.Npcs {
    public class NpcCatchEventTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;

            Assert.Throws<ArgumentNullException>(() => new NpcCatchEvent(null!, player));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var npc = new Mock<INpc>().Object;

            Assert.Throws<ArgumentNullException>(() => new NpcCatchEvent(npc, null!));
        }

        [Fact]
        public void Player_Get() {
            var npc = new Mock<INpc>().Object;
            var player = new Mock<IPlayer>().Object;
            var evt = new NpcCatchEvent(npc, player);

            Assert.Same(player, evt.Player);
        }
    }
}
