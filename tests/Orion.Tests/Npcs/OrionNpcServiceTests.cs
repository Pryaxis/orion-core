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
using System.Linq;
using Serilog.Core;
using Xunit;

namespace Orion.Npcs {
    [Collection("TerrariaTestsCollection")]
    public class OrionNpcServiceTests {
        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Npcs_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => npcService.Npcs[index]);
        }

        [Fact]
        public void Npcs_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var npc = npcService.Npcs[1];

            Assert.Equal(1, npc.Index);
            Assert.Same(Terraria.Main.npc[1], ((OrionNpc)npc).Wrapped);
        }

        [Fact]
        public void Npcs_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);

            var npc = npcService.Npcs[0];
            var npc2 = npcService.Npcs[0];

            Assert.Same(npc, npc2);
        }

        [Fact]
        public void Npcs_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);

            var npcs = npcService.Npcs.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                Assert.Same(Terraria.Main.npc[i], ((OrionNpc)npcs[i]).Wrapped);
            }
        }
    }
}
