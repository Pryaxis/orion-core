using System;
using FluentAssertions;
using Moq;
using Orion.Networking;
using Xunit;

namespace Orion.Tests.Networking {
    public class OrionClientTests {
        [Theory]
        [InlineData(1)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaClient = new Terraria.RemoteClient {Id = index};
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Index.Should().Be(index);
        }

        [Fact]
        public void GetIsConnected_False_IsCorrect() {
            var terrariaClient = new Terraria.RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.IsConnected.Should().BeFalse();
        }

        [Theory]
        [InlineData("test")]
        public void GetName_IsCorrect(string name) {
            var terrariaClient = new Terraria.RemoteClient {Name = name};
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("test")]
        public void SetName_IsCorrect(string name) {
            var terrariaClient = new Terraria.RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);

            client.Name = name;

            terrariaClient.Name.Should().Be(name);
        }

        [Fact]
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaClient = new Terraria.RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);
            Action action = () => client.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SendPacket_NullPacket_ThrowsArgumentNullException() {
            var terrariaClient = new Terraria.RemoteClient();
            var client = new OrionClient(new Mock<INetworkService>().Object, terrariaClient);
            Action action = () => client.SendPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
