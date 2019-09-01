using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;
using Xunit;

namespace Orion.Tests.Networking.Packets.Extensions {
    public class NetworkTextExtensionsTests {
        public static readonly IEnumerable<object[]> GetBinaryLengthData = new List<object[]> {
            new object[] {NetworkText.FromLiteral("test"), Encoding.UTF8},
            new object[] {
                NetworkText.FromFormattable("test{0}{1}{2}", "1", "sub2", "substitution345"),
                Encoding.UTF8,
            },
        };

        [Theory]
        [MemberData(nameof(GetBinaryLengthData))]
        public void GetBinaryLength_IsCorrect(NetworkText text, Encoding encoding) {
            var stream = new MemoryStream();
            using (var writer = new BinaryWriter(stream, encoding, true)) {
                writer.Write(text);
            }

            text.GetBinaryLength(encoding).Should().Be((int)stream.Position);
        }
    }
}
