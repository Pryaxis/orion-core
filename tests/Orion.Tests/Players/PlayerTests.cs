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
using Microsoft.Xna.Framework;
using Moq;
using Orion.Packets;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Packets.World;
using Xunit;

namespace Orion.Players {
    public class PlayerTests {
        [Fact]
        public void Disconnect() {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Object.Disconnect("test");

            mockPlayer.Verify(
                p => p.SendPacket(
                    It.Is<PlayerDisconnectPacket>(pdp => pdp.DisconnectReason == "test")));
            mockPlayer.VerifyNoOtherCalls();
        }

        [Fact]
        public void Disconnect_NullPlayer_ThrowsArgumentNullException() {
            Action action = () => PlayerExtensions.Disconnect(null, "");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Disconnect_NullReason_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Action action = () => player.Disconnect(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendMessage() {
            var mockPlayer = new Mock<IPlayer>();

            mockPlayer.Object.SendMessage("test", Color.White);

            mockPlayer.Verify(
                p => p.SendPacket(
                    It.Is<ChatPacket>(cp => cp.Color == Color.White && cp.Text == "test")));
        }

        [Fact]
        public void SendMessage_NullPlayer_ThrowsArgumentNullException() {
            Action action = () => PlayerExtensions.SendMessage(null, "", Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendMessage_NullMessage_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Action action = () => player.SendMessage(null, Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendMessageFrom() {
            var isRun = false;
            var mockPlayer = new Mock<IPlayer>();
            mockPlayer.Setup(p => p.SendPacket(It.IsAny<ModulePacket>())).Callback<Packet>(p => {
                isRun = true;

                var mp = p as ModulePacket;
                mp.Should().NotBeNull();

                var cm = mp.Module as ChatModule;
                cm.Should().NotBeNull();
                cm.ServerChatterIndex.Should().Be(123);
                cm.ServerText.Should().Be("test");
                cm.ServerColor.Should().Be(Color.White);
            });

            var fromMockPlayer = new Mock<IPlayer>();
            fromMockPlayer.SetupGet(p => p.Index).Returns(123);

            mockPlayer.Object.SendMessageFrom(fromMockPlayer.Object, "test", Color.White);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void SendMessageFrom_NullPlayer_ThrowsArgumentNullException() {
            var fromPlayer = new Mock<IPlayer>().Object;
            Action action = () => PlayerExtensions.SendMessageFrom(null, fromPlayer, "", Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendMessageFrom_NullFromPlayer_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            Action action = () => player.SendMessageFrom(null, "", Color.White);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendMessageFrom_NullMessage_ThrowsArgumentNullException() {
            var player = new Mock<IPlayer>().Object;
            var fromPlayer = new Mock<IPlayer>().Object;
            Action action = () => player.SendMessageFrom(fromPlayer, null, Color.White);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
