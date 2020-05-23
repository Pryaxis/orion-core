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

using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class PlayerTeamTests {
        [Fact]
        public void Color() {
            for (var i = 0; i < 6; ++i) {
                var color = ((PlayerTeam)i).Color();

                Assert.Equal(Terraria.Main.teamColor[i].R, color.R);
                Assert.Equal(Terraria.Main.teamColor[i].G, color.G);
                Assert.Equal(Terraria.Main.teamColor[i].B, color.B);
            }
        }
    }
}
