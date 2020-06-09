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

using System.Linq;
using Xunit;

namespace Orion.Collections {
    public class ArrayTests {
        [Fact]
        public void GetEnumerator() {
            var nums = new[] { 1, 2, 3 };
            var array = new TestArray(nums);

            Assert.Equal(nums, array.ToList());
        }

        private class TestArray : IArray<int> {
            private readonly int[] _array;

            public TestArray(int[] array) {
                _array = array;
            }

            public int this[int index] {
                get => _array[index];
                set => _array[index] = value;
            }

            public int Count => _array.Length;
        }
    }
}
