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
    public class ClientConnectTests
    {
        private readonly byte[] _bytes = { 15, 0, 1, 11, 84, 101, 114, 114, 97, 114, 105, 97, 49, 57, 52 };

        [Fact]
        public void Version_GetNullValue()
        {
            var clientConnect = new ClientConnect();

            Assert.Equal(string.Empty, clientConnect.Version);
        }

        [Fact]
        public void Version_SetNullValue_ThrowsArgumentNullException()
        {
            var clientConnect = new ClientConnect();

            Assert.Throws<ArgumentNullException>(() => clientConnect.Version = null!);
        }

        [Fact]
        public void Version_Set_Get()
        {
            var clientConnect = new ClientConnect();

            clientConnect.Version = "Terraria194";

            Assert.Equal("Terraria194", clientConnect.Version);
        }

        [Fact]
        public void Read()
        {
            var clientConnect = TestUtils.ReadPacket<ClientConnect>(_bytes, PacketContext.Server);

            Assert.Equal("Terraria194", clientConnect.Version);
        }
    }
}
