using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Terraria.Localization;
using Xunit;

namespace Orion.Tests.Networking.Packets.Extensions {
    public class BinaryExtensionTests {
        public static readonly IEnumerable<object[]> ColorData = new List<object[]> {
            new object[] {new Color(255, 0, 0)},
            new object[] {new Color(0, 255, 0)},
            new object[] {new Color(0, 0, 255)},
        };

        public static readonly IEnumerable<object[]> NetworkTextData = new List<object[]> {
            new object[] {NetworkText.FromLiteral("literal_test")},
            new object[] {NetworkText.FromFormattable("formattable_test{0}", "sub1")},
        };
        
        [Theory]
        [MemberData(nameof(ColorData))]
        public void WriteColor_ReadColor_IsCorrect(Color color) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(color);
                stream.Position = 0;

                reader.ReadColor().Should().Be(color);
            }
        }
        
        [Theory]
        [MemberData(nameof(NetworkTextData))]
        public void WriteNetworkText_ReadNetworkText_IsCorrect(NetworkText text) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            using (var reader = new BinaryReader(stream)) {
                writer.Write(text);
                stream.Position = 0;

                reader.ReadNetworkText().ToString().Should().Be(text.ToString());
            }
        }
    }
}
