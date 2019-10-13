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
using TerrariaProjectile = Terraria.Projectile;

namespace Orion.Projectiles {
    [Collection("TerrariaTestsCollection")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionProjectileTests {
        [Fact]
        public void Name_Set() {
            var terrariaProjectile = new TerrariaProjectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Name = "test";

            projectile.Name.Should().Be("test");
        }

        [Fact]
        public void Name_Set_NullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new TerrariaProjectile();
            var projectile = new OrionProjectile(terrariaProjectile);
            Action action = () => projectile.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Type_Get() {
            var terrariaProjectile = new TerrariaProjectile { type = (int)ProjectileType.CrystalBullet };
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Type.Should().Be(ProjectileType.CrystalBullet);
        }

        [Fact]
        public void Type_Set() {
            var terrariaProjectile = new TerrariaProjectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.SetType(ProjectileType.CrystalBullet);

            terrariaProjectile.type.Should().Be((int)ProjectileType.CrystalBullet);
        }
    }
}
