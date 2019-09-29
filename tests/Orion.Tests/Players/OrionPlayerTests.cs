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
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionPlayerTests {
        [Fact]
        public void Name_Get_IsCorrect() {
            var terrariaPlayer = new TerrariaPlayer {name = "test"};
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set_IsCorrect() {
            var terrariaPlayer = new TerrariaPlayer();
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Name = "test";

            terrariaPlayer.name.Should().Be("test");
        }

        [Fact]
        public void Name_Set_NullValue_ThrowsArgumentNullException() {
            var terrariaPlayer = new TerrariaPlayer();
            IPlayer player = new OrionPlayer(terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Team_Get_IsCorrect() {
            var terrariaPlayer = new TerrariaPlayer {team = (int)PlayerTeam.Red};
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Team.Should().Be(PlayerTeam.Red);
        }

        [Fact]
        public void Team_Set_IsCorrect() {
            var terrariaPlayer = new TerrariaPlayer();
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Team = PlayerTeam.Red;

            terrariaPlayer.team.Should().Be((int)PlayerTeam.Red);
        }
    }
}
