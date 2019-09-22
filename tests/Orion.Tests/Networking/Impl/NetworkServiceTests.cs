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
using FluentAssertions;
using Microsoft.Xna.Framework;
using Moq;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Entities;
using Orion.Networking.Packets.Entities;
using Xunit;

namespace Orion.Networking.Impl {
    [Collection("TerrariaTestsCollection")]
    public class NetworkServiceTests : IDisposable {
        private readonly Mock<IPlayerService> _mockPlayerService = new Mock<IPlayerService>();
        private readonly NetworkService _networkService;

        public NetworkServiceTests() {
            _networkService = new NetworkService(new Lazy<IPlayerService>(() => _mockPlayerService.Object));
        }

        public void Dispose() {
            _networkService.Dispose();
        }

        [Fact]
        public void PacketReceive_IsTriggered() {
            var player = new Mock<IPlayer>().Object;
            _mockPlayerService.Setup(ps => ps[1]).Returns(player);

            var isRun = false;
            _networkService.PacketReceive += (sender, args) => {
                isRun = true;
                args.Sender.Should().BeSameAs(player);
            };

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerConnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void PacketReceive_PlayerConnect_IsTriggered() {
            var player = new Mock<IPlayer>().Object;
            _mockPlayerService.Setup(ps => ps[1]).Returns(player);

            var isRun = false;
            _mockPlayerService.Setup(ps => ps.PlayerConnect).Returns(
                new EventHandlerCollection<PlayerConnectEventArgs>((sender, args) => {
                    isRun = true;
                    args.Player.Should().BeSameAs(player);
                    args.PlayerVersionString.Should().Be("Terraria194");
                }));

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerConnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void PacketReceive_PlayerData_IsTriggered() {
            var player = new Mock<IPlayer>().Object;
            _mockPlayerService.Setup(ps => ps[1]).Returns(player);

            var isRun = false;
            _mockPlayerService.Setup(ps => ps.PlayerData).Returns(
                new EventHandlerCollection<PlayerDataEventArgs>((sender, args) => {
                    isRun = true;
                    args.Player.Should().BeSameAs(player);
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
                }));

            TestUtils.FakeReceiveBytes(1, PlayerDataPacketTests.Bytes);

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerData);
            _mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void ResetClient_PlayerDisconnect_IsTriggered() {
            var player = new Mock<IPlayer>().Object;
            _mockPlayerService.Setup(ps => ps[1]).Returns(player);

            var isRun = false;
            _mockPlayerService.Setup(ps => ps.PlayerDisconnect).Returns(
                new EventHandlerCollection<PlayerDisconnectEventArgs>((sender, args) => {
                    isRun = true;
                    args.Player.Should().BeSameAs(player);
                }));
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerDisconnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }
    }
}
