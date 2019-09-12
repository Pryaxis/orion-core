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
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Npcs;
using Terraria;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaNpc = new NPC {whoAmI = index};
            var npc = new OrionNpc(terrariaNpc);

            npc.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaNpc = new NPC {active = isActive};
            var npc = new OrionNpc(terrariaNpc);

            npc.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.IsActive = isActive;

            terrariaNpc.active.Should().Be(isActive);
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaNpc = new NPC {_givenName = name};
            var npc = new OrionNpc(terrariaNpc);

            npc.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Name = name;

            terrariaNpc.GivenOrTypeName.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);
            Action action = () => npc.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaNpc = new NPC {position = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Position = new Vector2(100, 100);

            terrariaNpc.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaNpc = new NPC {velocity = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Velocity = new Vector2(100, 100);

            terrariaNpc.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaNpc = new NPC {Size = new Vector2(100, 100)};
            var npc = new OrionNpc(terrariaNpc);

            npc.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Size = new Vector2(100, 100);

            terrariaNpc.Size.Should().Be(new Vector2(100, 100));
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        public void GetType_IsCorrect(NpcType npcType) {
            var terrariaNpc = new NPC {netID = (int)npcType};
            var npc = new OrionNpc(terrariaNpc);

            npc.Type.Should().Be(npcType);
        }

        [Theory]
        [InlineData(100)]
        public void GetHealth_IsCorrect(int health) {
            var terrariaNpc = new NPC {life = health};
            var npc = new OrionNpc(terrariaNpc);

            npc.Health.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void SetHealth_IsCorrect(int health) {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.Health = health;

            terrariaNpc.life.Should().Be(health);
        }

        [Theory]
        [InlineData(100)]
        public void GetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaNpc = new NPC {lifeMax = maxHealth};
            var npc = new OrionNpc(terrariaNpc);

            npc.MaxHealth.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(100)]
        public void SetMaxHealth_IsCorrect(int maxHealth) {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.MaxHealth = maxHealth;

            terrariaNpc.lifeMax.Should().Be(maxHealth);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime)]
        public void ApplyType_IsCorrect(NpcType npcType) {
            var terrariaNpc = new NPC();
            var npc = new OrionNpc(terrariaNpc);

            npc.ApplyType(npcType);

            terrariaNpc.netID.Should().Be((int)npcType);
        }

        [Theory]
        [InlineData(NpcType.BlueSlime, NpcType.GreenSlime)]
        [InlineData(NpcType.BlueSlime, NpcType.None)]
        public void Transform_IsCorrect(NpcType oldType, NpcType newType) {
            // TargetClosest requires non-null player array.
            for (var i = 0; i < Main.maxPlayers; ++i) {
                Main.player[i] = new Player();
            }

            var terrariaNpc = new NPC();
            terrariaNpc.SetDefaults((int)oldType);
            var npc = new OrionNpc(terrariaNpc);

            npc.Transform(newType);

            terrariaNpc.netID.Should().Be((int)newType);
        }
    }
}
