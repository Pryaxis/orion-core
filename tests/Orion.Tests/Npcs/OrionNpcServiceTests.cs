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
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Npcs {
    [Collection("TerrariaTestsCollection")]
    public class OrionNpcServiceTests : IDisposable {
        private readonly INpcService _npcService;

        public OrionNpcServiceTests() {
            for (var i = 0; i < Terraria.Main.npc.Length; ++i) {
                Terraria.Main.npc[i] = new Terraria.NPC {whoAmI = i};
            }

            for (var i = 0; i < Terraria.Main.combatText.Length; ++i) {
                Terraria.Main.combatText[i] = new Terraria.CombatText {active = true};
            }

            for (var i = 0; i < Terraria.Main.item.Length; ++i) {
                Terraria.Main.item[i] = new Terraria.Item {whoAmI = i};
            }

            for (var i = 0; i < Terraria.Main.player.Length; ++i) {
                Terraria.Main.player[i] = new Terraria.Player {whoAmI = i};
            }

            Terraria.Main.rand = new Terraria.Utilities.UnifiedRandom();

            _npcService = new OrionNpcService();
        }

        public void Dispose() {
            _npcService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var npc = (OrionNpc)_npcService[0];

            npc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var npc = _npcService[0];
            var npc2 = _npcService[0];

            npc.Should().BeSameAs(npc2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<INpc> func = () => _npcService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcSetDefaults_IsCorrect(NpcType npcType) {
            OrionNpc argsNpc = null;
            _npcService.NpcSetDefaults += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
                args.NpcType.Should().Be(npcType);
            };

            Terraria.Main.npc[0].SetDefaults((int)npcType);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcSetDefaults_ModifyNpcType_IsCorrect(NpcType oldType, NpcType newType) {
            _npcService.NpcSetDefaults += (sender, args) => {
                args.NpcType = newType;
            };

            Terraria.Main.npc[0].SetDefaults((int)oldType);

            Terraria.Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcSetDefaults_Canceled_IsCorrect() {
            _npcService.NpcSetDefaults += (sender, args) => {
                args.IsCanceled = true;
            };

            Terraria.Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Terraria.Main.npc[0].type.Should().Be(0);
        }

        [Fact]
        public void NpcSpawn_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcSpawn += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };

            var npcIndex = Terraria.NPC.NewNPC(0, 0, (int)NpcType.BlueSlime);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().Be(Terraria.Main.npc[npcIndex]);
        }

        [Fact]
        public void NpcSpawned_Canceled_IsCorrect() {
            _npcService.NpcSpawn += (sender, args) => {
                args.IsCanceled = true;
            };

            var npcIndex = Terraria.NPC.NewNPC(0, 0, (int)NpcType.BlueSlime);

            npcIndex.Should().BeGreaterOrEqualTo(_npcService.Count);
            Terraria.Main.npc[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcUpdate_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcUpdate += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };

            Terraria.Main.npc[0].UpdateNPC(0);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void NpcTransform_IsCorrect(NpcType npcType) {
            OrionNpc argsNpc = null;
            _npcService.NpcTransform += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
                args.NpcNewType.Should().Be(npcType);
            };

            Terraria.Main.npc[0].Transform((int)npcType);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcTransform_ModifyNpcNewType_IsCorrect(NpcType oldType, NpcType newType) {
            _npcService.NpcTransform += (sender, args) => {
                args.NpcNewType = newType;
            };
            Terraria.Main.npc[0].SetDefaults((int)oldType);

            Terraria.Main.npc[0].Transform((int)newType);

            Terraria.Main.npc[0].netID.Should().Be((int)newType);
        }

        [Fact]
        public void NpcTransform_Canceled_IsCorrect() {
            _npcService.NpcTransform += (sender, args) => {
                args.IsCanceled = true;
            };

            Terraria.Main.npc[0].Transform((int)NpcType.BlueSlime);

            Terraria.Main.npc[0].netID.Should().NotBe((int)NpcType.BlueSlime);
        }

        [Fact]
        public void NpcDamage_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcDamage += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
                args.Damage.Should().Be(100);
                args.Knockback.Should().Be(5.0f);
                args.HitDirection.Should().Be(1);
                args.IsCriticalHit.Should().BeTrue();
            };

            Terraria.Main.npc[0].StrikeNPC(100, 5.0f, 1, true);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void NpcDamage_Canceled_IsCorrect() {
            _npcService.NpcDamage += (sender, args) => {
                args.IsCanceled = true;
            };
            Terraria.Main.npc[0].SetDefaults((int)NpcType.BlueSlime);

            Terraria.Main.npc[0].StrikeNPC(10000, 0, 1);

            Terraria.Main.npc[0].active.Should().BeTrue();
        }

        [Fact]
        public void NpcDropLootItem_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcDropLootItem += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            Terraria.Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void NpcDropLootItem_Canceled_IsCorrect() {
            _npcService.NpcDropLootItem += (sender, args) => {
                args.IsCanceled = true;
            };
            Terraria.Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            Terraria.Main.item[0].active.Should().BeFalse();
        }

        [Fact]
        public void NpcKilled_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcKilled += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            Terraria.Main.npc[0].SetDefaults((int)NpcType.BlueSlime);
            Terraria.Main.npc[0].life = 0;

            Terraria.Main.npc[0].checkDead();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var npcs = _npcService.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                ((OrionNpc)npcs[i]).Wrapped.Should().BeSameAs(Terraria.Main.npc[i]);
            }
        }

        [Fact]
        public void SpawnNpc_IsCorrect() {
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Fact]
        public void SpawnNpc_AiValues_IsCorrect() {
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[] {1, 2, 3, 4});

            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void SpawnNpc_AiValuesWrongLength_ThrowsArgumentException(int aiValuesLength) {
            Func<INpc> func = () => _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[aiValuesLength]);

            func.Should().Throw<ArgumentException>();
        }
    }
}
