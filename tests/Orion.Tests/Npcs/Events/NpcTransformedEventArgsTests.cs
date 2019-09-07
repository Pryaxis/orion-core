using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcTransformedEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcTransformedEventArgs> func = () => new NpcTransformedEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new NpcTransformedEventArgs(npc);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}
