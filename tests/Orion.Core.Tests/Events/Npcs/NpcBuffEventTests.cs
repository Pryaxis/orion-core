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
using Orion.Core.Buffs;
using Orion.Core.Npcs;
using Orion.Core.Players;
using Xunit;

namespace Orion.Core.Events.Npcs {
    public class NpcBuffEventTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            var player = Mock.Of<IPlayer>();
            var buff = new Buff(BuffId.Poisoned, TimeSpan.FromSeconds(1));

            Assert.Throws<ArgumentNullException>(() => new NpcBuffEvent(null!, player, buff));
        }

        [Fact]
        public void Ctor_NullPlayer_ThrowsArgumentNullException() {
            var npc = Mock.Of<INpc>();
            var buff = new Buff(BuffId.Poisoned, TimeSpan.FromSeconds(1));

            Assert.Throws<ArgumentNullException>(() => new NpcBuffEvent(npc, null!, buff));
        }

        [Fact]
        public void Player_Get() {
            var npc = Mock.Of<INpc>();
            var player = Mock.Of<IPlayer>();
            var buff = new Buff(BuffId.Poisoned, TimeSpan.FromSeconds(1));
            var evt = new NpcBuffEvent(npc, player, buff);

            Assert.Same(player, evt.Player);
        }

        [Fact]
        public void Buff_Get() {
            var npc = Mock.Of<INpc>();
            var player = Mock.Of<IPlayer>();
            var buff = new Buff(BuffId.Poisoned, TimeSpan.FromSeconds(1));
            var evt = new NpcBuffEvent(npc, player, buff);

            Assert.Equal(buff, evt.Buff);
        }
    }
}
