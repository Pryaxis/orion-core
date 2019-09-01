namespace Orion.Tests.Networking.Packets {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class DisconnectPlayerPacketTests {
        public static IEnumerable<object[]> CtorReaderData =>
            new List<object[]> {
                new object[] {"test"},
                new object[] {new string('t', 128)},
                new object[] {new string('t', 32768)},
            };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<DisconnectPlayerPacket> func = () => DisconnectPlayerPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CtorReaderData))]

        public void FromReader_IsCorrect(string str) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(str);
                stream.Position = 0;

                var packet = DisconnectPlayerPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.DisconnectPlayer);
                packet.Reason.Should().Be(str);
            }
        }

        [Fact]
        public void SetReason_Null_ThrowsArgumentNullException() {
            var packet = new DisconnectPlayerPacket();
            Action action = () => packet.Reason = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
