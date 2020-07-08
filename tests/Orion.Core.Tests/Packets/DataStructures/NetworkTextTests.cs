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
            new object[] { new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", "literal", "literal2") },
            new object[] { new NetworkText(NetworkTextMode.Localized, "localized {0} {1}", "literal", "literal2") },
        };

        [Fact]
        public void Ctor_NullFormat_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new NetworkText(NetworkTextMode.Literal, null!));
        }

        [Fact]
        public void Ctor_NullSubstitutions_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", null!));
        }

        [Fact]
        public void Ctor_LiteralAndSubstitutions_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new NetworkText(NetworkTextMode.Literal, "literal", "test"));
        }

        [Fact]
        public void Ctor_SubstitutionsContainsNull_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", "test", null!));
        }

        [Fact]
        public void Mode_Get()
        {
            var text = new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", "test", "test2");

            Assert.Equal(NetworkTextMode.Formatted, text.Mode);
        }

        [Fact]
        public void Format_Get()
        {
            var text = new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", "test", "test2");

            Assert.Equal("formatted {0} {1}", text.Format);
        }

        [Fact]
        public void Substitutions_Get()
        {
            var text = new NetworkText(NetworkTextMode.Formatted, "formatted {0} {1}", "test", "test2");

            Assert.Collection(text.Substitutions,
                t => Assert.Equal("test", t),
                t => Assert.Equal("test2", t));
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void Equals_ReturnsTrue(NetworkText text)
        {
            Assert.True(text.Equals((object)text));
            Assert.True(text.Equals(text));
            Assert.Equal(text.GetHashCode(), text.GetHashCode());
        }

        [Fact]
        public void Equals_WrongType_ReturnsFalse()
        {
            NetworkText text = "test";

            Assert.False(text.Equals(1));
        }
        
        [Fact]
        public void Equals_Null_ReturnsFalse()
        {
            NetworkText text = "test";

            Assert.False(text.Equals(null));
        }

        [Fact]
        public void Equals_FormatNotEqual_ReturnsFalse()
        {
            NetworkText text = "test";
            NetworkText text2 = "test2";

            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
        }

        [Fact]
        public void Equals_SubstitutionsNotEqual_ReturnsFalse()
        {
            var text = new NetworkText(NetworkTextMode.Formatted, "test {0}", "test");
            var text2 = new NetworkText(NetworkTextMode.Formatted, "test {0}", "test2");

            Assert.False(text.Equals(text2));
            Assert.False(text.Equals((object)text2));
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void GetHashCode_Equals_AreEqual(NetworkText text)
        {
            Assert.Equal(text.GetHashCode(), text.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(NetworkTexts))]
        public void RoundTrip(NetworkText text)
        {
            var bytes = new byte[10000000];
            var span = bytes.AsSpan();

            var numBytes = text.Write(span);

            Assert.Equal(numBytes, NetworkText.Read(span, out var text2));

            Assert.Equal(text, text2);
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
            NetworkText text = "test";
            NetworkText text2 = "test2";

            Assert.False(text == text2);
        }

        [Fact]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Operator name")]
        public void op_Inequality_ReturnsTrue()
        {
            NetworkText text = "test";
            NetworkText text2 = "test2";

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
