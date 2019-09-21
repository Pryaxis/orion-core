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

namespace Orion.Networking.Packets.World.Tiles {
    public class SectionFramesPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new SectionFramesPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        private static readonly byte[] Bytes = {11, 0, 11, 18, 0, 1, 0, 22, 0, 3, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (SectionFramesPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.StartSectionX.Should().Be(18);
                packet.StartSectionY.Should().Be(1);
                packet.EndSectionX.Should().Be(22);
                packet.EndSectionY.Should().Be(3);
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
