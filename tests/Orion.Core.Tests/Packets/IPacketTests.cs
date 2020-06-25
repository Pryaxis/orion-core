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
            Assert.Throws<ArgumentNullException>(() => IPacketExtensions.Write(null!, default, PacketContext.Server));
        }

        [Fact]
        public void Write()
        {
            var bytes = new byte[4];
            var packet = new TestPacket();

            Assert.Equal(4, packet.Write(bytes, PacketContext.Server));

            Assert.Equal(new byte[] { 4, 0, 255, 42 }, bytes);
        }

        private class TestPacket : IPacket
        {
            public PacketId Id => (PacketId)255;

            int IPacket.ReadBody(Span<byte> span, PacketContext context) => throw new NotImplementedException();

            int IPacket.WriteBody(Span<byte> span, PacketContext context)
            {
                span[0] = 42;
                return 1;
            }
        }
    }
}
