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
using Orion.Core.Npcs;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcReleasePacketTests
    {
        private readonly byte[] _bytes = { 14, 0, 71, 64, 6, 0, 0, 0, 16, 0, 0, 100, 1, 1 };

        [Fact]
        public void X_Set_Get()
        {
            var packet = new NpcReleasePacket();

            packet.X = 1600;

            Assert.Equal(1600, packet.X);
        }

        [Fact]
        public void Y_Set_Get()
        {
            var packet = new NpcReleasePacket();

            packet.Y = 4096;

            Assert.Equal(4096, packet.Y);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new NpcReleasePacket();

            packet.Id = NpcId.Butterfly;

            Assert.Equal(NpcId.Butterfly, packet.Id);
        }

        [Fact]
        public void Style_Set_Get()
        {
            var packet = new NpcReleasePacket();

            packet.Style = 1;

            Assert.Equal(1, packet.Style);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcReleasePacket>(_bytes, PacketContext.Server);

            Assert.Equal(1600, packet.X);
            Assert.Equal(4096, packet.Y);
            Assert.Equal(NpcId.Butterfly, packet.Id);
            Assert.Equal(1, packet.Style);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(_bytes, PacketContext.Server);
        }
    }
}
