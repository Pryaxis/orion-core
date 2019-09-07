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
            var item = new Mock<IItem>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(null, item.Object);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_NullItem_ThrowsArgumentNullException() {
            var npc = new Mock<INpc>();
            Func<NpcDroppedLootItemEventArgs> func = () => new NpcDroppedLootItemEventArgs(npc.Object, null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var item = new Mock<IItem>().Object;
            var args = new NpcDroppedLootItemEventArgs(npc, item);

            args.Npc.Should().BeSameAs(npc);
        }

        [Fact]
        public void GetLootItem_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var item = new Mock<IItem>().Object;
            var args = new NpcDroppedLootItemEventArgs(npc, item);

            args.LootItem.Should().BeSameAs(item);
        }
    }
}
