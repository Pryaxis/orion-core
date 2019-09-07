using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcDroppingLootItemEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcDroppingLootItemEventArgs> func = () => new NpcDroppingLootItemEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcDroppingLootItemEventArgs(npc);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}
