using System;
using FluentAssertions;
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
    }
}
