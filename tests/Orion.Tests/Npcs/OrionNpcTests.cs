using System;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaNpc = new Terraria.NPC {whoAmI = index};
            var npc = new OrionNpc(terrariaNpc);

            npc.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaNpc = new Terraria.NPC {active = isActive};
            var npc = new OrionNpc(terrariaNpc);

            npc.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.IsActive = isActive;

            terrariaNpc.active.Should().Be(isActive);
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaNpc = new Terraria.NPC {_givenName = name};
            var npc = new OrionNpc(terrariaNpc);

            npc.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Name = name;

            terrariaNpc.GivenOrTypeName.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);
            Action action = () => npc.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaNpc = new Terraria.NPC {position = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Position = new Vector2(100, 100);

            terrariaNpc.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaNpc = new Terraria.NPC {velocity = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Velocity = new Vector2(100, 100);

            terrariaNpc.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaNpc = new Terraria.NPC {Size = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Size = new Vector2(100, 100);

            terrariaNpc.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        public void GetType_IsCorrect(NpcType npcType) {
            var terrariaNpc = new Terraria.NPC {netID = (int)npcType};
            var npc = new OrionNpc(terrariaNpc);

            npc.Type.Should().Be(npcType);
        }

        [Theory]
        [InlineData(100)]
        public void GetHealth_IsCorrect(int health) {
            var terrariaNpc = new Terraria.NPC {life = health};
            var npc = new OrionNpc(terrariaNpc);

            npc.Health.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void SetHealth_IsCorrect(int health) {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Health = health;

            terrariaNpc.life.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void GetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaNpc = new Terraria.NPC {lifeMax = maxHealth};
            var npc = new OrionNpc(terrariaNpc);

            npc.MaxHealth.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.MaxHealth = maxHealth;

            terrariaNpc.lifeMax.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        public void ApplyType_IsCorrect(NpcType npcType) {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.ApplyType(npcType);

            terrariaNpc.netID.Should().Be((int)npcType);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void Transform_IsCorrect(NpcType oldType, NpcType newType) {
            // TargetClosest requires non-null player array.
            for (var i = 0; i < Terraria.Main.maxPlayers; ++i) {
                Terraria.Main.player[i] = new Terraria.Player();
            }

            var terrariaNpc = new Terraria.NPC();
            terrariaNpc.SetDefaults((int)oldType);
            var npc = new OrionNpc(terrariaNpc);

            npc.Transform(newType);

            terrariaNpc.netID.Should().Be((int)newType);
        }
    }
}
