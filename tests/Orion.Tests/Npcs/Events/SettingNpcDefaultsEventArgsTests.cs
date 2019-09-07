using System;
using FluentAssertions;
using Moq;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class SettingNpcDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<SettingNpcDefaultsEventArgs> func = () => new SettingNpcDefaultsEventArgs(null, NpcType.None);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = new Mock<INpc>().Object;
            var args = new SettingNpcDefaultsEventArgs(npc, NpcType.None);

            args.Npc.Should().BeSameAs(npc);
        }
    }
}
