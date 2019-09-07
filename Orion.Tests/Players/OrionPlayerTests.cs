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
        [InlineData(100)]
        public void GetHp_IsCorrect(int hp) {
            var terrariaPlayer = new Terraria.Player {statLife = hp};
            var player = new OrionPlayer(terrariaPlayer);

            player.Hp.Should().Be(hp);
        }

        [Theory]
        [InlineData(100)]
        public void SetHp_IsCorrect(int hp) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Hp = hp;

            terrariaPlayer.statLife.Should().Be(hp);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMaxHp_IsCorrect(int maxMaxHp) {
            var terrariaPlayer = new Terraria.Player {statLifeMax = maxMaxHp};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHp.Should().Be(maxMaxHp);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxHp_IsCorrect(int maxMaxHp) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxHp = maxMaxHp;

            terrariaPlayer.statLifeMax.Should().Be(maxMaxHp);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMp_IsCorrect(int mp) {
            var terrariaPlayer = new Terraria.Player {statMana = mp};
            var player = new OrionPlayer(terrariaPlayer);

            player.Mp.Should().Be(mp);
        }

        [Theory]
        [InlineData(100)]
        public void SetMp_IsCorrect(int mp) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.Mp = mp;

            terrariaPlayer.statMana.Should().Be(mp);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetMaxMp_IsCorrect(int maxMaxMp) {
            var terrariaPlayer = new Terraria.Player {statManaMax = maxMaxMp};
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMp.Should().Be(maxMaxMp);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxMp_IsCorrect(int maxMaxMp) {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);

            player.MaxMp = maxMaxMp;

            terrariaPlayer.statManaMax.Should().Be(maxMaxMp);
        }
    }
}
