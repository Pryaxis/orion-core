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
using Moq;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Players;
using Orion.Networking.Packets.Players;
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
            var playerConnect = new EventHandlerCollection<PlayerConnectEventArgs>((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(player);
                args.PlayerVersionString.Should().Be("Terraria194");
            });
            _mockPlayerService.Setup(ps => ps.PlayerConnect).Returns(playerConnect);

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerConnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void ResetClient_PlayerDisconnect_IsTriggered() {
            var player = new Mock<IPlayer>().Object;
            _mockPlayerService.Setup(ps => ps[1]).Returns(player);

            var isRun = false;
            var playerDisconnect = new EventHandlerCollection<PlayerDisconnectEventArgs>((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(player);
            });
            _mockPlayerService.Setup(ps => ps.PlayerDisconnect).Returns(playerDisconnect);
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerDisconnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }
    }
}
