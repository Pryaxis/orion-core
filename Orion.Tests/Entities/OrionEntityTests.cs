namespace Orion.Tests.Entities {
    using System;
    using FluentAssertions;
    using Orion.Entities;
    using Xunit;

    public class OrionEntityTests {
        [Fact]
        public void Ctor_NullEntity_ThrowsArgumentNullException() {
            Func<OrionEntity> func = () => new OrionEntity(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
