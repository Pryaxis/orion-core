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

namespace Orion.Packets {
    public class UnknownPacketTests {
        public static readonly byte[] Bytes = { 11, 0, 255, 0, 1, 2, 3, 4, 5, 6, 7 };

        [Fact]
        public unsafe void Read() {
            UnknownPacket packet = new UnknownPacket();
            packet.Read(Bytes.AsSpan(3..), PacketContext.Server);

            for (var i = 0; i < 8; ++i) {
                Assert.Equal(i, packet.Data[i]);
            }
            Assert.Equal(8, packet.Length);
        }

        [Fact]
        public void RoundTrip() {
            TestUtils.RoundTrip<UnknownPacket>(Bytes.AsSpan(3..), PacketContext.Server);
        }
    }
}
