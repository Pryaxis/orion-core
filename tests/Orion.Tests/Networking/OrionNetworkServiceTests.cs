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
