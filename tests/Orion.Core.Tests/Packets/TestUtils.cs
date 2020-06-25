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
using Orion.Core.Packets.Modules;
using Xunit;

namespace Orion.Core.Packets
{
    public static class TestUtils
    {
        public static TPacket ReadPacket<TPacket>(Span<byte> span, PacketContext context) where TPacket : IPacket
        {
            var packetLength = IPacket.Read(span, context, out var packet);
            Assert.IsType<TPacket>(packet);
            Assert.Equal(span.Length, packetLength);

            return (TPacket)packet;
        }

        public static TModule ReadModule<TModule>(Span<byte> span, PacketContext context) where TModule : IModule
        {
            var moduleLength = IModule.Read(span, context, out var module);
            Assert.IsType<TModule>(module);
            Assert.Equal(span.Length, moduleLength);

            return (TModule)module;
        }

        /// <summary>
        /// Tests a packet round trip by reading, writing, re-reading, and then re-writing the packet, comparing the
        /// written byte sequences.
        /// </summary>
        /// <param name="span">The span to read from, initially.</param>
        /// <param name="context">The packet context to use.</param>
        public static void RoundTripPacket<TPacket>(Span<byte> span, PacketContext context)
        {
            var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

            // Read the packet.
            IPacket.Read(span, context, out var packet);

            // Write the packet.
            var bytes = new byte[ushort.MaxValue];
            var packetLength = packet.Write(bytes, otherContext);

            // Read the packet again.
            IPacket.Read(span, context, out var packet2);

            // Write the packet again.
            var bytes2 = new byte[ushort.MaxValue];
            var packetLength2 = packet2.Write(bytes2, otherContext);

            Assert.Equal(packetLength, packetLength2);
            for (var i = 0; i < packetLength; ++i)
            {
                Assert.True(bytes[i] == bytes2[i], $"Expected: {bytes[i]}\nActual:   {bytes2[i]}\n  at position {i}");
            }
        }

        /// <summary>
        /// Tests a packet round trip by reading, writing, re-reading, and then re-writing the packet, comparing the
        /// written byte sequences.
        /// </summary>
        /// <param name="span">The span to read from, initially.</param>
        /// <param name="context">The packet context to use.</param>
        public static void RoundTripModule(Span<byte> span, PacketContext context)
        {
            var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

            // Read the packet.
            IModule.Read(span, context, out var module);

            // Write the packet.
            var bytes = new byte[ushort.MaxValue];
            var moduleLength = module.Write(bytes, otherContext);

            // Read the packet again.
            IModule.Read(span, context, out var module2);

            // Write the packet again.
            var bytes2 = new byte[ushort.MaxValue];
            var moduleLength2 = module2.Write(bytes2, otherContext);

            Assert.Equal(moduleLength, moduleLength2);
            for (var i = 0; i < moduleLength; ++i)
            {
                Assert.True(bytes[i] == bytes2[i], $"Expected: {bytes[i]}\nActual:   {bytes2[i]}\n  at position {i}");
            }
        }
    }
}
