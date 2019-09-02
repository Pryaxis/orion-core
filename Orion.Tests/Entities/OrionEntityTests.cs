using System;
using FluentAssertions;
using Orion.Entities;
using Xunit;

namespace Orion.Tests.Entities {
   public class OrionEntityTests {
        [Fact]
        public void Ctor_NullEntity_ThrowsArgumentNullException() {
            Func<OrionEntity> func = () => new OrionEntity(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
