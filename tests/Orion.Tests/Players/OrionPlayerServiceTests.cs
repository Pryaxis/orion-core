// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using Orion.Events;
using Orion.Items;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Serilog.Core;
using Xunit;
using Main = Terraria.Main;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class OrionPlayerServiceTests {
        public OrionPlayerServiceTests() {
            for (var i = 0; i < Main.player.Length; ++i) {
                Main.player[i] = new TerrariaPlayer { whoAmI = i };
            }
        }

        [Fact]
        public void Players_Item_Get() {
            using var playerService = new OrionPlayerService(Logger.None);
            var player = playerService.Players[1];

            player.Index.Should().Be(1);
            ((OrionPlayer)player).Wrapped.Should().BeSameAs(Main.player[1]);
        }

        [Fact]
        public void Players_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var playerService = new OrionPlayerService(Logger.None);
            var player = playerService.Players[0];
            var player2 = playerService.Players[0];

            player.Should().BeSameAs(player2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Players_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var playerService = new OrionPlayerService(Logger.None);
            Func<IPlayer> func = () => playerService.Players[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Players_GetEnumerator() {
            using var playerService = new OrionPlayerService(Logger.None);
            var players = playerService.Players.ToList();

            for (var i = 0; i < players.Count; ++i) {
                ((OrionPlayer)players[i]).Wrapped.Should().BeSameAs(Main.player[i]);
            }
        }

        [Fact]
        public void PacketReceive_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PacketReceive.RegisterHandler((sender, args) => {
                isRun = true;
                args.Sender.Should().BeSameAs(playerService.Players[1]);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerConnect_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerConnect.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.PlayerVersionString.Should().Be("Terraria194");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerData_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerData.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.PlayerSkinType.Should().Be(2);
                args.PlayerHairType.Should().Be(50);
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
                args.PlayerHasExtraAccessory.Should().BeFalse();
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerDataPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInventorySlot_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerInventorySlot.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.PlayerInventorySlotIndex.Should().Be(0);
                args.ItemStackSize.Should().Be(1);
                args.ItemPrefix.Should().Be(ItemPrefix.Godly);
                args.ItemType.Should().Be(ItemType.CopperShortsword);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerInventorySlotPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerJoin_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerJoin.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerJoinPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerChat_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerChat.RegisterHandler((sender, args) => {
                isRun = true;
                args.ChatCommand.Should().Be("Say");
                args.ChatText.Should().Be("/command test");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, ChatModuleTests.ClientBytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ResetClient_PlayerDisconnected_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerDisconnected.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
            });
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
        }
    }
}
