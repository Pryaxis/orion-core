using System;
using FluentAssertions;
using Orion.Networking;
using Xunit;

namespace Orion.Tests.Networking {
    [Collection("TerrariaTests")]
    public class OrionNetworkServiceTests : IDisposable {
        private readonly OrionNetworkService _networkService;

        public OrionNetworkServiceTests() {
            _networkService = new OrionNetworkService();
        }

        public void Dispose() {
            _networkService.Dispose();
        }

        [Fact]
        public void BroadcastPacket_NullPacket_ThrowsArgumentNullException() {
            Action action = () => _networkService.BroadcastPacket(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
