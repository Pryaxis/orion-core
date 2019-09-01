using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class UpdateClientStatusPacketTests {
        // These canned bytes were taken from a real server.
        private static readonly byte[] Bytes = {
            15, 0, 0, 0, 2, 18, 76, 101, 103, 97, 99, 121, 73, 110, 116, 101, 114, 102, 97, 99, 101, 46, 52, 52, 0,
        };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<UpdateClientStatusPacket> func = () => UpdateClientStatusPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FromReader_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream)) {
                var packet = UpdateClientStatusPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.UpdateClientStatus);
                packet.StatusIncrease.Should().Be(15);
                packet.StatusText.Should().Be("LegacyInterface.44");
                stream.Position.Should().Be(Bytes.Length);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes))
            using (var reader = new BinaryReader(stream))
            using (var stream2 = new MemoryStream()) {
                var packet = UpdateClientStatusPacket.FromReader(reader);

                packet.WriteToStream(stream2);

                stream2.ToArray().Skip(3).Should().BeEquivalentTo(Bytes);
            }
        }
    }
}
