namespace Orion.Tests.Networking.Packets.Extensions {
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using Microsoft.Xna.Framework;
    using Orion.Networking.Packets.Extensions;
    using Xunit;

    public class BinaryExtensionTests {
        public static IEnumerable<object[]> ColorData =>
            new List<object[]> {
                new object[] {255, 0, 0},
                new object[] {0, 255, 0},
                new object[] {0, 0, 255},
            };

        [Theory]
        [MemberData(nameof(ColorData))]
        public void ReadColor_IsCorrect(byte r, byte g, byte b) {
            using (var stream = new MemoryStream(new[] {r, g, b}))
            using (var reader = new BinaryReader(stream)) {
                reader.ReadColor().Should().Be(new Color(r, g, b));
            }
        }

        [Theory]
        [MemberData(nameof(ColorData))]
        public void WriteColor_IsCorrect(byte r, byte g, byte b) {
            var buffer = new byte[3];
            using (var stream = new MemoryStream(buffer)) 
            using (var writer = new BinaryWriter(stream)) {
                writer.Write(new Color(r, g, b));
            }

            buffer[0].Should().Be(r);
            buffer[1].Should().Be(g);
            buffer[2].Should().Be(b);
        }
    }
}
