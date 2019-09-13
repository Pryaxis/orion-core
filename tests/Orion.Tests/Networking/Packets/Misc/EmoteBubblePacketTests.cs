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

namespace Orion.Networking.Packets.Misc {
    public class EmoteBubblePacketTests {
        public static readonly byte[] EmoteBubbleBytes = {12, 0, 91, 1, 0, 0, 0, 0, 100, 0, 255, 1};

        [Fact]
        public void ReadFromStream_Normal_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteIndex.Should().Be(1);
                packet.AnchorType.Should().Be(0);
                packet.AnchorIndex.Should().Be(100);
                packet.EmoteLifetime.Should().Be(255);
                packet.EmoteEmotion.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_Normal_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(EmoteBubbleBytes);
            }
        }

        public static readonly byte[] EmoteBubbleBytes2 = {8, 0, 91, 1, 0, 0, 0, 255};

        [Fact]
        public void ReadFromStream_Remove_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes2)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteIndex.Should().Be(1);
                packet.AnchorType.Should().Be(255);
            }
        }

        [Fact]
        public void WriteToStream_Remove_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes2))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(EmoteBubbleBytes2);
            }
        }

        public static readonly byte[] EmoteBubbleBytes3 = {14, 0, 91, 1, 0, 0, 0, 0, 100, 0, 255, 255, 1, 0};

        [Fact]
        public void ReadFromStream_WithMetadata_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes3)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteIndex.Should().Be(1);
                packet.AnchorType.Should().Be(0);
                packet.AnchorIndex.Should().Be(100);
                packet.EmoteLifetime.Should().Be(255);
                packet.EmoteEmotion.Should().Be(-1);
                packet.EmoteMetadata.Should().Be(1);
            }
        }

        [Fact]
        public void WriteToStream_WithMetadata_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes3))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream, PacketContext.Server);

                packet.WriteToStream(stream2, PacketContext.Server);

                stream2.ToArray().Should().BeEquivalentTo(EmoteBubbleBytes3);
            }
        }
    }
}
