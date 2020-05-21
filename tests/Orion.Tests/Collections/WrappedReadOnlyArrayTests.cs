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
using System.Linq;
using Orion.Entities;
using Xunit;

namespace Orion.Collections {
    public class WrappedReadOnlyArrayTests {
        [Fact]
        public void Count_Get() {
            var wrappedItems = new TestWrappedClass[10];
            for (var i = 0; i < 10; ++i) {
                wrappedItems[i] = new TestWrappedClass();
            }

            var array = new WrappedReadOnlyArray<TestClass, TestWrappedClass>(
                wrappedItems, (_, testWrappedClass) => new TestClass(testWrappedClass));

            Assert.Equal(10, array.Count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void Item_GetIndexOutOfRange_ThrowsArgumentOutOfRangeException(int index) {
            var wrappedItems = new TestWrappedClass[10];
            for (var i = 0; i < 10; ++i) {
                wrappedItems[i] = new TestWrappedClass();
            }

            var array = new WrappedReadOnlyArray<TestClass, TestWrappedClass>(
                wrappedItems, (_, testWrappedClass) => new TestClass(testWrappedClass));

            Assert.Throws<IndexOutOfRangeException>(() => array[index]);
        }

        [Fact]
        public void Item_Get() {
            var wrappedItems = new TestWrappedClass[10];
            for (var i = 0; i < 10; ++i) {
                wrappedItems[i] = new TestWrappedClass();
            }

            var array = new WrappedReadOnlyArray<TestClass, TestWrappedClass>(
                wrappedItems, (_, testWrappedClass) => new TestClass(testWrappedClass));

            var item = array[1];
            Assert.Same(wrappedItems[1], item.Wrapped);
        }

        [Fact]
        public void Item_GetMultipleTimes_ReturnsSameInstance() {
            var wrappedItems = new TestWrappedClass[10];
            for (var i = 0; i < 10; ++i) {
                wrappedItems[i] = new TestWrappedClass();
            }

            var array = new WrappedReadOnlyArray<TestClass, TestWrappedClass>(
                wrappedItems, (_, testWrappedClass) => new TestClass(testWrappedClass));

            var item = array[1];
            var item2 = array[1];
            Assert.Same(item, item2);
        }

        [Fact]
        public void GetEnumerator() {
            var wrappedItems = new TestWrappedClass[10];
            for (var i = 0; i < 10; ++i) {
                wrappedItems[i] = new TestWrappedClass();
            }

            var items = new WrappedReadOnlyArray<TestClass, TestWrappedClass>(
                wrappedItems, (_, testWrappedClass) => new TestClass(testWrappedClass)).ToList();
            for (var i = 0; i < 10; ++i) {
                Assert.Same(wrappedItems[i], items[i].Wrapped);
            }
        }

        private class TestClass : IWrapping<TestWrappedClass> {
            public TestWrappedClass Wrapped { get; }

            public TestClass(TestWrappedClass testWrappedClass) {
                Wrapped = testWrappedClass;
            }
        }

        public class TestWrappedClass { }
    }
}
