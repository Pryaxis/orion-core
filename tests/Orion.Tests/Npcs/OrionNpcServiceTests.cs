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
using Orion.Core.DataStructures;
using Orion.Core.Events;
using Orion.Core.Events.Npcs;
using Orion.Core.Items;
using Orion.Core.Packets.Npcs;
using Orion.Core.Players;
using Serilog.Core;
using Xunit;

namespace Orion.Core.Npcs {
    // These tests depend on Terraria state.
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
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

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
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

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

        [Fact]
        public void NpcKilled_EventTriggered() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcKilledEvent>(evt => {
                Assert.Same(Terraria.Main.npc[0], ((OrionNpc)evt.Npc).Wrapped);
                isRun = true;
            }, Logger.None);
            Terraria.Main.npc[0].SetDefaults((int)NpcId.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            Assert.True(isRun);
        }

        [Fact]
        public void NpcLoot_EventTriggered() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcLootEvent>(evt => {
                Assert.Same(Terraria.Main.npc[0], ((OrionNpc)evt.Npc).Wrapped);
                Assert.Equal(ItemId.Gel, evt.Id);
                Assert.InRange(evt.StackSize, 1, 2);
                Assert.Equal(ItemPrefix.Random, evt.Prefix);
                isRun = true;
            }, Logger.None);
            Terraria.Main.npc[0].SetDefaults((int)NpcId.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            Assert.True(isRun);
            Assert.Equal(ItemId.Gel, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void NpcLoot_EventModified() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcLootEvent>(evt => {
                evt.Id = ItemId.Sdmg;
                evt.StackSize = 1;
                evt.Prefix = ItemPrefix.Unreal;
            }, Logger.None);
            Terraria.Main.npc[0].SetDefaults((int)NpcId.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            Assert.Equal(ItemId.Sdmg, (ItemId)Terraria.Main.item[0].type);
            Assert.Equal(1, Terraria.Main.item[0].stack);
            Assert.Equal(ItemPrefix.Unreal, (ItemPrefix)Terraria.Main.item[0].prefix);
        }

        [Fact]
        public void NpcLoot_EventCanceled() {
            Terraria.Main.npc[0] = new Terraria.NPC { whoAmI = 0 };
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcLootEvent>(evt => evt.Cancel(), Logger.None);
            Terraria.Main.npc[0].SetDefaults((int)NpcId.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            Assert.NotEqual(ItemId.Gel, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void PacketReceive_NpcCatchEventTriggered() {
            // Set `State` to 10 so that the NPC catch packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.npc[1] = new Terraria.NPC { whoAmI = 1 };
            Terraria.Main.npc[1].SetDefaults((int)NpcId.GoldWorm);
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcCatchEvent>(evt => {
                Assert.Same(npcService.Npcs[1], evt.Npc);
                Assert.Same(playerService.Players[5], evt.Player);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, NpcCatchPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(ItemId.GoldWorm, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void PacketReceive_NpcCatchEventCanceled() {
            // Set `State` to 10 so that the NPC catch packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.npc[1] = new Terraria.NPC { whoAmI = 1 };
            Terraria.Main.npc[1].SetDefaults((int)NpcId.GoldWorm);
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcCatchEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, NpcCatchPacketTests.Bytes);

            Assert.Equal(ItemId.None, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void PacketReceive_NpcFishEventTriggered() {
            // Set `State` to 10 so that the NPC fish packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.npc[0] = new Terraria.NPC();

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<NpcFishEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.X);
                Assert.Equal(256, evt.Y);
                Assert.Equal(NpcId.HemogoblinShark, evt.Id);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, NpcFishPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(NpcId.HemogoblinShark, (NpcId)Terraria.Main.npc[0].type);
        }

        [Fact]
        public void PacketReceive_NpcFishEventCanceled() {
            // Set `State` to 10 so that the NPC fish packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.npc[0] = new Terraria.NPC();

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            kernel.RegisterHandler<NpcFishEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, NpcFishPacketTests.Bytes);

            Assert.Equal(NpcId.None, (NpcId)Terraria.Main.npc[0].type);
        }

        [Fact]
        public void SpawnNpc() {
            using var kernel = new OrionKernel(Logger.None);
            using var npcService = new OrionNpcService(kernel, Logger.None);
            var npc = npcService.SpawnNpc(NpcId.BlueSlime, Vector2f.Zero);

            Assert.NotNull(npc);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal(NpcId.BlueSlime, npc.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
