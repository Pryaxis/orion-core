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
using System.Text;
using Orion.DataStructures;
using Xunit;

namespace Orion.Packets {
    public class SpanExtensionsTests {
        public static readonly IEnumerable<object[]> StringParams = new[] {
            new object[] { "a" },
            new object[] { new string('a', 128) },
            new object[] { new string('a', 16384) },
            new object[] { new string('a', 2097152) },
        };

        public static readonly IEnumerable<object[]> NetworkTextParams = new[] {
            new object[] { (NetworkText)"literal" },
            new object[] { NetworkText.Formatted("formattable {0} {1}", "literal", "literal2") },
            new object[] { NetworkText.Localized("localized {0} {1}", "literal", "literal2") },
        };

        [Theory]
        [MemberData(nameof(StringParams))]
        public void WriteString_ReadString(string str) {
            var bytes = new byte[10000000];
            var span = bytes.AsSpan();

            var numBytes = span.Write(str, Encoding.UTF8);

            Assert.Equal(numBytes, span.Read(Encoding.UTF8, out string str2));

            Assert.Equal(str, str2);
        }

        [Theory]
        [MemberData(nameof(NetworkTextParams))]
        public void WriteNetworkText_ReadNetworkText(NetworkText text) {
            var bytes = new byte[10000000];
            var span = bytes.AsSpan();

            var numBytes = span.Write(text, Encoding.UTF8);

            Assert.Equal(numBytes, span.Read(Encoding.UTF8, out NetworkText text2));

            Assert.Equal(text, text2);
        }
    }
}
