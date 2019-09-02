using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileRemovedEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileRemovedEventArgs> func = () => new ProjectileRemovedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
