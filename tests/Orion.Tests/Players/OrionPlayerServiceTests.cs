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
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Players;
using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
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
        public void GetItem_IsCorrect() {
            var player = (OrionPlayer)_playerService[0];

            player.Wrapped.Should().BeSameAs(Terraria.Main.player[0]);
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
        public void GetEnumerator_IsCorrect() {
            var players = _playerService.ToList();

            for (var i = 0; i < players.Count; ++i) {
                ((OrionPlayer)players[i]).Wrapped.Should().BeSameAs(Terraria.Main.player[i]);
            }
        }

        [Fact]
        public void PacketReceive_IsTriggered() {
            var isRun = false;
            _playerService.PacketReceive += (sender, args) => {
                isRun = true;
                args.Sender.Should().BeSameAs(_playerService[1]);
            };

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerConnect_IsTriggered() {
            var isRun = false;
            _playerService.PlayerConnect += (sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(_playerService[1]);
                args.PlayerVersionString.Should().Be("Terraria194");
            };

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerData_IsTriggered() {
            var isRun = false;
            _playerService.PlayerData += (sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(_playerService[1]);
                args.PlayerSkinType.Should().Be(2);
                args.PlayerName.Should().Be("f");
                args.PlayerHairDye.Should().Be(0);
                args.PlayerHiddenVisualsFlags.Should().Be(0);
                args.PlayerHiddenMiscFlags.Should().Be(0);
                args.PlayerHairColor.Should().Be(new Color(26, 131, 54));
                args.PlayerSkinColor.Should().Be(new Color(158, 74, 51));
                args.PlayerEyeColor.Should().Be(new Color(47, 39, 88));
                args.PlayerShirtColor.Should().Be(new Color(184, 58, 43));
                args.PlayerUndershirtColor.Should().Be(new Color(69, 8, 97));
                args.PlayerPantsColor.Should().Be(new Color(162, 167, 255));
                args.PlayerShoeColor.Should().Be(new Color(212, 159, 76));
                args.PlayerDifficulty.Should().Be(PlayerDifficulty.Softcore);
            };

            TestUtils.FakeReceiveBytes(1, PlayerDataPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInventorySlot_IsTriggered() {
            var isRun = false;
            _playerService.PlayerInventorySlot += (sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(_playerService[1]);
                args.PlayerInventorySlotIndex.Should().Be(0);
                args.ItemStackSize.Should().Be(1);
                args.ItemPrefix.Should().Be(ItemPrefix.Godly);
                args.ItemType.Should().Be(ItemType.CopperShortsword);
            };

            TestUtils.FakeReceiveBytes(1, PlayerInventorySlotPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerJoin_IsTriggered() {
            var isRun = false;
            _playerService.PlayerJoin += (sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(_playerService[1]);
            };

            TestUtils.FakeReceiveBytes(1, PlayerJoinPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ResetClient_PlayerDisconnected_IsTriggered() {
            var isRun = false;
            _playerService.PlayerDisconnected += (sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(_playerService[1]);
            };
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
        }
    }
}
