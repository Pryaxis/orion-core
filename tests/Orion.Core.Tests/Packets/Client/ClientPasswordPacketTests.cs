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
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Packets.Client
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ClientPasswordPacketTests
    {
        private readonly byte[] _bytes = { 12, 0, 38, 8, 84, 101, 114, 114, 97, 114, 105, 97 };

        [Fact]
        public void Password_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new ClientPasswordPacket();

            Assert.Throws<ArgumentNullException>(() => packet.Password = null!);
        }

        [Fact]
        public void Password_Set_Get()
        {
            var packet = new ClientPasswordPacket();

            packet.Password = "Terraria";

            Assert.Equal("Terraria", packet.Password);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ClientPasswordPacket>(_bytes, PacketContext.Server);

            Assert.Equal("Terraria", packet.Password);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(_bytes, PacketContext.Server);
        }
    }
}
