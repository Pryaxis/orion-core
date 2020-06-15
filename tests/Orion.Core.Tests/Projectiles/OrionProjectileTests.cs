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
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Projectiles {
    // These tests depend on Terraria state.
    [Collection("TerrariaTestsCollection")]
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionProjectileTests {
        [Fact]
        public void Name_Get() {
            var terrariaProjectile = new Terraria.Projectile { type = (int)ProjectileId.WoodenArrow };
            var projectile = new OrionProjectile(terrariaProjectile);

            Assert.Equal("Wooden Arrow", projectile.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            Assert.Throws<ArgumentNullException>(() => projectile.Name = null!);
        }

        [Fact]
        public void Name_Set_Get() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.Name = "test";

            Assert.Equal("test", projectile.Name);
        }

        [Fact]
        public void Id_Get() {
            var terrariaProjectile = new Terraria.Projectile { type = (int)ProjectileId.CrystalBullet };
            var projectile = new OrionProjectile(terrariaProjectile);

            Assert.Equal(ProjectileId.CrystalBullet, projectile.Id);
        }

        [Fact]
        public void SetId() {
            var terrariaProjectile = new Terraria.Projectile();
            var projectile = new OrionProjectile(terrariaProjectile);

            projectile.SetId(ProjectileId.CrystalBullet);

            Assert.Equal(ProjectileId.CrystalBullet, (ProjectileId)terrariaProjectile.type);
        }
    }
}
