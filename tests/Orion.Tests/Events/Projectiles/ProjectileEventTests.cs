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
using FluentAssertions;
using Moq;
using Orion.Projectiles;
using Xunit;

namespace Orion.Events.Projectiles {
    public class ProjectileEventTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileEvent> func = () => new TestProjectileEvent(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Projectile_Get() {
            var projectile = new Mock<IProjectile>().Object;
            var args = new TestProjectileEvent(projectile);

            args.Projectile.Should().BeSameAs(projectile);
        }

        private class TestProjectileEvent : ProjectileEvent {
            public TestProjectileEvent(IProjectile projectile) : base(projectile) { }
        }
    }
}
