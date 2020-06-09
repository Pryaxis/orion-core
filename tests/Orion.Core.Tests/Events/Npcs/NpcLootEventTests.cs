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
using System.Diagnostics.CodeAnalysis;
using Moq;
using Orion.Core.Items;
using Orion.Core.Npcs;
using Xunit;

namespace Orion.Core.Events.Npcs {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcLootEventTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new NpcLootEvent(null!));
        }

        [Fact]
        public void Id_Set_Get() {
            var npc = new Mock<INpc>().Object;
            var evt = new NpcLootEvent(npc);

            evt.Id = ItemId.Sdmg;

            Assert.Equal(ItemId.Sdmg, evt.Id);
        }

        [Fact]
        public void StackSize_Set_Get() {
            var npc = new Mock<INpc>().Object;
            var evt = new NpcLootEvent(npc);

            evt.StackSize = 1;

            Assert.Equal(1, evt.StackSize);
        }

        [Fact]
        public void Prefix_Set_Get() {
            var npc = new Mock<INpc>().Object;
            var evt = new NpcLootEvent(npc);

            evt.Prefix = ItemPrefix.Unreal;

            Assert.Equal(ItemPrefix.Unreal, evt.Prefix);
        }

        [Fact]
        public void CancellationReason_Set_Get() {
            var npc = new Mock<INpc>().Object;
            var evt = new NpcLootEvent(npc);

            evt.CancellationReason = "test";

            Assert.Equal("test", evt.CancellationReason);
        }
    }
}
