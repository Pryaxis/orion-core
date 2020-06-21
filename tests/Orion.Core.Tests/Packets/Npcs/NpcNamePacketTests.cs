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

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcNamePacketTests
    {
        private readonly byte[] _serverBytes = { 5, 0, 69, 5, 0 };
        private readonly byte[] _clientBytes = { 18, 0, 69, 5, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97, 1, 0, 0, 0 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcNamePacket();

            packet.NpcIndex = 5;

            Assert.Equal(5, packet.NpcIndex);
        }

        [Fact]
        public void Name_GetNullValue()
        {
            var packet = new NpcNamePacket();

            Assert.Equal(string.Empty, packet.Name);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException()
        {
            var packet = new NpcNamePacket();

            Assert.Throws<ArgumentNullException>(() => packet.Name = null!);
        }

        [Fact]
        public void Name_Set_Get()
        {
            var packet = new NpcNamePacket();

            packet.Name = "Terraria";

            Assert.Equal("Terraria", packet.Name);
        }

        [Fact]
        public void Variant_Set_Get()
        {
            var packet = new NpcNamePacket();

            packet.Variant = 1;

            Assert.Equal(1, packet.Variant);
        }

        [Fact]
        public void Read_AsServer()
        {
            var packet = new NpcNamePacket();
            var span = _serverBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(5, packet.NpcIndex);
        }

        [Fact]
        public void Read_AsClient()
        {
            var packet = new NpcNamePacket();
            var span = _clientBytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Client));

            Assert.Equal(5, packet.NpcIndex);
            Assert.Equal("Terraria", packet.Name);
            Assert.Equal(1, packet.Variant);
        }

        [Fact]
        public void RoundTrip_AsServer()
        {
            TestUtils.RoundTripPacket<NpcNamePacket>(_serverBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_AsClient()
        {
            TestUtils.RoundTripPacket<NpcNamePacket>(_clientBytes.AsSpan(IPacket.HeaderSize..), PacketContext.Client);
        }
    }
}
