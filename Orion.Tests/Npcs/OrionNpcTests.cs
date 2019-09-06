using System;
using FluentAssertions;
using Orion.Npcs;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaNpc = new Terraria.NPC();
            var npc = new OrionNpc(terrariaNpc);
            Action action = () => npc.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
