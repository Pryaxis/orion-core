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
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Orion.Core.Packets.DataStructures
{
    public class NetworkTextTests
    {
        public static readonly IEnumerable<object[]> NetworkTexts = new[]
        {
            new object[] { (NetworkText)"literal" },
            new object[] { NetworkText.Formatted("formattable {0} {1}", "literal", "literal2") },
            new object[] { NetworkText.Localized("localized {0} {1}", "literal", "literal2") },
        };

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void Equals_ReturnsTrue(NetworkText text)
        {
            Assert.True(text.Equals((object)text));
            Assert.True(text.Equals(text));
            Assert.Equal(text.GetHashCode(), text.GetHashCode());
        }

        [Fact]
        public void Equals_ReturnsFalse()
        {
            NetworkText text = "test";
            NetworkText text2 = "test2";

            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
            Assert.False(text.Equals(1234));
            Assert.False(text.Equals((object?)null!));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.False(text.Equals(null!));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [Fact]
        public void Equals_ArgsDifferent_ReturnsFalse()
        {
            var text = NetworkText.Formatted("test {0}", "test");
            var text2 = NetworkText.Formatted("test {0}", "test2");

            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void GetHashCode_Equals_AreEqual(NetworkText text)
        {
            Assert.Equal(text.GetHashCode(), text.GetHashCode());
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Implicit_NullText_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                NetworkText text = (string)null!;
            });
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Implicit()
        {
            NetworkText text = "test";

            Assert.Equal("test", text);
        }

        [Fact]
        public void Formatted_NullFormat_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NetworkText.Formatted(null!));
        }

        [Fact]
        public void Formatted_NullArgs_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NetworkText.Formatted("{0} {1}", null!));
        }

        [Fact]
        public void Formatted_NullArg_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => NetworkText.Formatted("{0} {1}", "a", null!));
        }

        [Fact]
        public void Formatted()
        {
            var text = NetworkText.Formatted("{0} {1}", "a", "b");

            Assert.Equal(NetworkText.Formatted("{0} {1}", "a", "b"), text);
        }

        [Fact]
        public void Localized_NullFormat_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NetworkText.Localized(null!));
        }

        [Fact]
        public void Localized_NullArgs_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => NetworkText.Localized("{0} {1}", null!));
        }

        [Fact]
        public void Localized_NullArg_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => NetworkText.Localized("{0} {1}", "a", null!));
        }

        [Fact]
        public void Localized()
        {
            var text = NetworkText.Localized("{0} {1}", "a", "b");

            Assert.Equal(NetworkText.Localized("{0} {1}", "a", "b"), text);
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsTrue(NetworkText text)
        {
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(text == text);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.True((NetworkText?)null == null);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Equality_ReturnsFalse()
        {
            var text = "test";
            var text2 = "test2";

            Assert.False(text == text2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsTrue()
        {
            var text = "test";
            var text2 = "test2";

            Assert.True(text != text2);
        }

        [Theory]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        [MemberData(nameof(NetworkTexts))]
        public void op_Inequality_ReturnsFalse(NetworkText text)
        {
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.False(text != text);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.False((NetworkText?)null != null);
        }
    }
}
