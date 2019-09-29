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
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Events.Extensions;
using Xunit;
using Main = Terraria.Main;
using TerrariaNpc = Terraria.NPC;
using TerrariaItem = Terraria.Item;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Npcs {
    [Collection("TerrariaTestsCollection")]
    public class OrionNpcServiceTests : IDisposable {
        private readonly INpcService _npcService;

        public OrionNpcServiceTests() {
            for (var i = 0; i < Main.npc.Length; ++i) {
                Main.npc[i] = new TerrariaNpc {whoAmI = i};
            }

            for (var i = 0; i < Main.combatText.Length; ++i) {
                Main.combatText[i] = new Terraria.CombatText {active = true};
            }

            for (var i = 0; i < Main.item.Length; ++i) {
                Main.item[i] = new TerrariaItem {whoAmI = i};
            }

            for (var i = 0; i < Main.player.Length; ++i) {
                Main.player[i] = new TerrariaPlayer {whoAmI = i};
            }

            Main.rand = new Terraria.Utilities.UnifiedRandom();

            _npcService = new OrionNpcService();
        }

        public void Dispose() {
            _npcService.Dispose();
        }

        [Fact]
        public void Npcs_GetItem_IsCorrect() {
            var npc = _npcService.Npcs[0];

            ((OrionNpc)npc).Wrapped.Should().BeSameAs(Main.npc[0]);
        }

        [Fact]
        public void Npcs_GetItem_MultipleTimes_ReturnsSameInstance() {
            var npc = _npcService.Npcs[0];
            var npc2 = _npcService.Npcs[0];

            npc.Should().BeSameAs(npc2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Npcs_GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<INpc> func = () => _npcService.Npcs[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Npcs_GetEnumerator_IsCorrect() {
            var npcs = _npcService.Npcs.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                ((OrionNpc)npcs[i]).Wrapped.Should().BeSameAs(Main.npc[i]);
            }
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcSetDefaults_IsCorrect(NpcType npcType) {
            var isRun = false;
            _npcService.NpcSetDefaults += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.NpcType.Should().Be(npcType);
            };

            Main.npc[0].SetDefaults((int)npcType);

            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcSetDefaults_ModifyNpcType_IsCorrect(NpcType oldType, NpcType newType) {
            _npcService.NpcSetDefaults += (sender, args) => {
                args.NpcType = newType;
            };

            Main.npc[0].SetDefaults((int)oldType);

            Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcSetDefaults_Canceled_IsCorrect() {
            _npcService.NpcSetDefaults += (sender, args) => {
                args.Cancel();
            };

            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Main.npc[0].type.Should().Be(0);
        }

        [Fact]
        public void NpcSpawn_IsCorrect() {
            INpc? argsNpc = null;
            _npcService.NpcSpawn += (sender, args) => {
                argsNpc = args.Npc;
            };

            var npcIndex = TerrariaNpc.NewNPC(0, 0, (int)NpcType.BlueSlime);

            argsNpc.Should().NotBeNull();
            ((OrionNpc)argsNpc!).Wrapped.Should().Be(Main.npc[npcIndex]);
        }

        [Fact]
        public void NpcSpawn_Canceled_IsCorrect() {
            _npcService.NpcSpawn += (sender, args) => {
                args.Cancel();
            };

            var npcIndex = TerrariaNpc.NewNPC(0, 0, (int)NpcType.BlueSlime);

            npcIndex.Should().BeGreaterOrEqualTo(_npcService.Npcs.Count);
            Main.npc[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcUpdate_IsCorrect() {
            var isRun = false;
            _npcService.NpcUpdate += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            };

            Main.npc[0].UpdateNPC(0);
            
            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcTransform_IsCorrect(NpcType npcType) {
            var isRun = false;
            _npcService.NpcTransform += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.NpcNewType.Should().Be(npcType);
            };

            Main.npc[0].Transform((int)npcType);
            
            isRun.Should().BeTrue();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcTransform_ModifyNpcNewType_IsCorrect(NpcType oldType, NpcType newType) {
            _npcService.NpcTransform += (sender, args) => {
                args.NpcNewType = newType;
            };
            Main.npc[0].SetDefaults((int)oldType);

            Main.npc[0].Transform((int)newType);

            Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcTransform_Canceled_IsCorrect() {
            _npcService.NpcTransform += (sender, args) => {
                args.Cancel();
            };

            Main.npc[0].Transform((int)NpcType.BlueSlime);

            Main.npc[0].netID.Should().NotBe((int)NpcType.BlueSlime);
        }

        [Fact]
        public void NpcDamage_IsCorrect() {
            var isRun = false;
            _npcService.NpcDamage += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
                args.Damage.Should().Be(100);
                args.Knockback.Should().Be(5.0f);
                args.HitDirection.Should().Be(1);
                args.IsCriticalHit.Should().BeTrue();
            };

            Main.npc[0].StrikeNPC(100, 5.0f, 1, true);
            
            isRun.Should().BeTrue();
        }

        [Fact]
        public void NpcDamage_Canceled_IsCorrect() {
            _npcService.NpcDamage += (sender, args) => {
                args.Cancel();
            };
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Main.npc[0].StrikeNPC(10000, 0, 1);

            Main.npc[0].active.Should().BeTrue();
        }

        [Fact]
        public void NpcDropLootItem_IsCorrect() {
            var isRun = false;
            _npcService.NpcDropLootItem += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            };
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();
            
            isRun.Should().BeTrue();
        }

        [Fact]
        public void NpcDropLootItem_Canceled_IsCorrect() {
            _npcService.NpcDropLootItem += (sender, args) => {
                args.Cancel();
            };
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();

            Main.item[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcKilled_IsCorrect() {
            var isRun = false;
            _npcService.NpcKilled += (sender, args) => {
                isRun = true;
                ((OrionNpc)args.Npc).Wrapped.Should().BeSameAs(Main.npc[0]);
            };
            Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Main.npc[0].life = 0;

            Main.npc[0].checkDead();
            
            isRun.Should().BeTrue();
        }

        [Fact]
        public void SpawnNpc_IsCorrect() {
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            Debug.Assert(npc != null);
            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Fact]
        public void SpawnNpc_AiValues_IsCorrect() {
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[] {1, 2, 3, 4});

            Debug.Assert(npc != null);
            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void SpawnNpc_AiValuesWrongLength_ThrowsArgumentException(int aiValuesLength) {
            Func<INpc?> func = () => _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[aiValuesLength]);

            func.Should().Throw<ArgumentException>();
        }
    }
}
