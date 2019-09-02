using System;
using FluentAssertions;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<IItem> func = () => new OrionItem(null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
