using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Misc;
using Xunit;

namespace Orion.Tests.Networking.Packets.Misc {
    public class EmoteBubblePacketTests {
        public static readonly byte[] EmoteBubbleBytes = {
            12, 0, 91, 1, 0, 0, 0, 0, 100, 0, 255, 1
        };

        [Fact]
        public void ReadFromStream_Normal_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteId.Should().Be(1);
                packet.AnchorType.Should().Be(0);
                packet.AnchorIndex.Should().Be(100);
                packet.Lifetime.Should().Be(255);
                packet.Emotion.Should().Be(1);
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

        public static readonly byte[] EmoteBubbleBytes2 = {
            8, 0, 91, 1, 0, 0, 0, 255,
        };

        [Fact]
        public void ReadFromStream_Remove_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes2)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteId.Should().Be(1);
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

        public static readonly byte[] EmoteBubbleBytes3 = {
            14, 0, 91, 1, 0, 0, 0, 0, 100, 0, 255, 255, 1, 0,
        };

        [Fact]
        public void ReadFromStream_WithMetadata_IsCorrect() {
            using (var stream = new MemoryStream(EmoteBubbleBytes3)) {
                var packet = (EmoteBubblePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                packet.EmoteId.Should().Be(1);
                packet.AnchorType.Should().Be(0);
                packet.AnchorIndex.Should().Be(100);
                packet.Lifetime.Should().Be(255);
                packet.Emotion.Should().Be(-1);
                packet.Metadata.Should().Be(1);
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
