// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Xunit;

namespace Orion.Packets {
    public class SpanUtilsTests {
        public static IEnumerable<object[]> StringParams = new[] {
            new object[] { "a" },
            new object[] { new string('a', 128) },
            new object[] { new string('a', 16384) },
            new object[] { new string('a', 2097152) },
        };

        [Theory]
        [MemberData(nameof(StringParams))]
        public void ReadString(string str) {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            writer.Write(str);

            ReadOnlySpan<byte> span = stream.ToArray();

            Assert.Equal(str, SpanUtils.ReadString(ref span, Encoding.UTF8));
            Assert.True(span.IsEmpty);
        }

        [Theory]
        [MemberData(nameof(StringParams))]
        public void WriteString(string str) {
            var bytes = new byte[10000000];
            var span = bytes.AsSpan();

            SpanUtils.Write(ref span, str, Encoding.UTF8);

            using var stream = new MemoryStream(bytes);
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            Assert.Equal(str, reader.ReadString());
        }
    }
}
