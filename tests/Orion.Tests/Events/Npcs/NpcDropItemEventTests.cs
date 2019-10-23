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
using Orion.Items;
using Orion.Npcs;
using Xunit;

namespace Orion.Events.Npcs {
    public class NpcDropItemEventTests {
        [Fact]
        public void Ctor_NotDirty() {
            var npc = new Mock<INpc>().Object;
            var e = new NpcDropItemEvent(npc, ItemType.None, 0, ItemPrefix.None);

            e.IsDirty.Should().BeFalse();
        }

        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcDropItemEvent> func = () => new NpcDropItemEvent(null, ItemType.None, 0, ItemPrefix.None);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SimpleProperties_Set_MarkAsDirty() {
            var npc = new Mock<INpc>().Object;
            var e = new NpcDropItemEvent(npc, ItemType.None, 0, ItemPrefix.None);

            e.SimpleProperties_Set_MarkAsDirty();
        }

        [Fact]
        public void ItemType_Get() {
            var npc = new Mock<INpc>().Object;
            var e = new NpcDropItemEvent(npc, ItemType.Sdmg, 0, ItemPrefix.None);

            e.ItemType.Should().Be(ItemType.Sdmg);
        }

        [Fact]
        public void ItemStackSize_Get() {
            var npc = new Mock<INpc>().Object;
            var e = new NpcDropItemEvent(npc, ItemType.None, 5167, ItemPrefix.None);

            e.StackSize.Should().Be(5167);
        }

        [Fact]
        public void ItemPrefix_Get() {
            var npc = new Mock<INpc>().Object;
            var e = new NpcDropItemEvent(npc, ItemType.None, 0, ItemPrefix.Unreal);

            e.ItemPrefix.Should().Be(ItemPrefix.Unreal);
        }
    }
}
