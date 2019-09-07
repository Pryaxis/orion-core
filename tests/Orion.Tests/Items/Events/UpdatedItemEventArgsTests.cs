using System;
using FluentAssertions;
using Moq;
using Orion.Items;
using Orion.Items.Events;
using Xunit;

namespace Orion.Tests.Items.Events {
    public class UpdatedItemEventArgsTests {
        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            Func<UpdatedItemEventArgs> func = () => new UpdatedItemEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var item = new Mock<IItem>().Object;
            var args = new UpdatedItemEventArgs(item);

            args.Item.Should().BeSameAs(item);
        }
    }
}
