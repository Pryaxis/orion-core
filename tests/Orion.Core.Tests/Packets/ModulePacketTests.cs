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
using Orion.Core.Packets.DataStructures.Modules;
using Xunit;

namespace Orion.Core.Packets
{
    public class ModulePacketTests
    {
        private readonly byte[] _bytes = { 5, 0, 82, 255, 255 };

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<ModulePacket>(_bytes, PacketContext.Server);

            Assert.IsType<UnknownModule>(packet.Module);
            Assert.Equal((ModuleId)65535, ((UnknownModule)packet.Module).Id);
            Assert.Equal(0, ((UnknownModule)packet.Module).Data.Length);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket(_bytes, PacketContext.Server);
        }

        private struct TestModule : IModule
        {
            public ModuleId Id => (ModuleId)65535;
            public int ReadBody(Span<byte> span, PacketContext context) => 0;
            public int WriteBody(Span<byte> span, PacketContext context) => 0;
        }
    }
}
