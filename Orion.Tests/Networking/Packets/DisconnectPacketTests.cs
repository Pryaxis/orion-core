// ReSharper disable ObjectCreationAsStatement

namespace Orion.Tests.Networking.Packets {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class DisconnectPacketTests {
        public static IEnumerable<object[]> CtorReaderData =>
            new List<object[]> {
                new object[] {"test"},
                new object[] {new string('t', 128)},
                new object[] {new string('t', 32768)},
            };

        [Fact]
        public void Ctor_NullReader_ThrowsArgumentNullException() {
            Action action = () => new DisconnectPacket((BinaryReader)null);

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

                var packet = new DisconnectPacket(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.Disconnect);
                packet.Reason.Should().Be(str);
            }
        }

        [Fact]
        public void Ctor_NullVersion_ThrowsArgumentNullException() {
            Action action = () => new DisconnectPacket((string)null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_VersionTooLong_ThrowsArgumentOutOfRangeException() {
            Action action = () => new DisconnectPacket(new string('t', 65536));

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
