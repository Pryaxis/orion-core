namespace Orion.Tests.Networking.Packets {
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class UnknownPacketTests {
        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<TerrariaPacket> func = () => UnknownPacket.FromReader(null, (TerrariaPacketType)255, 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FromReader_HeaderlessLengthTooLong_ThrowsArgumentOutOfRangeException() {
            using (var stream = new MemoryStream())
            using (var reader = new BinaryReader(stream)) {
                // ReSharper disable once AccessToDisposedClosure
                Func<TerrariaPacket> func = () => UnknownPacket.FromReader(reader, (TerrariaPacketType)255, 65535);

                func.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void FromReader_IsCorrect() {
            var bytes = Enumerable.Range(0, 32768).Select(i => (byte)i).ToArray();
            using (var stream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(stream)) {
                var packet = UnknownPacket.FromReader(reader, (TerrariaPacketType)255, 32768);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be((TerrariaPacketType)255);
                packet.Bytes.Should().BeEquivalentTo(bytes);
            }
        }

        [Fact]
        public void SetBytes_NullValue_ThrowsArgumentNullException() {
            var packet = new UnknownPacket((TerrariaPacketType)255);
            Action action = () => packet.Bytes = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
