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
    public class IAnnotatableTests
    {
        [Fact]
        public void GetAnnotation_NullKey_ThrowsArgumentNullException()
        {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.GetAnnotation<int>(null!));
        }

        [Fact]
        public void GetAnnotation_KeyDoesNotExist_NullInitializer()
        {
            var annotatable = new AnnotatableObject();
            var key = new AnnotationKey<int>();

            Assert.Equal(0, annotatable.GetAnnotation(key));
        }

        [Fact]
        public void GetAnnotation_KeyDoesNotExist()
        {
            var annotatable = new AnnotatableObject();
            var key = new AnnotationKey<int>();

            Assert.Equal(1, annotatable.GetAnnotation(key, () => 1));
        }

        [Fact]
        public void SetAnnotation_NullKey_ThrowsArgumentNullException()
        {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.SetAnnotation(null!, 1));
        }

        [Fact]
        public void SetAnnotation_GetAnnotation()
        {
            var annotatable = new AnnotatableObject();
            var key = new AnnotationKey<int>();

            annotatable.SetAnnotation(key, 10);

            Assert.Equal(10, annotatable.GetAnnotation(key));
        }

        [Fact]
        public void RemoveAnnotation_NullKey_ThrowsArgumentNullException()
        {
            var annotatable = new AnnotatableObject();

            Assert.Throws<ArgumentNullException>(() => annotatable.RemoveAnnotation<int>(null!));
        }

        [Fact]
        public void RemoveAnnotation_KeyExists()
        {
            var annotatable = new AnnotatableObject();
            var key = new AnnotationKey<int>();
            annotatable.SetAnnotation(key, 10);

            Assert.True(annotatable.RemoveAnnotation(key));
        }

        [Fact]
        public void RemoveAnnotation_KeyDoesNotExist()
        {
            var annotatable = new AnnotatableObject();
            var key = new AnnotationKey<int>();

            Assert.False(annotatable.RemoveAnnotation(key));
        }
    }
}
