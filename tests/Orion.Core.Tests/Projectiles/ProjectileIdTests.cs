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

using System;
using System.Linq;
using Xunit;

namespace Orion.Core.Projectiles {
    public class ProjectileIdTests {
        [Fact]
        public void AllProjectileIdsCovered() {
            var maxId = Enum.GetValues(typeof(ProjectileId)).Cast<ProjectileId>().Max();
            Assert.Equal((ProjectileId)(Terraria.ID.ProjectileID.Count - 1), maxId);
        }
    }
}
