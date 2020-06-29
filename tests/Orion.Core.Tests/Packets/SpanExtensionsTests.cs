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
using System.Runtime.CompilerServices;
using Xunit;

namespace Orion.Core.Packets
{
    public class SpanExtensionsTests
    {
        public static readonly IEnumerable<object[]> StringParams = new[]
        {
            new object[] { "a" },
            new object[] { new string('a', 128) },
            new object[] { new string('a', 16384) },
            new object[] { new string('a', 2097152) },
        };

        [Fact]
        public void At()
        {
            Span<byte> span = stackalloc byte[] { 255, 255 };

            Assert.True(Unsafe.AreSame(ref span[1], ref span.At(1)));
        }

        [Fact]
        public void ReadBytes()
        {
            Span<byte> span = stackalloc byte[] { 255, 255, 255, 255, 255, 255, 255, 255 };
            var bytes = new byte[8];

            Assert.Equal(8, span.Read(ref bytes[0], 8));

            Assert.Equal(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255 }, bytes);
        }

        [Fact]
        public void ReadString_7BitEncodedIntegerTooLarge_ThrowsArgumentException()
        {
            var bytes = new byte[] { 255, 255, 255, 255, 255 };

            Assert.Throws<ArgumentException>(() =>
            {
                var span = bytes.AsSpan();
                return span.Read(out string _);
            });
        }

        [Fact]
        public void WriteBytes()
        {
            Span<byte> span = stackalloc byte[8];
            var bytes = new byte[8] { 255, 255, 255, 255, 255, 255, 255, 255 };

            Assert.Equal(8, span.Write(ref bytes[0], 8));

            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(255, span[i]);
            }
        }

        [Theory]
        [MemberData(nameof(StringParams))]
        public void WriteString_ReadString(string str)
        {
            var bytes = new byte[10000000];
            var span = bytes.AsSpan();

            var numBytes = span.Write(str);

            Assert.Equal(numBytes, span.Read(out string str2));

            Assert.Equal(str, str2);
        }
    }
}
