namespace Orion.Tests.Networking.Packets {
    using System;
    using System.IO;
    using FluentAssertions;
    using Orion.Networking.Packets;
    using Xunit;

    public class FinishConnectionPacketTests {
        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<FinishConnectionPacket> func = () => FinishConnectionPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void FromReader_IsCorrect() {
            using (var stream = new MemoryStream())
            using (var reader = new BinaryReader(stream)) {

                var packet = FinishConnectionPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeFalse();
                packet.IsSentToServer.Should().BeTrue();
                packet.Type.Should().Be(TerrariaPacketType.FinishConnection);
            }
        }
    }
}
