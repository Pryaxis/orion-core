using System;
using FluentAssertions;
using Moq;
using Orion.Items;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDroppedLootItemEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            var mockItem = new Mock<IItem>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(null, mockItem.Object);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            var mockNpc = new Mock<INpc>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(mockNpc.Object, null);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
