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
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Events;
using Serilog.Core;
using Xunit;
using Main = Terraria.Main;
using TerrariaItem = Terraria.Item;
using TerrariaNpc = Terraria.NPC;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Npcs {
    [Collection("TerrariaTestsCollection")]
    public class OrionNpcServiceTests {
        public OrionNpcServiceTests() {
            for (var i = 0; i < Main.npc.Length; ++i) {
                Main.npc[i] = new TerrariaNpc { whoAmI = i };
            }

            for (var i = 0; i < Main.combatText.Length; ++i) {
                Main.combatText[i] = new Terraria.CombatText { active = true };
            }

            for (var i = 0; i < Main.item.Length; ++i) {
                Main.item[i] = new TerrariaItem { whoAmI = i };
            }

            for (var i = 0; i < Main.player.Length; ++i) {
                Main.player[i] = new TerrariaPlayer { whoAmI = i };
            }

            Main.rand = new Terraria.Utilities.UnifiedRandom();
        }

        [Fact]
        public void Npcs_Item_Get() {
            using var npcService = new OrionNpcService(Logger.None);
            var npc = npcService.Npcs[1];

            npc.Index.Should().Be(1);
            ((OrionNpc)npc).Wrapped.Should().BeSameAs(Main.npc[1]);
        }

        [Fact]
        public void Npcs_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var npcService = new OrionNpcService(Logger.None);
            var npc = npcService.Npcs[0];
            var npc2 = npcService.Npcs[0];

            npc.Should().BeSameAs(npc2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Npcs_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var npcService = new OrionNpcService(Logger.None);
            Func<INpc> func = () => npcService.Npcs[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Npcs_GetEnumerator() {
            using var npcService = new OrionNpcService(Logger.None);
            var npcs = npcService.Npcs.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                ((OrionNpc)npcs[i]).Wrapped.Should().BeSameAs(Main.npc[i]);
            }
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcSetDefaults(NpcType npcType) {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcSetDefaults.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.NpcType.Should().Be(npcType);
            });

            Main.npc[0].SetDefaults((int)npcType);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcSetDefaults_ModifyNpcType(NpcType oldType, NpcType newType) {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcSetDefaults.RegisterHandler((sender, args) => {
                args.NpcType = newType;
            });

            Main.npc[0].SetDefaults((int)oldType);

            Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcSetDefaults_Canceled() {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcSetDefaults.RegisterHandler((sender, args) => {
                args.Cancel();
            });

            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Main.npc[0].type.Should().Be(0);
        }

        [Fact]
        public void NpcSpawn() {
            using var npcService = new OrionNpcService(Logger.None);
            INpc argsNpc = null;
            npcService.NpcSpawn.RegisterHandler((sender, args) => {
                argsNpc = args.Npc;
            });

            var npcIndex = TerrariaNpc.NewNPC(0, 0, (int)NpcType.BlueSlime);

            argsNpc.Should().NotBeNull();
            ((OrionNpc)argsNpc!).Wrapped.Should().Be(Main.npc[npcIndex]);
        }

        [Fact]
        public void NpcSpawn_Canceled() {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcSpawn.RegisterHandler((sender, args) => {
                args.Cancel();
            });

            var npcIndex = TerrariaNpc.NewNPC(0, 0, (int)NpcType.BlueSlime);

            npcIndex.Should().BeGreaterOrEqualTo(npcService.Npcs.Count);
            Main.npc[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcUpdate() {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcUpdate.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            });

            Main.npc[0].UpdateNPC(0);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcTransform(NpcType npcType) {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcTransform.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.NpcNewType.Should().Be(npcType);
            });

            Main.npc[0].Transform((int)npcType);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcTransform_ModifyNpcNewType(NpcType oldType, NpcType newType) {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcTransform.RegisterHandler((sender, args) => {
                args.NpcNewType = newType;
            });
            Main.npc[0].SetDefaults((int)oldType);

            Main.npc[0].Transform((int)newType);

            Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcTransform_Canceled() {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcTransform.RegisterHandler((sender, args) => {
                args.Cancel();
            });

            Main.npc[0].Transform((int)NpcType.BlueSlime);

            Main.npc[0].netID.Should().NotBe((int)NpcType.BlueSlime);
        }

        [Fact]
        public void NpcDamage() {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcDamage.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.Damage.Should().Be(100);
                args.Knockback.Should().Be(5.0f);
                args.HitDirection.Should().Be(1);
                args.IsCriticalHit.Should().BeTrue();
            });

            Main.npc[0].StrikeNPC(100, 5.0f, 1, true);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void NpcDamage_Canceled() {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcDamage.RegisterHandler((sender, args) => {
                args.Cancel();
            });
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Main.npc[0].StrikeNPC(10000, 0, 1);

            Main.npc[0].active.Should().BeTrue();
        }

        [Fact]
        public void NpcDropLootItem() {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcDropLootItem.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            });
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();

            isRun.Should().BeTrue();
        }

        [Fact]
        public void NpcDropLootItem_Canceled() {
            using var npcService = new OrionNpcService(Logger.None);
            npcService.NpcDropLootItem.RegisterHandler((sender, args) => {
                args.Cancel();
            });
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();

            Main.item[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcKilled() {
            using var npcService = new OrionNpcService(Logger.None);
            var isRun = false;
            npcService.NpcKilled.RegisterHandler((sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            });
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();

            isRun.Should().BeTrue();
        }

        [Fact]
        public void SpawnNpc() {
            using var npcService = new OrionNpcService(Logger.None);
            var npc = npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Should().NotBeNull();
            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Fact]
        public void SpawnNpc_AiValues() {
            using var npcService = new OrionNpcService(Logger.None);
            var npc = npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[] { 1, 2, 3, 4 });

            npc.Should().NotBeNull();
            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void SpawnNpc_AiValuesWrongLength_ThrowsArgumentException(int aiValuesLength) {
            using var npcService = new OrionNpcService(Logger.None);
            Func<INpc> func = () => npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[aiValuesLength]);

            func.Should().Throw<ArgumentException>();
        }
    }
}
