using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class RequestWorldSectionPacketTests {
        public static IEnumerable<object[]> CtorByteData =>
            new List<object[]> {
                new object[] {-1, -1},
                new object[] {5140, 1230},
            };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<RequestWorldSectionPacket> func = () => RequestWorldSectionPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CtorByteData))]
        public void FromReader_IsCorrect(int sectionX, int sectionY) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(sectionX);
                writer.Write(sectionY);
                stream.Position = 0;

                var packet = RequestWorldSectionPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.RequestWorldSection);
                packet.SectionX.Should().Be(sectionX);
                packet.SectionY.Should().Be(sectionY);
            }
        }
    }
}
