using System;
using FluentAssertions;
using Orion.Npcs;
using Orion.Npcs.Events;
using Xunit;

namespace Orion.Tests.Npcs.Events {
    public class NpcSettingDefaultsEventArgsTests {
        [Fact]
        public void Ctor_NullNpc_ThrowsArgumentNullException() {
            Func<NpcSettingDefaultsEventArgs> func = () => new NpcSettingDefaultsEventArgs(null, NpcType.None);

            func.Should().Throw<ArgumentNullException>();
        }
    }
}
