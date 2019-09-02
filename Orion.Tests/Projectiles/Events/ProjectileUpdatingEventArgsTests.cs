using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileUpdatingEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileUpdatingEventArgs> func = () => new ProjectileUpdatingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
