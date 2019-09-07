using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class SetNpcDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<SetNpcDefaultsEventArgs> func = () => new SetNpcDefaultsEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new SetNpcDefaultsEventArgs(npc);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}
