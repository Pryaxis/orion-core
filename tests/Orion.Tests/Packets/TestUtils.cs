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
using Orion.Packets.Modules;
using Xunit;

namespace Orion.Packets {
    public static class TestUtils {
        // Tests a packet round trip by reading, writing, reading the written packet, writing that read packet, and then
        // comparing the two written byte sequences. This resmoves any boundary conditions where data is in a different
        // form than would be normally expected.
        public static void RoundTripPacket<TPacket>(Span<byte> span, PacketContext context)
                where TPacket : struct, IPacket {
            // Read packet.
            var packet = new TPacket();
            Assert.Equal(span.Length, packet.Read(span, context));

            // Write the packet.
            var bytes = new byte[ushort.MaxValue - IPacket.HeaderSize];
            var packetLength = packet.Write(bytes, context.Switch());

            // Read the packet again.
            Assert.Equal(packetLength, packet.Read(bytes.AsSpan(..packetLength), context));

            // Write the packet again.
            var bytes2 = new byte[ushort.MaxValue - IPacket.HeaderSize];
            var packetLength2 = packet.Write(bytes2, context.Switch());

            Assert.Equal(packetLength, packetLength2);
            Assert.Equal(bytes, bytes2);
        }

        // Tests a module round trip by reading, writing, reading the written module, writing that read module, and then
        // comparing the two written byte sequences. This resmoves any boundary conditions where data is in a different
        // form than would be normally expected.
        public static void RoundTripModule<TModule>(Span<byte> span, PacketContext context)
                where TModule : struct, IModule {
            // Read module.
            var module = new TModule();
            Assert.Equal(span.Length, module.Read(span, context));

            // Write the module.
            var bytes = new byte[ushort.MaxValue - IPacket.HeaderSize - IModule.HeaderSize];
            var moduleLength = module.Write(bytes, context.Switch());

            // Read the module again.
            Assert.Equal(moduleLength, module.Read(bytes.AsSpan(..moduleLength), context));

            // Write the module again.
            var bytes2 = new byte[ushort.MaxValue - IPacket.HeaderSize - IModule.HeaderSize];
            var moduleLength2 = module.Write(bytes2, context.Switch());

            Assert.Equal(moduleLength, moduleLength2);
            Assert.Equal(bytes, bytes2);
        }
    }
}
