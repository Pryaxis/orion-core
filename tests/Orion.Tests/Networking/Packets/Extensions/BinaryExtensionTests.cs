using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public static readonly IEnumerable<object[]> Vector2Data = new List<object[]> {
            new object[] {new Vector2(100, 100)},
            new object[] {new Vector2(-100, -100)},
        };
        
        [Theory]
        [MemberData(nameof(ColorData))]
        public void WriteColor_ReadColor_IsCorrect(Color color) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8))
            using (var reader = new BinaryReader(stream, Encoding.UTF8)) {
                writer.Write(color);
                stream.Position = 0;

                reader.ReadColor().Should().Be(color);
            }
        }
        
        [Theory]
        [MemberData(nameof(NetworkTextData))]
        public void WriteNetworkText_ReadNetworkText_IsCorrect(NetworkText text) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8))
            using (var reader = new BinaryReader(stream, Encoding.UTF8)) {
                writer.Write(text);
                stream.Position = 0;

                reader.ReadNetworkText().ToString().Should().Be(text.ToString());
            }
        }
        
        [Theory]
        [MemberData(nameof(Vector2Data))]
        public void WriteVector2_ReadVector2_IsCorrect(Vector2 vector) {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8))
            using (var reader = new BinaryReader(stream, Encoding.UTF8)) {
                writer.Write(vector);
                stream.Position = 0;

                reader.ReadVector2().ToString().Should().Be(vector.ToString());
            }
        }
    }
}
