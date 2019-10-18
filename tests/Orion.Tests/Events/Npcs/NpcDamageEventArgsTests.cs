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

using FluentAssertions;
using Moq;
using Orion.Npcs;
using Xunit;

namespace Orion.Events.Npcs {
    public class NpcDamageEventArgsTests {
        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDamageEventArgs(npc, 0, 0, false, false);

            args.SetSimplePropertiesShouldMarkAsDirty();
        }

        [Fact]
        public void Damage_Get() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDamageEventArgs(npc, 167, 0, false, false);

            args.Damage.Should().Be(167);
        }

        [Fact]
        public void Knockback_Get() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDamageEventArgs(npc, 0, 1.234f, false, false);

            args.Knockback.Should().Be(1.234f);
        }

        [Fact]
        public void HitDirection_Get() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDamageEventArgs(npc, 0, 0, true, false);

            args.HitDirection.Should().BeTrue();
        }

        [Fact]
        public void IsCriticalHit_Get() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDamageEventArgs(npc, 0, 0, false, true);

            args.IsCriticalHit.Should().BeTrue();
        }
    }
}
