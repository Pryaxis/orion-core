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
using FluentAssertions;
using Moq;
using Orion.Packets.Players;
using Orion.Utils;
using Xunit;

namespace Orion.Players.Extensions {
    public class PlayerServiceExtensionsTests {
        [Fact]
        public void BroadcastPacket_IsCorrect() {
            var packet = new PlayerDisconnectPacket();
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer.Setup(p => p.SendPacket(packet));
            var mockPlayers = new Mock<IReadOnlyArray<IPlayer>>();
            mockPlayers.SetupGet(p => p.Count).Returns(1);
            mockPlayers.SetupGet(p => p[0]).Returns(mockPlayer.Object);
            var mockPlayerService = new Mock<IPlayerService>();
            mockPlayerService.Setup(p => p.Players).Returns(mockPlayers.Object);

            mockPlayerService.Object.BroadcastPacket(packet);

            mockPlayer.Verify(p => p.SendPacket(packet));
            mockPlayer.VerifyNoOtherCalls();
            mockPlayers.VerifyGet(p => p.Count);
            mockPlayers.VerifyGet(p => p[0]);
            mockPlayers.VerifyNoOtherCalls();
            mockPlayerService.VerifyGet(ps => ps.Players);
            mockPlayerService.VerifyNoOtherCalls();
        }

        [Fact]
        public void BroadcastPacket_NullPlayerService_ThrowsArgumentNullException() {
            var packet = new PlayerDisconnectPacket();
            Action action = () => PlayerServiceExtensions.BroadcastPacket(null, packet);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BroadcastPacket_NullPacket_ThrowsArgumentNullException() {
            var playerService = new Mock<IPlayerService>().Object;
            Action action = () => playerService.BroadcastPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
