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

namespace Orion.Core.Entities
{
    public class AnnotatableObjectTests
    {
        [Fact]
        public void GetAnnotation_NullKey_ThrowsArgumentNullException()
        {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.GetAnnotation<int>(null!));
        }

        [Fact]
        public void GetAnnotation_InvalidType_ThrowsArgumentException()
        {
            var annotatable = new AnnotatableObject();
            annotatable.GetAnnotation<int>("test") = 5;

            Assert.Throws<ArgumentException>(() => annotatable.GetAnnotation<string>("test"));
        }

        [Fact]
        public void GetAnnotation_KeyDoesNotExist_HasDefaultValue()
        {
            var annotatable = new AnnotatableObject();

            Assert.Equal(1, annotatable.GetAnnotation("test", () => 1));
        }

        [Fact]
        public void GetAnnotation_KeyDoesNotExistNullProvider_HasDefaultValue()
        {
            var annotatable = new AnnotatableObject();

            Assert.Equal(0, annotatable.GetAnnotation<int>("test"));
        }

        [Fact]
        public void GetAnnotation_Set_Get()
        {
            var annotatable = new AnnotatableObject();

            ref var value = ref annotatable.GetAnnotation<int>("test");

            value = 5;

            Assert.Equal(5, annotatable.GetAnnotation<int>("test"));
        }

        [Fact]
        public void RemoveAnnotation_NullValue_ThrowsArgumentNullException()
        {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.RemoveAnnotation(null!));
        }

        [Fact]
        public void RemoveAnnotation_KeyExists_ReturnsTrue()
        {
            var annotatable = new AnnotatableObject();
            annotatable.GetAnnotation<int>("test") = 1;

            Assert.True(annotatable.RemoveAnnotation("test"));
        }

        [Fact]
        public void RemoveAnnotation_KeyDoesntExist_ReturnsFalse()
        {
            var annotatable = new AnnotatableObject();

            Assert.False(annotatable.RemoveAnnotation("test"));
        }
    }
}
