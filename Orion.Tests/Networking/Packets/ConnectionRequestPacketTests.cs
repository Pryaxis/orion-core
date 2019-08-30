// ReSharper disable ObjectCreationAsStatement

namespace Orion.Tests.Networking.Packets {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class ConnectionRequestPacketTests {
        public static IEnumerable<object[]> CtorReaderData =>
            new List<object[]> {
                new object[] {"test"},
                new object[] {new string('t', 128)},
                new object[] {new string('t', 32768)},
            };

        [Fact]
        public void Ctor_NullReader_ThrowsArgumentNullException() {
            Action action = () => new ConnectionRequestPacket((BinaryReader)null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CtorReaderData))]

        public void Ctor_Reader_IsCorrect(string str) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(str);
                stream.Position = 0;

                var packet = new ConnectionRequestPacket(reader);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.ConnectionRequest);
                packet.Version.Should().Be(str);
            }
        }

        [Fact]
        public void Ctor_NullVersion_ThrowsArgumentNullException() {
            Action action = () => new ConnectionRequestPacket((string)null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_VersionTooLong_ThrowsArgumentOutOfRangeException() {
            Action action = () => new ConnectionRequestPacket(new string('t', 65536));

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
