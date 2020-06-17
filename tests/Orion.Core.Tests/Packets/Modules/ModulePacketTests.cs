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

namespace Orion.Core.Packets.Modules
{
    public class ModulePacketTests
    {
        public static readonly byte[] Bytes = { 5, 0, 82, 255, 255 };

        [Fact]
        public void Read()
        {
            var packet = new ModulePacket<TestModule>();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));
        }

        [Fact]
        public void Read_AsUnknownModule()
        {
            var packet = new ModulePacket<UnknownModule>();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal((ModuleId)65535, packet.Module.Id);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<ModulePacket<TestModule>>(
                Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_AsUnknownModule()
        {
            TestUtils.RoundTripPacket<ModulePacket<UnknownModule>>(
                Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }

        private struct TestModule : IModule
        {
            public ModuleId Id => (ModuleId)65535;
            public int Read(Span<byte> span, PacketContext context) => 0;
            public int Write(Span<byte> span, PacketContext context) => 0;
        }
    }
}
