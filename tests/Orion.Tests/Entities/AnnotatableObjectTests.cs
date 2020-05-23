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
using Xunit;

namespace Orion.Entities {
    public class AnnotatableObjectTests {
        [Fact]
        public void GetAnnotationOrDefault_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.GetAnnotationOrDefault(null!, () => 0));
        }

        [Fact]
        public void GetAnnotationOrDefault_KeyDoesNotExist_ReturnsDefaultValue() {
            var annotatable = new AnnotatableObject();

            Assert.Equal(1, annotatable.GetAnnotationOrDefault("test", () => 1));
        }

        [Fact]
        public void GetAnnotationOrDefault_KeyDoesNotExist_NullProvider_ReturnsDefaultValue() {
            var annotatable = new AnnotatableObject();

            Assert.Equal(0, annotatable.GetAnnotationOrDefault<int>("test"));
        }

        [Fact]
        public void GetAnnotationOrDefault_Create() {
            var annotatable = new AnnotatableObject();

            Assert.Equal(10, annotatable.GetAnnotationOrDefault("test", () => 10, true));

            Assert.Equal(10, annotatable.GetAnnotationOrDefault<int>("test"));
        }

        [Fact]
        public void SetAnnotation_NullKey_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.SetAnnotation(null!, "test"));
        }

        [Fact]
        public void SetAnnotation_GetAnnotationOrDefault() {
            var annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", 1);

            Assert.Equal(1, annotatable.GetAnnotationOrDefault<int>("test"));
        }

        [Fact]
        public void RemoveAnnotation_NullValue_ThrowsArgumentNullException() {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.RemoveAnnotation(null!));
        }

        [Fact]
        public void RemoveAnnotation_KeyExists() {
            var annotatable = new AnnotatableObject();
            annotatable.SetAnnotation("test", 1);

            Assert.True(annotatable.RemoveAnnotation("test"));
        }

        [Fact]
        public void RemoveAnnotation_KeyDoesntExist() {
            var annotatable = new AnnotatableObject();

            Assert.False(annotatable.RemoveAnnotation("test"));
        }
    }
}
