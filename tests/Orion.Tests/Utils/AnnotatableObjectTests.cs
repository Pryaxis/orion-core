// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using FluentAssertions;
using Orion.Utils;
using Xunit;

namespace Orion.Tests.Utils {
    public class AnnotatableObjectTests {
        [Theory]
        [InlineData(100)]
        public void GetAnnotation_KeyDoesNotExist_ReturnsDefaultValue(int value) {
            var annotatable = new AnnotatableObject();

            annotatable.GetAnnotation("test", value).Should().Be(value);
        }

        [Fact]
        public void GetAnnotation_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();
            Action action = () => annotatable.GetAnnotation<int>(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetAnnotation_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();
            Action action = () => annotatable.SetAnnotation(null, "");

            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(100)]
        public void SetAnnotation_GetAnnotation_IsCorrect(int value) {
            var annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", value);

            annotatable.GetAnnotation<int>("test").Should().Be(value);
        }
    }
}
