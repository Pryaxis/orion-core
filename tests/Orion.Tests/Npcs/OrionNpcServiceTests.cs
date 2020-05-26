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
using Orion.Events;
using Orion.Events.Npcs;
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

        [Theory]
        [InlineData(NpcId.BlueSlime)]
        [InlineData(NpcId.GreenSlime)]
        public void NpcSetDefaults_EventTriggered(NpcId id) {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcDefaultsEvent>(evt => {
                Assert.Same(Terraria.Main.npc[0], ((OrionNpc)evt.Npc).Wrapped);
                Assert.Equal(id, evt.Id);
                isRun = true;
            }, Logger.None);

            Terraria.Main.npc[0].SetDefaults((int)id);

            Assert.True(isRun);
            Assert.Equal(id, (NpcId)Terraria.Main.npc[0].netID);
        }

        [Theory]
        [InlineData(NpcId.BlueSlime, NpcId.GreenSlime)]
        [InlineData(NpcId.BlueSlime, NpcId.None)]
        public void NpcSetDefaults_EventModified(NpcId oldId, NpcId newId) {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcDefaultsEvent>(evt => evt.Id = newId, Logger.None);

            Terraria.Main.npc[0].SetDefaults((int)oldId);

            Assert.Equal(newId, (NpcId)Terraria.Main.npc[0].netID);
        }

        [Fact]
        public void NpcSetDefaults_EventCanceled() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcDefaultsEvent>(evt => evt.Cancel(), Logger.None);

            Terraria.Main.npc[0].SetDefaults((int)NpcId.BlueSlime);

            Assert.Equal(NpcId.None, (NpcId)Terraria.Main.npc[0].netID);
        }

        [Fact]
        public void NpcSpawn_EventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            INpc? evtNpc = null;
            kernel.RegisterHandler<NpcSpawnEvent>(evt => evtNpc = evt.Npc, Logger.None);

            var npcIndex = Terraria.NPC.NewNPC(0, 0, (int)NpcId.BlueSlime);

            Assert.NotNull(evtNpc);
            Assert.Same(Terraria.Main.npc[npcIndex], ((OrionNpc)evtNpc!).Wrapped);
        }

        [Fact]
        public void NpcSpawn_EventCanceled() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcSpawnEvent>(evt => evt.Cancel(), Logger.None);

            var npcIndex = Terraria.NPC.NewNPC(0, 0, (int)NpcId.BlueSlime);

            Assert.Equal(npcService.Npcs.Count, npcIndex);
            Assert.False(Terraria.Main.npc[0].active);
        }

        [Fact]
        public void NpcTick_EventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcTickEvent>(evt => {
                Assert.Same(Terraria.Main.npc[0], ((OrionNpc)evt.Npc).Wrapped);
                isRun = true;
            }, Logger.None);

            Terraria.Main.npc[0].UpdateNPC(0);

            Assert.True(isRun);
        }
    }
}
