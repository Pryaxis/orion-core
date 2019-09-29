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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Npcs {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionNpcTests {
        [Fact]
        public void GetName_IsCorrect() {
            var terrariaNpc = new TerrariaNpc {_givenName = "test"};
            INpc npc = new OrionNpc(terrariaNpc);

            npc.Name.Should().Be("test");
        }

        [Fact]
        public void SetName_IsCorrect() {
            var terrariaNpc = new TerrariaNpc();
            INpc npc = new OrionNpc(terrariaNpc);

            npc.Name = "test";

            terrariaNpc.GivenOrTypeName.Should().Be("test");
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaNpc = new TerrariaNpc();
            INpc npc = new OrionNpc(terrariaNpc);
            Action action = () => npc.Name = null!;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetType_IsCorrect() {
            var terrariaNpc = new TerrariaNpc {netID = (int)NpcType.BlueSlime};
            INpc npc = new OrionNpc(terrariaNpc);

            npc.Type.Should().Be(NpcType.BlueSlime);
        }

        [Fact]
        public void SetType_IsCorrect() {
            var terrariaNpc = new TerrariaNpc();
            INpc npc = new OrionNpc(terrariaNpc);

            npc.SetType(NpcType.BlueSlime);

            terrariaNpc.type.Should().Be((int)NpcType.BlueSlime);
        }
    }
}
