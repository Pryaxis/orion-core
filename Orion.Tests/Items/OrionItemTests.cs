using System;
using FluentAssertions;
using Orion.Items;
using Xunit;

namespace Orion.Tests.Items {
    public class OrionItemTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaItem = new Terraria.Item();
            var item = new OrionItem(terrariaItem);
            Action action = () => item.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
