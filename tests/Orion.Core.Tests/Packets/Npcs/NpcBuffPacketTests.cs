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
using Orion.Core.Buffs;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class NpcBuffPacketTests
    {
        private readonly byte[] _bytes = { 9, 0, 53, 1, 0, 20, 0, 60, 0 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcBuffPacket();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new NpcBuffPacket();

            packet.Id = BuffId.Poisoned;

            Assert.Equal(BuffId.Poisoned, packet.Id);
        }

        [Fact]
        public void Ticks_Set_Get()
        {
            var packet = new NpcBuffPacket();

            packet.Ticks = 60;

            Assert.Equal(60, packet.Ticks);
        }

        [Fact]
        public void Read()
        {
            var packet = new NpcBuffPacket();
            var span = _bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(1, packet.NpcIndex);
            Assert.Equal(BuffId.Poisoned, packet.Id);
            Assert.Equal(60, packet.Ticks);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<NpcBuffPacket>(_bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
