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
using Xunit;

namespace Orion.Players {
    public class OrionPlayerTests {
        [Fact]
        public void GetName_IsCorrect() {
            var terrariaPlayer = new Terraria.Player {name = "test"};
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Name.Should().Be("test");
        }

        [Fact]
        public void SetName_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Name = "test";

            terrariaPlayer.name.Should().Be("test");
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaPlayer = new Terraria.Player();
            IPlayer player = new OrionPlayer(terrariaPlayer);
            Action action = () => player.Name = null!;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetTeam_IsCorrect() {
            var terrariaPlayer = new Terraria.Player {team = (int)PlayerTeam.Red};
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Team.Should().Be(PlayerTeam.Red);
        }

        [Fact]
        public void SetTeam_IsCorrect() {
            var terrariaPlayer = new Terraria.Player();
            IPlayer player = new OrionPlayer(terrariaPlayer);

            player.Team = PlayerTeam.Red;

            terrariaPlayer.team.Should().Be((int)PlayerTeam.Red);
        }
    }
}
