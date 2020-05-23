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
using Xunit;

namespace Orion.Packets.DataStructures {
    public class NetworkTextTests {
        public static readonly IEnumerable<object[]> NetworkTexts = new[] {
            new object[] { new NetworkText(NetworkTextMode.Literal, "literal") },
            new object[] {
                new NetworkText(NetworkTextMode.Formattable, "formattable {0} {1}", "literal", "literal2")
            },
            new object[] {
                new NetworkText(NetworkTextMode.Localized, "localized {0} {1}", "literal", "literal2")
            },
        };

        [Fact]
        public void Ctor_NullText_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new NetworkText(NetworkTextMode.Literal, null!));

            Assert.Throws<ArgumentNullException>(
                () => new NetworkText(NetworkTextMode.Literal, null!, new List<NetworkText>()));
        }

        [Fact]
        public void Ctor_NullSubstitutions_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new NetworkText(NetworkTextMode.Literal, "test", null!));

            Assert.Throws<ArgumentNullException>(
                () => new NetworkText(NetworkTextMode.Literal, null!, (List<NetworkText>)null!));
        }

        [Fact]
        public void Mode_Get() {
            var text = new NetworkText(NetworkTextMode.Literal, "test");

            Assert.Equal(NetworkTextMode.Literal, text.Mode);
        }

        [Fact]
        public void Text_Get() {
            var text = new NetworkText(NetworkTextMode.Literal, "test");

            Assert.Equal("test", text.Text);
        }

        [Fact]
        public void Substitutions_Get() {
            var sub = new NetworkText(NetworkTextMode.Literal, "test");
            var text = new NetworkText(NetworkTextMode.Formattable, "test", sub);

            Assert.Equal(new List<NetworkText> { sub }, text.Substitutions);
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void Equals_ReturnsTrue(NetworkText text) {
            Assert.True(text.Equals((object)text));
            Assert.True(text.Equals(text));
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(text == text);
            Assert.False(text != text);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.Equal(text.GetHashCode(), text.GetHashCode());
        }

        [Fact]
        public void Equals_ReturnsFalse() {
            NetworkText text = "test";
            NetworkText text2 = "test2";

            Assert.False(text == text2);
            Assert.True(text != text2);
            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
            Assert.False(text.Equals(""));
            Assert.False(text.Equals((object)null!));
        }

        [Fact]
        public void Equals_Substitutions_ReturnsFalse() {
            var text = new NetworkText(NetworkTextMode.Formattable, "test {0}", "test");
            var text2 = new NetworkText(NetworkTextMode.Formattable, "test {0}", "test2");

            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
            Assert.False(text == text2);
            Assert.True(text != text2);
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void Write_Read(NetworkText text) {
            var bytes = new byte[10000000];
            var writeSpan = bytes.AsSpan();

            text.Write(ref writeSpan, Encoding.UTF8);

            ReadOnlySpan<byte> readSpan = bytes.AsSpan();
            var text2 = NetworkText.Read(ref readSpan, Encoding.UTF8);

            Assert.Equal(text.Mode, text2.Mode);
        }

        [Fact]
        public void op_Implicit() {
            NetworkText text = "test";

            Assert.Equal(NetworkTextMode.Literal, text.Mode);
            Assert.Equal("test", text.Text);
        }
    }
}
