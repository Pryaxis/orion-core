// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Linq;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Terraria;
using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class OrionPlayerServiceTests : IDisposable {
        private readonly Mock<INetworkService> _networkService = new Mock<INetworkService>();
        private readonly IPlayerService _playerService;

        public OrionPlayerServiceTests() {
            for (var i = 0; i < Main.maxPlayers + 1; ++i) {
                Main.player[i] = new Player {whoAmI = i};
            }

            Main.motd = "test";

            _playerService = new OrionPlayerService(new Lazy<INetworkService>(() => _networkService.Object));
        }

        public void Dispose() {
            _playerService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var player = (OrionPlayer)_playerService[0];

            player.Wrapped.Should().BeSameAs(Main.player[0]);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            var player = _playerService[0];
            var player2 = _playerService[0];

            player.Should().BeSameAs(player2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<IPlayer> func = () => _playerService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void PlayerUpdating_IsCorrect() {
            OrionPlayer argsPlayer = null;
            _playerService.UpdatingPlayer += (sender, args) => {
                argsPlayer = (OrionPlayer)args.Player;
            };
            var player = (OrionPlayer)_playerService[0];

            player.Wrapped.Update(player.Index);

            argsPlayer.Should().NotBeNull();
            argsPlayer.Wrapped.Should().BeSameAs(player.Wrapped);
        }

        [Fact]
        public void PlayerUpdated_IsCorrect() {
            OrionPlayer argsPlayer = null;
            _playerService.UpdatedPlayer += (sender, args) => {
                argsPlayer = (OrionPlayer)args.Player;
            };
            var player = (OrionPlayer)_playerService[0];

            player.Wrapped.Update(player.Index);

            argsPlayer.Should().NotBeNull();
            argsPlayer.Wrapped.Should().BeSameAs(player.Wrapped);
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var players = _playerService.ToList();

            for (var i = 0; i < players.Count; ++i) {
                ((OrionPlayer)players[i]).Wrapped.Should().BeSameAs(Main.player[i]);
            }
        }
    }
}
