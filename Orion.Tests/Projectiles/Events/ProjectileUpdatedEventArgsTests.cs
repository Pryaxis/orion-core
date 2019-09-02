using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileUpdatedEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileUpdatedEventArgs> func = () => new ProjectileUpdatedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
