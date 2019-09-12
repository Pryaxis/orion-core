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
using Orion.Items;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDroppedLootItemEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            var item = new Mock<IItem>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(null, item.Object);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            var npc = new Mock<INpc>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(npc.Object, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var item = new Mock<IItem>().Object;
            var args = new NpcDroppedLootItemEventArgs(npc, item);

            args.Npc.Should().BeSameAs(npc);
        }

        [Fact]
        public void GetLootItem_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var item = new Mock<IItem>().Object;
            var args = new NpcDroppedLootItemEventArgs(npc, item);

            args.LootItem.Should().BeSameAs(item);
        }
    }
}
