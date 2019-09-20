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

using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Networking.Packets.World.TileEntities {
    public class SignInfoPacketTests {
        [Fact]
        public void SetDefaultableProperties_MarkAsDirty() {
            var packet = new SignInfoPacket();

            packet.ShouldHaveDefaultablePropertiesMarkAsDirty();
        }

        [Fact]
        public void SetSignText_MarksAsDirty() {
            var packet = new SignInfoPacket();

            packet.SignText = "";

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetSignText_NullValue_ThrowsArgumentNullException() {
            var packet = new SignInfoPacket();
            Action action = () => packet.SignText = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {18, 0, 47, 0, 0, 0, 1, 100, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var packet = (SignInfoPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.SignIndex.Should().Be(0);
                packet.SignX.Should().Be(256);
                packet.SignY.Should().Be(100);
                packet.SignText.Should().Be("Terraria");
            }
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
