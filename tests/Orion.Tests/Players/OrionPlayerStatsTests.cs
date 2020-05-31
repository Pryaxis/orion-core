// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Players {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionPlayerStatsTests {
        [Fact]
        public void Health_Get() {
            var terrariaPlayer = new Terraria.Player { statLife = 100 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            Assert.Equal(100, playerStats.Health);
        }

        [Fact]
        public void Health_Set() {
            var terrariaPlayer = new Terraria.Player();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Health = 100;

            Assert.Equal(100, terrariaPlayer.statLife);
        }

        [Fact]
        public void MaxHealth_Get() {
            var terrariaPlayer = new Terraria.Player { statLifeMax = 500 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            Assert.Equal(500, playerStats.MaxHealth);
        }

        [Fact]
        public void MaxHealth_Set() {
            var terrariaPlayer = new Terraria.Player();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxHealth = 500;

            Assert.Equal(500, terrariaPlayer.statLifeMax);
        }

        [Fact]
        public void Mana_Get() {
            var terrariaPlayer = new Terraria.Player { statMana = 100 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            Assert.Equal(100, playerStats.Mana);
        }

        [Fact]
        public void Mana_Set() {
            var terrariaPlayer = new Terraria.Player();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.Mana = 100;

            Assert.Equal(100, terrariaPlayer.statMana);
        }

        [Fact]
        public void MaxMana_Get() {
            var terrariaPlayer = new Terraria.Player { statManaMax = 200 };
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            Assert.Equal(200, playerStats.MaxMana);
        }

        [Fact]
        public void MaxMana_Set() {
            var terrariaPlayer = new Terraria.Player();
            var playerStats = new OrionPlayerStats(terrariaPlayer);

            playerStats.MaxMana = 200;

            Assert.Equal(200, terrariaPlayer.statManaMax);
        }
    }
}
