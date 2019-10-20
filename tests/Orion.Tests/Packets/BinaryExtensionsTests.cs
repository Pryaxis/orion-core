// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Xunit;

namespace Orion.Packets {
    public class BinaryExtensionTests {
        public static readonly IEnumerable<object[]> ColorData = new List<object[]> {
            new object[] {new Color(255, 0, 0)},
            new object[] {new Color(0, 255, 0)},
            new object[] {new Color(0, 0, 255)}
        };

        public static readonly IEnumerable<object[]> NetworkTextData = new List<object[]> {
            new object[] {NetworkText.FromLiteral("literal_test")},
            new object[] {NetworkText.FromFormattable("formattable_test{0}", "sub1")}
        };

        public static readonly IEnumerable<object[]> PlayerDeathReasonData = new List<object[]> {
            new object[] {Terraria.DataStructures.PlayerDeathReason.ByCustomReason("test")}
        };

        public static readonly IEnumerable<object[]> Vector2Data = new List<object[]> {
            new object[] {new Vector2(100, 100)},
            new object[] {new Vector2(-100, -100)}
        };

        public static readonly IEnumerable<object[]> TimeSpanData = new List<object[]> {
            new object[] {TimeSpan.FromSeconds(60), 2},
            new object[] {TimeSpan.FromSeconds(125), 2},
            new object[] {TimeSpan.FromSeconds(3600), 4}
        };

        [Theory]
        [MemberData(nameof(ColorData))]
        public void WriteColor_ReadColor(Color color) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(in color);
            stream.Position = 0;

            reader.ReadColor().Should().Be(color);
        }

        [Theory]
        [MemberData(nameof(NetworkTextData))]
        public void WriteNetworkText_ReadNetworkText(NetworkText text) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(text);
            stream.Position = 0;

            reader.ReadNetworkText().ToString().Should().Be(text.ToString());
        }

        [Theory]
        [MemberData(nameof(PlayerDeathReasonData))]
        public void WritePlayerDeathReason_ReadPlayerDeathReason(
            Terraria.DataStructures.PlayerDeathReason playerDeathReason) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(playerDeathReason);
            stream.Position = 0;

            reader.ReadPlayerDeathReason().Should().BeEquivalentTo(playerDeathReason);
        }

        [Theory]
        [MemberData(nameof(Vector2Data))]
        public void WriteVector2_ReadVector2(Vector2 vector) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(vector);
            stream.Position = 0;

            reader.ReadVector2().Should().Be(vector);
        }

        [Theory]
        [MemberData(nameof(TimeSpanData))]
        public void WriteTimeSpan_ReadTimeSpan(TimeSpan timeSpan, int numOfBytes) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(in timeSpan, numOfBytes);
            stream.Position = 0;

            reader.ReadTimeSpan(numOfBytes).Should().Be(timeSpan);
        }

        [Fact]
        public void WriteTimeSpan_OutOfRange_2() {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(TimeSpan.FromDays(100), 2);
            stream.Position = 0;

            reader.ReadInt16().Should().Be(short.MaxValue);
        }

        [Fact]
        public void WriteTimeSpan_OutOfRange_4() {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            writer.Write(TimeSpan.FromDays(1000), 4);
            stream.Position = 0;

            reader.ReadInt32().Should().Be(int.MaxValue);
        }

        [Fact]
        public void WriteTimeSpan_Not2Or4_ThrowsArgumentException() {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            Action action = () => writer.Write(TimeSpan.Zero, 10);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ReadTimeSpan_Not2Or4_ThrowsArgumentException() {
            using var stream = new MemoryStream();
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            Action action = () => reader.ReadTimeSpan(10);

            action.Should().Throw<ArgumentException>();
        }
    }
}
