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
using Orion.Items;
using Orion.Players;
using Terraria;
using Xunit;

namespace Orion.Tests.Players {
    public class OrionPlayerTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaPlayer = new Player {whoAmI = index};
            var player = new OrionPlayer(terrariaPlayer);

            player.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaPlayer = new Player {active = isActive};
            var player = new OrionPlayer(terrariaPlayer);

            player.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.IsActive = isActive;

            terrariaPlayer.active.Should().Be(isActive);
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaPlayer = new Player {name = name};
            var player = new OrionPlayer(terrariaPlayer);

            player.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Name = name;

            terrariaPlayer.name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaPlayer = new Player {position = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Position = new Vector2(100, 100);

            terrariaPlayer.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaPlayer = new Player {velocity = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Velocity = new Vector2(100, 100);

            terrariaPlayer.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaPlayer = new Player {Size = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Size = new Vector2(100, 100);

            terrariaPlayer.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(PlayerDifficulty.Mediumcore)]
        public void GetDifficulty_IsCorrect(PlayerDifficulty difficulty) {
            var terrariaPlayer = new Player {difficulty = (byte)difficulty};
            var player = new OrionPlayer(terrariaPlayer);

            player.Difficulty.Should().Be(difficulty);
        }

        [Theory]
        [InlineData(PlayerDifficulty.Mediumcore)]
        public void SetDifficulty_IsCorrect(PlayerDifficulty difficulty) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Difficulty = difficulty;

            terrariaPlayer.difficulty.Should().Be((byte)difficulty);
        }

        [Theory]
        [InlineData(100)]
        public void GetHealth_IsCorrect(int health) {
            var terrariaPlayer = new Player {statLife = health};
            var player = new OrionPlayer(terrariaPlayer);

            player.Health.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void SetHealth_IsCorrect(int health) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Health = health;

            terrariaPlayer.statLife.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void GetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaPlayer = new Player {statLifeMax = maxHealth};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHealth.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHealth = maxHealth;

            terrariaPlayer.statLifeMax.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(100)]
        public void GetMana_IsCorrect(int mana) {
            var terrariaPlayer = new Player {statMana = mana};
            var player = new OrionPlayer(terrariaPlayer);

            player.Mana.Should().Be(mana);
        }

        [Theory]
        [InlineData(100)]
        public void SetMana_IsCorrect(int mana) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Mana = mana;

            terrariaPlayer.statMana.Should().Be(mana);
        }

        [Theory]
        [InlineData(100)]
        public void GetMaxMana_IsCorrect(int maxMana) {
            var terrariaPlayer = new Player {statManaMax = maxMana};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMana.Should().Be(maxMana);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxMana_IsCorrect(int maxMana) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMana = maxMana;

            terrariaPlayer.statManaMax.Should().Be(maxMana);
        }

        [Theory]
        [InlineData(PlayerTeam.Red)]
        public void GetTeam_IsCorrect(PlayerTeam team) {
            var terrariaPlayer = new Player {team = (byte)team};
            var player = new OrionPlayer(terrariaPlayer);

            player.Team.Should().Be(team);
        }

        [Theory]
        [InlineData(PlayerTeam.Red)]
        public void SetTeam_IsCorrect(PlayerTeam team) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Team = team;

            terrariaPlayer.team.Should().Be((byte)team);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsInPvp_IsCorrect(bool isInPvp) {
            var terrariaPlayer = new Player {hostile = isInPvp};
            var player = new OrionPlayer(terrariaPlayer);

            player.IsInPvp.Should().Be(isInPvp);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsInPvp_IsCorrect(bool isInPvp) {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.IsInPvp = isInPvp;

            terrariaPlayer.hostile.Should().Be(isInPvp);
        }

        [Fact]
        public void Buffs_GetIndex_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.buffType[0] = (int)BuffType.ObsidianSkin;
            terrariaPlayer.buffTime[0] = 3600;
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs[0].Should().Be(new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1)));
        }

        [Fact]
        public void Buffs_SetIndex_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs[0] = new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1));

            terrariaPlayer.buffType[0].Should().Be((int)BuffType.ObsidianSkin);
            terrariaPlayer.buffTime[0].Should().Be(3600);
        }

        [Fact]
        public void Buffs_GetCount_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs.Count.Should().Be(terrariaPlayer.buffType.Length);
        }

        [Fact]
        public void Buffs_GetEnumerable_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            var buffs = player.Buffs.ToList();

            foreach (var buff in buffs) {
                buff.BuffType.Should().Be(BuffType.None);
                buff.Duration.Should().Be(TimeSpan.Zero);
            }
        }

        [Fact]
        public void GetInventory_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.inventory[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.Inventory[0].Type.Should().Be(ItemType.SDMG);
            player.Inventory[0].StackSize.Should().Be(1);
            player.Inventory[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetEquips_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.armor[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.Equips[0].Type.Should().Be(ItemType.SDMG);
            player.Equips[0].StackSize.Should().Be(1);
            player.Equips[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetDyes_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.dye[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.Dyes[0].Type.Should().Be(ItemType.SDMG);
            player.Dyes[0].StackSize.Should().Be(1);
            player.Dyes[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetMiscEquips_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.miscEquips[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.MiscEquips[0].Type.Should().Be(ItemType.SDMG);
            player.MiscEquips[0].StackSize.Should().Be(1);
            player.MiscEquips[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetMiscDyes_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.miscDyes[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.MiscDyes[0].Type.Should().Be(ItemType.SDMG);
            player.MiscDyes[0].StackSize.Should().Be(1);
            player.MiscDyes[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetPiggyBank_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.bank = new Chest();
            terrariaPlayer.bank.item[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.PiggyBank[0].Type.Should().Be(ItemType.SDMG);
            player.PiggyBank[0].StackSize.Should().Be(1);
            player.PiggyBank[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetSafe_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.bank2.item[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.Safe[0].Type.Should().Be(ItemType.SDMG);
            player.Safe[0].StackSize.Should().Be(1);
            player.Safe[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetTrashCan_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.trashItem = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.TrashCan.Type.Should().Be(ItemType.SDMG);
            player.TrashCan.StackSize.Should().Be(1);
            player.TrashCan.Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void GetDefendersForge_IsCorrect() {
            var terrariaPlayer = new Player();
            terrariaPlayer.bank3.item[0] = new Item {
                type = (int)ItemType.SDMG,
                stack = 1,
                prefix = (byte)ItemPrefix.Unreal
            };
            var player = new OrionPlayer(terrariaPlayer);

            player.DefendersForge[0].Type.Should().Be(ItemType.SDMG);
            player.DefendersForge[0].StackSize.Should().Be(1);
            player.DefendersForge[0].Prefix.Should().Be(ItemPrefix.Unreal);
        }

        [Fact]
        public void AddBuff_IsCorrect() {
            var terrariaPlayer = new Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.AddBuff(new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1)));

            terrariaPlayer.buffType[0].Should().Be((int)BuffType.ObsidianSkin);
            terrariaPlayer.buffTime[0].Should().Be(3600);
        }
    }
}
