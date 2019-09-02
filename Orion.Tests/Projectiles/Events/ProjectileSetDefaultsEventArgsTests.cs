using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileSetDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileSetDefaultsEventArgs> func = () => new ProjectileSetDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
