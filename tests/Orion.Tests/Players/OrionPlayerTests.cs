using System;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Players {
    public class OrionPlayerTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaPlayer = new Terraria.Player {whoAmI = index};
            var player = new OrionPlayer(terrariaPlayer);

            player.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaPlayer = new Terraria.Player {active = isActive};
            var player = new OrionPlayer(terrariaPlayer);

            player.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.IsActive = isActive;

            terrariaPlayer.active.Should().Be(isActive);
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaPlayer = new Terraria.Player {name = name};
            var player = new OrionPlayer(terrariaPlayer);

            player.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Name = name;

            terrariaPlayer.name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaPlayer = new Terraria.Player {position = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Position.Should().Be(new Vector2(100, 100));
        }
        
        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Position = new Vector2(100, 100);

            terrariaPlayer.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaPlayer = new Terraria.Player {velocity = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Velocity.Should().Be(new Vector2(100, 100));
        }
        
        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Velocity = new Vector2(100, 100);

            terrariaPlayer.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaPlayer = new Terraria.Player {Size = new Vector2(100, 100)};
            var player = new OrionPlayer(terrariaPlayer);

            player.Size.Should().Be(new Vector2(100, 100));
        }
        
        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Size = new Vector2(100, 100);

            terrariaPlayer.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(PlayerDifficulty.Mediumcore)]
        public void GetDifficulty_IsCorrect(PlayerDifficulty difficulty) {
            var terrariaPlayer = new Terraria.Player {difficulty = (byte)difficulty};
            var player = new OrionPlayer(terrariaPlayer);

            player.Difficulty.Should().Be(difficulty);
        }

        [Theory]
        [InlineData(PlayerDifficulty.Mediumcore)]
        public void SetDifficulty_IsCorrect(PlayerDifficulty difficulty) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Difficulty = difficulty;

            terrariaPlayer.difficulty.Should().Be((byte)difficulty);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetHealth_IsCorrect(int health) {
            var terrariaPlayer = new Terraria.Player {statLife = health};
            var player = new OrionPlayer(terrariaPlayer);

            player.Health.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void SetHealth_IsCorrect(int health) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Health = health;

            terrariaPlayer.statLife.Should().Be(health);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaPlayer = new Terraria.Player {statLifeMax = maxHealth};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHealth.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHealth = maxHealth;

            terrariaPlayer.statLifeMax.Should().Be(maxHealth);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMana_IsCorrect(int mana) {
            var terrariaPlayer = new Terraria.Player {statMana = mana};
            var player = new OrionPlayer(terrariaPlayer);

            player.Mana.Should().Be(mana);
        }

        [Theory]
        [InlineData(100)]
        public void SetMana_IsCorrect(int mana) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Mana = mana;

            terrariaPlayer.statMana.Should().Be(mana);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMaxMana_IsCorrect(int maxMana) {
            var terrariaPlayer = new Terraria.Player {statManaMax = maxMana};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMana.Should().Be(maxMana);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxMana_IsCorrect(int maxMana) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMana = maxMana;

            terrariaPlayer.statManaMax.Should().Be(maxMana);
        }

        [Theory]
        [InlineData(PlayerTeam.Red)]
        public void GetTeam_IsCorrect(PlayerTeam team) {
            var terrariaPlayer = new Terraria.Player {team = (byte)team};
            var player = new OrionPlayer(terrariaPlayer);

            player.Team.Should().Be(team);
        }

        [Theory]
        [InlineData(PlayerTeam.Red)]
        public void SetTeam_IsCorrect(PlayerTeam team) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Team = team;

            terrariaPlayer.team.Should().Be((byte)team);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsInPvp_IsCorrect(bool isInPvp) {
            var terrariaPlayer = new Terraria.Player {hostile = isInPvp};
            var player = new OrionPlayer(terrariaPlayer);

            player.IsInPvp.Should().Be(isInPvp);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsInPvp_IsCorrect(bool isInPvp) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.IsInPvp = isInPvp;

            terrariaPlayer.hostile.Should().Be(isInPvp);
        }

        [Fact]
        public void Buffs_GetIndex_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            terrariaPlayer.buffType[0] = (int)BuffType.ObsidianSkin;
            terrariaPlayer.buffTime[0] = 3600;
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs[0].Should().Be(new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1)));
        }

        [Fact]
        public void Buffs_SetIndex_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs[0] = new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1));

            terrariaPlayer.buffType[0].Should().Be((int)BuffType.ObsidianSkin);
            terrariaPlayer.buffTime[0].Should().Be(3600);
        }

        [Fact]
        public void Buffs_GetCount_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Buffs.Count.Should().Be(terrariaPlayer.buffType.Length);
        }

        [Fact]
        public void AddBuff_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.AddBuff(new Buff(BuffType.ObsidianSkin, TimeSpan.FromMinutes(1)));

            terrariaPlayer.buffType[0].Should().Be((int)BuffType.ObsidianSkin);
            terrariaPlayer.buffTime[0].Should().Be(3600);
        }
    }
}
