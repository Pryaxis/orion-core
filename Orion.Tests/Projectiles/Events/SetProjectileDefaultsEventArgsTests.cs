using System;
using FluentAssertions;
using Orion.Projectiles.Events;
using Xunit;

namespace Orion.Tests.Projectiles.Events {
    public class SetProjectileDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullProjectile_ThrowsArgumentNullException() {
            Func<SetProjectileDefaultsEventArgs> func = () => new SetProjectileDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
