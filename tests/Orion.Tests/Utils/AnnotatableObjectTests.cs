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
using FluentAssertions;
using Xunit;

namespace Orion.Utils {
    public class AnnotatableObjectTests {
        [Fact]
        public void GetAnnotationOrDefault_KeyDoesNotExist_ReturnsDefaultValue() {
            IAnnotatable annotatable = new AnnotatableObject();

            annotatable.GetAnnotationOrDefault("test", () => 1).Should().Be(1);
        }

        [Fact]
        public void GetAnnotationOrDefault_KeyDoesNotExist_NullProvider_ReturnsDefaultValue() {
            IAnnotatable annotatable = new AnnotatableObject();

            annotatable.GetAnnotationOrDefault<int>("test").Should().Be(0);
        }

        [Fact]
        public void GetAnnotationOrDefault_Create() {
            IAnnotatable annotatable = new AnnotatableObject();

            annotatable.GetAnnotationOrDefault("test", () => 10, true).Should().Be(10);
            
            annotatable.GetAnnotationOrDefault("test", () => 1).Should().Be(10);
        }

        [Fact]
        public void GetAnnotationOrDefault_NullKey_ThrowsArgumentNullException() {
            IAnnotatable annotatable = new AnnotatableObject();
            Func<int> func = () => annotatable.GetAnnotationOrDefault(null, () => 0);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetAnnotation_GetAnnotationOrDefault() {
            IAnnotatable annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", 1);

            annotatable.GetAnnotationOrDefault("test", () => 1).Should().Be(1);
        }

        [Fact]
        public void SetAnnotation_NullKey_ThrowsArgumentNullException() {
            IAnnotatable annotatable = new AnnotatableObject();
            Action action = () => annotatable.SetAnnotation(null, "");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RemoveAnnotation_KeyExists() {
            IAnnotatable annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", 1);

            annotatable.RemoveAnnotation("test").Should().BeTrue();
        }

        [Fact]
        public void RemoveAnnotation_KeyDoesntExist() {
            IAnnotatable annotatable = new AnnotatableObject();

            annotatable.RemoveAnnotation("test").Should().BeFalse();
        }

        [Fact]
        public void RemoveAnnotation_NullValue_ThrowsArgumentNullException() {
            IAnnotatable annotatable = new AnnotatableObject();
            Action action = () => annotatable.RemoveAnnotation(null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
