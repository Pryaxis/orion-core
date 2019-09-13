// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Networking.Packets.World {
    public class RequestSectionPacketTests {
        private static readonly byte[] RequestSectionBytes = {11, 0, 8, 255, 255, 255, 255, 255, 255, 255, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestSectionBytes)) {
                var packet = (RequestSectionPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SectionX.Should().Be(-1);
                packet.SectionY.Should().Be(-1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(RequestSectionBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(RequestSectionBytes);
            }
        }
    }
}
