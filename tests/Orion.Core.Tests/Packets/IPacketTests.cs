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
using Xunit;

namespace Orion.Core.Packets
{
    public class IPacketTests
    {
        [Fact]
        public void Write_NullPacket_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => IPacketExtensions.Write<IPacket>(null!, default, PacketContext.Server));
        }

        [Fact]
        public void Write_AsServer()
        {
            var packet = new TestPacket { Value = 42 };
            var bytes = new byte[1000];

            var length = packet.Write(bytes, PacketContext.Server);

            Assert.Equal(new byte[] { 4, 0, 255, 42 }, bytes[..length]);
        }

        [Fact]
        public void Write_AsClient()
        {
            var packet = new TestPacket { Value = 42 };
            var bytes = new byte[1000];

            var length = packet.Write(bytes, PacketContext.Client);

            Assert.Equal(new byte[] { 4, 0, 255, 0 }, bytes[..length]);
        }

        private sealed class TestPacket : IPacket
        {
            public byte Value { get; set; }

            public PacketId Id => (PacketId)255;

            public int ReadBody(Span<byte> span, PacketContext context)
            {
                Value = (byte)(span[0] + (context == PacketContext.Server ? 0 : 42));
                return 1;
            }

            public int WriteBody(Span<byte> span, PacketContext context)
            {
                span[0] = (byte)(Value - (context == PacketContext.Server ? 0 : 42));
                return 1;
            }
        }
    }
}
