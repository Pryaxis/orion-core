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
using Orion.Networking;
using Terraria;
using Xunit;

namespace Orion.Tests.Networking {
    public class OrionClientTests {
        [Theory]
        [InlineData(1)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaClient = new RemoteClient {Id = index};
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Index.Should().Be(index);
        }

        [Fact]
        public void GetIsConnected_False_IsCorrect() {
            var terrariaClient = new RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.IsConnected.Should().BeFalse();
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaClient = new RemoteClient {Name = name};
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaClient = new RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Name = name;

            terrariaClient.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaClient = new RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);
            Action action = () => client.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendPacket_NullPacket_ThrowsArgumentNullException() {
            var terrariaClient = new RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);
            Action action = () => client.SendPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
