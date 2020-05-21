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
    public static class TestUtils {
        // Tests a packet round trip by reading, writing, reading the written packet, writing that read packet, and then
        // comparing the two written byte sequences. This resmoves any boundary conditions where data is in a different
        // form than would be normally expected.
        public static void RoundTrip<TPacket>(ReadOnlySpan<byte> span, PacketContext context)
                where TPacket : struct, IPacket {
            // Read packet.
            var packet = new TPacket();
            packet.Read(span, context);

            // Write the packet.
            var bytes = new byte[ushort.MaxValue - sizeof(ushort)];
            Span<byte> tmp = bytes;
            packet.Write(ref tmp, context.Switch());

            // Read the packet again.
            packet.Read(bytes.AsSpan(..(bytes.Length - tmp.Length)), context);

            // Write the packet again.
            var bytes2 = new byte[ushort.MaxValue - sizeof(ushort)];
            Span<byte> tmp2 = bytes2;
            packet.Write(ref tmp2, context.Switch());

            Assert.Equal(bytes, bytes2);
        }
    }
}
