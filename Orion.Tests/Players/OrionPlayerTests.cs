using System;
using FluentAssertions;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Players {
    public class OrionPlayerTests {
        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaPlayer = new Terraria.Player();
            var player = new OrionPlayer(terrariaPlayer);
            Action action = () => player.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
