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

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.PlayerConnectBytes);

            isRun.Should().BeTrue();
        }
    }
}
