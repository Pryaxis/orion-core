using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class UpdatedNpcEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<UpdatedNpcEventArgs> func = () => new UpdatedNpcEventArgs(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new UpdatedNpcEventArgs(npc);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}
