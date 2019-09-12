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
using Orion.Networking.Packets;
using Orion.Networking.Packets.Npcs;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcNamePacketTests {
        [Fact]
        public void SetNpcName_NullValue_ThrowsArgumentNullException() {
            var packet = new NpcNamePacket();
            Action action = () => packet.NpcName = null;

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] NpcNameBytes = {14, 0, 56, 0, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97};

        [Fact]
        public void ReadFromStream_ToClient_IsCorrect() {
            using (var stream = new MemoryStream(NpcNameBytes)) {
                var packet = (NpcNamePacket)Packet.ReadFromStream(stream, PacketContext.Client);

                packet.NpcIndex.Should().Be(0);
                packet.NpcName.Should().Be("Terraria");
            }
        }

        [Fact]
        public void WriteToStream_ToClient_IsCorrect() {
            using (var stream = new MemoryStream(NpcNameBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Client);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(NpcNameBytes);
            }
        }

        public static readonly byte[] NpcNameBytes2 = {5, 0, 56, 0, 0};

        [Fact]
        public void ReadFromStream_ToServer_IsCorrect() {
            using (var stream = new MemoryStream(NpcNameBytes2)) {
                var packet = (NpcNamePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.NpcIndex.Should().Be(0);
                packet.NpcName.Should().BeNull();
            }
        }

        [Fact]
        public void WriteToStream_ToServer_IsCorrect() {
            using (var stream = new MemoryStream(NpcNameBytes2))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Client);

                stream2.ToArray().Should().BeEquivalentTo(NpcNameBytes2);
            }
        }
    }
}
