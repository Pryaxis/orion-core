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
using Orion.World.Tiles;
using Xunit;

namespace Orion.Projectiles {
    [Collection("TerrariaTestsCollection")]
    public class OrionProjectileTests {
        /*
         * TODO: this test requires language localization, which fails for .NET core projects currently
         *
        [Fact(Skip = "Localization")]
        public void GetName_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile {type = (int)ProjectileType.WoodenArrow};
            IProjectile projectile = new OrionProjectile(terrariaProjectile);

            projectile.Name.Should().Be("Wooden Arrow");
        }
        */

        [Fact]
        public void SetName_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile();
            IProjectile projectile = new OrionProjectile(terrariaProjectile);

            projectile.Name = "test";

            projectile.Name.Should().Be("test");
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaProjectile = new Terraria.Projectile();
            IProjectile projectile = new OrionProjectile(terrariaProjectile);
            Action action = () => projectile.Name = null!;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetType_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile {type = (int)ProjectileType.CrystalBullet};
            IProjectile projectile = new OrionProjectile(terrariaProjectile);

            projectile.Type.Should().Be(ProjectileType.CrystalBullet);
        }

        [Fact]
        public void SetType_IsCorrect() {
            var terrariaProjectile = new Terraria.Projectile();
            IProjectile projectile = new OrionProjectile(terrariaProjectile);

            projectile.SetType(ProjectileType.CrystalBullet);

            terrariaProjectile.type.Should().Be((int)ProjectileType.CrystalBullet);
        }

        public Tile[] tiles = new Tile[10];

        private ref Tile GetTile(int index) => ref tiles[index];

        [Fact]
        public void Test() {
            GetTile(0).LiquidAmount = 255;

            tiles[0].LiquidAmount.Should().Be(255);
        }
    }
}
