using System;
using FluentAssertions;
using Orion.Projectiles;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class SettingProjectileDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<SettingProjectileDefaultsEventArgs> func = () =>
                new SettingProjectileDefaultsEventArgs(null, ProjectileType.None);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
