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
using Orion.Events;
using Orion.Events.Players;
using Orion.Networking;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Networking {
    [Collection("TerrariaTests")]
    public class OrionNetworkServiceTests : IDisposable {
        private readonly Mock<IPlayerService> _mockPlayerService = new Mock<IPlayerService>();
        private readonly OrionNetworkService _networkService;

        public OrionNetworkServiceTests() {
            _networkService = new OrionNetworkService(new Lazy<IPlayerService>(() => _mockPlayerService.Object));

            for (var i = 0; i < Terraria.NetMessage.buffer.Length; ++i) {
                Terraria.NetMessage.buffer[i] = new Terraria.MessageBuffer {whoAmI = i};
            }

            for (var i = 0; i < Terraria.Netplay.Clients.Length; ++i) {
                Terraria.Netplay.Clients[i] = new Terraria.RemoteClient {Id = i};
            }
        }

        public void Dispose() {
            _networkService.Dispose();
        }

        [Fact]
        public void BroadcastPacket_NullPacket_ThrowsArgumentNullException() {
            Action action = () => _networkService.BroadcastPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Disconnect_TriggersPlayerDisconnect() {
            var mockPlayer = new Mock<IPlayer>();
            var playerDisconnectCalled = false;
            var playerDisconnect = new EventHandlerCollection<PlayerDisconnectEventArgs>((sender, args) => {
                playerDisconnectCalled = true;

                args.Player.Should().BeSameAs(mockPlayer.Object);
            });

            _mockPlayerService.Setup(ps => ps[1]).Returns(mockPlayer.Object);
            _mockPlayerService.Setup(ps => ps.PlayerDisconnect).Returns(playerDisconnect);

            Terraria.Netplay.Clients[1].IsActive = true;
            Terraria.Netplay.Clients[1].Reset();

            playerDisconnectCalled.Should().BeTrue();
            _mockPlayerService.VerifyGet(ps => ps[1]);
            _mockPlayerService.VerifyGet(ps => ps.PlayerDisconnect);
            _mockPlayerService.VerifyNoOtherCalls();
        }
    }
}
