using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileRemovingEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileRemovingEventArgs> func = () => new ProjectileRemovingEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
