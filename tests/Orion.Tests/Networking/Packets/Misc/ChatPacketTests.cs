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
using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Networking.Packets.Misc {
    public class ChatPacketTests {
        public static readonly byte[] ChatBytes = {
            18, 0, 107, 255, 255, 255, 0, 8, 84, 101, 114, 114, 97, 114, 105, 97, 100, 0
        };

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(ChatBytes)) {
                var packet = (ChatPacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.ChatColor.Should().Be(Color.White);
                packet.ChatText.ToString().Should().Be("Terraria");
                packet.ChatLineWidth.Should().Be(100);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(ChatBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(ChatBytes);
            }
        }
    }
}
