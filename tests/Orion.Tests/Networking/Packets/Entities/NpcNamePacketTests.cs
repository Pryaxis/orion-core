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

namespace Orion.Networking.Packets.Entities {
    public class NpcNamePacketTests {
        [Fact]
        public void SetSimpleProperties_MarkAsDirty() {
            var packet = new NpcNamePacket();

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        [Fact]
        public void SetNpcName_MarksAsDirty() {
            var packet = new NpcNamePacket();

            packet.NpcName = "";

            packet.ShouldBeDirty();
        }

        [Fact]
        public void SetNpcName_NullValue_ThrowsArgumentNullException() {
            var packet = new NpcNamePacket();
            Action action = () => packet.NpcName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] ClientBytes = {14, 0, 56, 0, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_Client_IsCorrect() {
            using (var stream = new MemoryStream(ClientBytes)) {
                var packet = (NpcNamePacket)Packet.ReadFromStream(stream, PacketContext.Client);

                packet.NpcIndex.Should().Be(0);
                packet.NpcName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void DeserializeAndSerialize_Client_SamePacket() {
            using (var stream = new MemoryStream(ClientBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Client);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ClientBytes);
            }
        }

        public static readonly byte[] ServerBytes = {5, 0, 56, 0, 0};

        [Fact]
        public void ReadFromStream_Server_IsCorrect() {
            using (var stream = new MemoryStream(ServerBytes)) {
                var packet = (NpcNamePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcIndex.Should().Be(0);
                packet.NpcName.Should().BeNullOrEmpty();
            }
        }

        [Fact]
        public void DeserializeAndSerialize_Server_SamePacket() {
            using (var stream = new MemoryStream(ServerBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Client);

                stream2.ToArray().Should().BeEquivalentTo(ServerBytes);
            }
        }
    }
}
