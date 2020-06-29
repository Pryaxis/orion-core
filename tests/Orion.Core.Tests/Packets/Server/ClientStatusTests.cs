// Copyright (c) 2020 Pryaxis & Orion Contributors
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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.Packets.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Server
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ClientStatusTests
    {
        private readonly byte[] _bytes =
        {
            29, 0, 9, 15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52,
            52, 0, 0
        };

        [Fact]
        public void OutstandingPackets_Set_Get()
        {
            var packet = new ClientStatus();

            packet.OutstandingPackets = 15;

            Assert.Equal(15, packet.OutstandingPackets);
        }

        [Fact]
        public void StatusText_GetNullValue()
        {
            var packet = new ClientStatus();

            Assert.Equal(NetworkText.Empty, packet.StatusText);
        }

        [Fact]
        public void StatusText_Set_Get()
        {
            var packet = new ClientStatus();

            packet.StatusText = new NetworkText(NetworkTextMode.Localized, "LegacyInterface.44");

            Assert.Equal(new NetworkText(NetworkTextMode.Localized, "LegacyInterface.44"), packet.StatusText);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HidePercentage_Set_Get(bool value)
        {
            var packet = new ClientStatus();

            packet.HidePercentage = value;

            Assert.Equal(value, packet.HidePercentage);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasShadows_Set_Get(bool value)
        {
            var packet = new ClientStatus();

            packet.HasShadows = value;

            Assert.Equal(value, packet.HasShadows);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ClientStatus>(_bytes, PacketContext.Server);

            Assert.Equal(15, packet.OutstandingPackets);
            Assert.Equal(new NetworkText(NetworkTextMode.Localized, "LegacyInterface.44"), packet.StatusText);
            Assert.False(packet.HidePercentage);
            Assert.False(packet.HasShadows);
        }
    }
}
