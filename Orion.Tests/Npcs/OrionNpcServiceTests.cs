using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Moq;
using Orion.Items;
using Orion.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcServiceTests : IDisposable {
        private readonly Mock<IItemService> _itemService = new Mock<IItemService>();
        private readonly Mock<IPlayerService> _playerService = new Mock<IPlayerService>();
        private readonly INpcService _npcService;

        public OrionNpcServiceTests() {
            for (var i = 0; i < Terraria.Main.maxNPCs + 1; ++i) {
                Terraria.Main.npc[i] = new Terraria.NPC {whoAmI = i};
            }
            for (var i = 0; i < Terraria.Main.maxCombatText; ++i) {
                Terraria.Main.combatText[i] = new Terraria.CombatText {active = true};
            }
            for (var i = 0; i < Terraria.Main.maxItems + 1; ++i) {
                Terraria.Main.item[i] = new Terraria.Item {whoAmI = i};
            }
            for (var i = 0; i < Terraria.Main.maxPlayers + 1; ++i) {
                Terraria.Main.player[i] = new Terraria.Player {whoAmI = i};
            }

            Terraria.Main.rand = new Terraria.Utilities.UnifiedRandom();
            
            _itemService.Setup(s => s.SpawnItem(It.IsAny<ItemType>(), It.IsAny<Vector2>(), It.IsAny<int>(),
                                                It.IsAny<ItemPrefix>()))
                        .Returns(new Mock<IItem>().Object);
            _npcService = new OrionNpcService(_itemService.Object, _playerService.Object);
        }

        public void Dispose() {
            _npcService.Dispose();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = (OrionNpc)_npcService[0];

            npc.Wrapped.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void GetNpc_MultipleTimes_ReturnsSameInstance() {
            var npc = _npcService[0];
            var npc2 = _npcService[0];

            npc.Should().BeSameAs(npc2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetNpc_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<INpc> func = () => _npcService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void NpcSpawning_IsCorrect() {
            var isRun = false;
            _npcService.SpawningNpc += (sender, args) => {
                isRun = true;
                args.NpcType.Should().Be(NpcType.BlueSlime);
                args.Position.Should().Be(Vector2.Zero);
            };

            _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void NpcSpawning_Handled_IsCorrect() {
            _npcService.SpawningNpc += (sender, args) => {
                args.Handled = true;
            };

            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Should().BeNull();
        }

        [Fact]
        public void NpcSettingDefaults_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.SettingNpcDefaults += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };

            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcSettingDefaults_ModifyType_IsCorrect(NpcType oldType, NpcType newType) {
            _npcService.SettingNpcDefaults += (sender, args) => {
                args.Type = newType;
            };

            var npc = _npcService.SpawnNpc(oldType, Vector2.Zero);

            npc.Type.Should().Be(newType);
        }

        [Fact]
        public void NpcSettingDefaults_Handled_IsCorrect() {
            _npcService.SettingNpcDefaults += (sender, args) => {
                args.Handled = true;
            };

            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Type.Should().Be(NpcType.None);
        }

        [Fact]
        public void NpcSettingDefaults_NegativeType_RunsOnce() {
            var count = 0;
            _npcService.SettingNpcDefaults += (sender, args) => {
                ++count;
            };

            _npcService.SpawnNpc(NpcType.GreenSlime, Vector2.Zero);

            count.Should().Be(1);
        }

        [Fact]
        public void NpcSetDefaults_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.SetNpcDefaults += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };

            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcSetDefaults_NegativeType_RunsOnce() {
            var count = 0;
            _npcService.SetNpcDefaults += (sender, args) => {
                ++count;
            };

            _npcService.SpawnNpc(NpcType.GreenSlime, Vector2.Zero);

            count.Should().Be(1);
        }

        [Fact]
        public void NpcUpdating_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.UpdatingNpc += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Wrapped.UpdateNPC(npc.Index);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcUpdatingAi_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.UpdatingNpcAi += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Wrapped.AI();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcUpdatedAi_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.UpdatedNpcAi += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Wrapped.AI();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcUpdated_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.UpdatedNpc += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Wrapped.UpdateNPC(npc.Index);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Theory]
        [InlineData(10, 0.4, 1)]
        [InlineData(100, -0.4, -1)]
        public void NpcDamaging_IsCorrect(int damage, float knockback, int hitDirection) {
            OrionNpc argsNpc = null;
            _npcService.DamagingNpc += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
                args.Damage.Should().Be(damage);
                args.Knockback.Should().Be(knockback);
                args.HitDirection.Should().Be(hitDirection);
                args.PlayerResponsible.Should().BeNull();
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Damage(damage, knockback, hitDirection);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }
        
        [Fact]
        public void NpcDamaging_Handled_IsCorrect() {
            _npcService.DamagingNpc += (sender, args) => {
                args.Handled = true;
            };
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Damage(10000, 0, 0);

            npc.Hp.Should().Be(npc.MaxHp);
        }

        [Theory]
        [InlineData(10, 0.4, 1)]
        [InlineData(100, -0.4, -1)]
        public void NpcDamaged_IsCorrect(int damage, float knockback, int hitDirection) {
            OrionNpc argsNpc = null;
            _npcService.DamagedNpc += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
                args.Damage.Should().Be(damage);
                args.Knockback.Should().Be(knockback);
                args.HitDirection.Should().Be(hitDirection);
                args.PlayerResponsible.Should().BeNull();
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Damage(damage, knockback, hitDirection);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }
        
        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void NpcTransforming_IsCorrect(NpcType oldType, NpcType newType) {
            OrionNpc argsNpc = null;
            _npcService.NpcTransforming += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(oldType, Vector2.Zero);

            npc.Transform(newType);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcTransforming_Handled_IsCorrect() {
            _npcService.NpcTransforming += (sender, args) => {
                args.Handled = true;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Transform(NpcType.GreenSlime);

            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Fact]
        public void NpcTransformed_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcTransformed += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Transform(NpcType.BlueSlime);

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcDroppingLootItem_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcDroppingLootItem += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Kill();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcDroppingLootItem_Handled_IsCorrect() {
            _npcService.NpcDroppingLootItem += (sender, args) => {
                args.Handled = true;
            };
            var npc = _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Kill();

            _itemService.VerifyNoOtherCalls();
        }

        [Fact]
        public void NpcDroppedLootItem_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.NpcDroppedLootItem += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Kill();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void NpcKilled_IsCorrect() {
            OrionNpc argsNpc = null;
            _npcService.KilledNpc += (sender, args) => {
                argsNpc = (OrionNpc)args.Npc;
            };
            var npc = (OrionNpc)_npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero);

            npc.Kill();

            argsNpc.Should().NotBeNull();
            argsNpc.Wrapped.Should().BeSameAs(npc.Wrapped);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var npcs = _npcService.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                ((OrionNpc)npcs[i]).Wrapped.Should().BeSameAs(Terraria.Main.npc[i]);
            }
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        [InlineData(NpcType.GreenSlime)]
        public void SpawnNpc_IsCorrect(NpcType type) {
            var npc = _npcService.SpawnNpc(type, Vector2.Zero);

            npc.Type.Should().Be(type);
        }

        [Fact]
        public void SpawnNpc_InvalidAiValues_ThrowsArgumentException() {
            Func<INpc> func = () => _npcService.SpawnNpc(NpcType.BlueSlime, Vector2.Zero, new float[8]);

            func.Should().Throw<ArgumentException>();
        }
    }
}
