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
    public class ClientUuidTests
    {
        private readonly byte[] _bytes = { 12, 0, 68, 8, 84, 101, 114, 114, 97, 114, 105, 97 };

        [Fact]
        public void Uuid_GetNullValue()
        {
            var packet = new ClientUuid();

            Assert.Equal(string.Empty, packet.Uuid);
        }

        [Fact]
        public void Uuid_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new ClientUuid();

            Assert.Throws<ArgumentNullException>(() => packet.Uuid = null!);
        }

        [Fact]
        public void Uuid_Set_Get()
        {
            var packet = new ClientUuid();

            packet.Uuid = "Terraria";

            Assert.Equal("Terraria", packet.Uuid);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ClientUuid>(_bytes, PacketContext.Server);

            Assert.Equal("Terraria", packet.Uuid);
        }
    }
}
