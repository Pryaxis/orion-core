// ReSharper disable StringLiteralTypo

namespace Orion.Tests.Networking.Packets.Extensions {
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using FluentAssertions;
    using Orion.Networking.Packets.Extensions;
    using Xunit;

    public class StringExtensionsTests {

        public static IEnumerable<object[]> GetBinaryLengthData =>
            new List<object[]> {
                new object[] {"test", Encoding.UTF8},
                new object[] {"tëst", Encoding.UTF8},
                new object[] {"tëst", Encoding.ASCII},
                new object[] {new string('t', 128), Encoding.UTF8},
                new object[] {new string('t', 32768), Encoding.UTF8},
                new object[] {new string('t', 8388608), Encoding.UTF8},
            };

        [Theory]
        [MemberData(nameof(GetBinaryLengthData))]
        public void GetBinaryLength_IsCorrect(string str, Encoding encoding) {
            var stream = new MemoryStream();
            using (var writer = new BinaryWriter(stream, encoding, true)) {
                writer.Write(str);
            }

            str.GetBinaryLength(encoding).Should().Be((int)stream.Position);
        }
    }
}
