namespace Orion.Tests.Networking.Packets {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class ContinueConnectingPacketTests {
        public static IEnumerable<object[]> CtorByteData =>
            new List<object[]> {
                new object[] {(byte)0},
                new object[] {(byte)1},
                new object[] {(byte)255},
            };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => ContinueConnectingPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CtorByteData))]

        public void FromReader_IsCorrect(byte playerId) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(playerId);
                stream.Position = 0;

                var packet = ContinueConnectingPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.ContinueConnecting);
                packet.PlayerId.Should().Be(playerId);
            }
        }
    }
}
