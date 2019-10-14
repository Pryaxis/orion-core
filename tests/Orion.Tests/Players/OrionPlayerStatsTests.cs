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

using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionPlayerStatsTests {
        [Fact]
        public void Health_Get() {
            var terrariaPlayer = new TerrariaPlayer { statLife = 105 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Health.Should().Be(105);
        }

        [Fact]
        public void Health_Set() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Health = 650;

            terrariaPlayer.statLife.Should().Be(650);
        }
        
        [Fact]
        public void MaxHealth_Get() {
            var terrariaPlayer = new TerrariaPlayer { statLifeMax = 105 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxHealth.Should().Be(105);
        }

        [Fact]
        public void MaxHealth_Set() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxHealth = 650;

            terrariaPlayer.statLifeMax.Should().Be(650);
        }

        [Fact]
        public void Mana_Get() {
            var terrariaPlayer = new TerrariaPlayer { statMana = 67 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Mana.Should().Be(67);
        }

        [Fact]
        public void Mana_Set() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Mana = 4515;

            terrariaPlayer.statMana.Should().Be(4515);
        }

        [Fact]
        public void MaxMana_Get() {
            var terrariaPlayer = new TerrariaPlayer { statManaMax = 678 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxMana.Should().Be(678);
        }

        [Fact]
        public void MaxMana_Set() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxMana = 1567;

            terrariaPlayer.statManaMax.Should().Be(1567);
        }
    }
}
