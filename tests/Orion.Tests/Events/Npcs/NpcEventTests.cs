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
using Orion.Npcs;
using Xunit;

namespace Orion.Events.Npcs {
    public class NpcEventTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcEvent> func = () => new TestNpcEvent(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Npc_Get() {
            var npc = new Mock<INpc>().Object;
            var args = new TestNpcEvent(npc);

            args.Npc.Should().BeSameAs(npc);
        }

        private class TestNpcEvent : NpcEvent {
            public TestNpcEvent(INpc npc) : base(npc) { }
        }
    }
}
