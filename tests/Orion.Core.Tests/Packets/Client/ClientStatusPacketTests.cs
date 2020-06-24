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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Orion.Core.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Client
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ClientStatusPacketTests
    {
        private readonly byte[] _bytes =
        {
            29, 0, 9, 15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52,
            52, 0, 0
        };

        [Fact]
        public void MaxStatus_Set_Get()
        {
            var packet = new ClientStatusPacket();

            packet.MaxStatus = 15;

            Assert.Equal(15, packet.MaxStatus);
        }

        [Fact]
        public void StatusText_Set_Get()
        {
            var packet = new ClientStatusPacket();

            packet.StatusText = NetworkText.Localized("LegacyInterface.44");

            Assert.Equal(NetworkText.Localized("LegacyInterface.44"), packet.StatusText);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HidePercentage_Set_Get(bool value)
        {
            var packet = new ClientStatusPacket();

            packet.HidePercentage = value;

            Assert.Equal(value, packet.HidePercentage);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasShadows_Set_Get(bool value)
        {
            var packet = new ClientStatusPacket();

            packet.HasShadows = value;

            Assert.Equal(value, packet.HasShadows);
        }

        [Fact]
        public void Read()
        {
            var packet = new ClientStatusPacket();
            var span = _bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(15, packet.MaxStatus);
            Assert.Equal(NetworkText.Localized("LegacyInterface.44"), packet.StatusText);
            Assert.False(packet.HidePercentage);
            Assert.False(packet.HasShadows);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<ClientStatusPacket>(_bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
