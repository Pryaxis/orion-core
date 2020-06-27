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

namespace Orion.Core.Packets
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class UnknownPacketTests
    {
        private readonly byte[] _bytes = { 11, 0, 255, 0, 1, 2, 3, 4, 5, 6, 7 };
        private readonly byte[] _emptyBytes = { 3, 0, 255 };

        [Fact]
        public void Ctor_NegativeLength_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new UnknownPacket(-1, (PacketId)255));
        }

        [Fact]
        public void Data_Get()
        {
            var packet = new UnknownPacket(8, (PacketId)255);

            Assert.Equal(8, packet.Data.Length);
        }

        [Fact]
        public void Id_Set_Get()
        {
            var packet = new UnknownPacket(8, (PacketId)255);

            Assert.Equal((PacketId)255, packet.Id);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<UnknownPacket>(_bytes, PacketContext.Server);

            Assert.Equal(8, packet.Data.Length);
            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(i, packet.Data[i]);
            }
        }

        [Fact]
        public void Read_Empty()
        {
            var packet = TestUtils.ReadPacket<UnknownPacket>(_emptyBytes, PacketContext.Server);

            Assert.Equal(0, packet.Data.Length);
        }
    }
}
