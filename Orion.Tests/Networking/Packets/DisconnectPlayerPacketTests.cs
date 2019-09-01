using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;
using Xunit;

namespace Orion.Tests.Networking.Packets {
    public class DisconnectPlayerPacketTests {
        public static readonly IEnumerable<object[]> CtorReaderData = new List<object[]> {
            new object[] {NetworkText.FromLiteral("test")},
            new object[] {NetworkText.FromFormattable("test{0}", "1")},
        };

        [Fact]
        public void FromReader_NullReader_ThrowsArgumentNullException() {
            Func<DisconnectPlayerPacket> func = () => DisconnectPlayerPacket.FromReader(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CtorReaderData))]
        public void FromReader_IsCorrect(NetworkText text) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(text);
                stream.Position = 0;

                var packet = DisconnectPlayerPacket.FromReader(reader);

                packet.IsSentToClient.Should().BeTrue();
                packet.IsSentToServer.Should().BeFalse();
                packet.Type.Should().Be(TerrariaPacketType.DisconnectPlayer);
                packet.Reason.ToString().Should().Be(text.ToString());
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
