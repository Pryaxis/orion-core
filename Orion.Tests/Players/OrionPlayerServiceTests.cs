using System;
using System.Linq;
using FluentAssertions;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Players {
    public class OrionPlayerServiceTests : IDisposable {
        private readonly IPlayerService _playerService;

        public OrionPlayerServiceTests() {
            for (var i = 0; i < Terraria.Main.player.Length; ++i) {
                Terraria.Main.player[i] = new Terraria.Player {whoAmI = i};
            }
            
            _playerService = new OrionPlayerService();
        }

        public void Dispose() {
            _playerService.Dispose();
        }

        [Fact]
        public void GetCount_IsCorrect() {
            _playerService.Count.Should().Be(Terraria.Main.player.Length);
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var player = _playerService[0];

            player.WrappedEntity.Should().BeSameAs(Terraria.Main.player[0]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var player = _playerService[0];
            var player2 = _playerService[0];

            player.Should().BeSameAs(player2);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var players = _playerService.ToList();

            for (var i = 0; i < players.Count; ++i) {
                players[i].WrappedEntity.Should().BeSameAs(Terraria.Main.player[i]);
            }
        }
    }
}
