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
using Xunit;

namespace Orion.Core.Packets.Players
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerHostTests
    {
        private readonly byte[] _bytes = { 5, 0, 139, 5, 1 };

        [Fact]
        public void Index_Set_Get()
        {
            var packet = new PlayerHost();

            packet.Index = 5;

            Assert.Equal(5, packet.Index);
        }

        [Fact]
        public void IsHost_Set_Get()
        {
            var packet = new PlayerHost();

            packet.IsHost = true;

            Assert.True(packet.IsHost);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerHost>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.Index);
            Assert.True(packet.IsHost);
        }
    }
}
