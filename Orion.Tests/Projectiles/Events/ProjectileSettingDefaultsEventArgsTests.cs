using System;
using FluentAssertions;
using Orion.Projectiles;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class ProjectileSettingDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<ProjectileSettingDefaultsEventArgs> func = () =>
                new ProjectileSettingDefaultsEventArgs(null, ProjectileType.None);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
