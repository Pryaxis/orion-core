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

using FluentAssertions;
using Moq;
using Orion.Events;
using Xunit;

namespace Orion.Utils {
    public class DirtiableListTests {
        [Fact]
        public void TIsIDirtiable_IsCorrect() {
            var isDirty = true;
            var mockDirtiable = new Mock<IDirtiable>();
            mockDirtiable.Setup(d => d.IsDirty).Returns(() => isDirty);
            mockDirtiable.Setup(d => d.Clean()).Callback(() => isDirty = false);
            var list = new DirtiableList<IDirtiable> {mockDirtiable.Object};

            list.ShouldBeDirty();

            mockDirtiable.VerifyGet(d => d.IsDirty);
            mockDirtiable.Verify(d => d.Clean());
            mockDirtiable.VerifyNoOtherCalls();
        }

        [Fact]
        public void Count_IsCorrect() {
            var list = new DirtiableList<int>();
            for (var i = 0; i < 100; ++i) {
                list.Add(0);
            }

            list.Count.Should().Be(100);
        }

        [Fact]
        public void IsReadOnly_IsCorrect() {
            var list = new DirtiableList<int>();

            list.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            var list = new DirtiableList<int> {100};

            list[0].Should().Be(100);
        }

        [Fact]
        public void SetItem_MarksAsDirty() {
            var list = new DirtiableList<int> {0};

            list[0] = 100;

            list.ShouldBeDirty();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var list = new DirtiableList<int> {0};

            list.Should().BeEquivalentTo(0);
        }

        [Fact]
        public void Add_IsCorrect() {
            var list = new DirtiableList<int>();

            list.Add(0);

            list[0].Should().Be(0);
            list.Count.Should().Be(1);
            list.ShouldBeDirty();
        }

        [Fact]
        public void Clear_IsCorrect() {
            var list = new DirtiableList<int> {0};
            list.Clean();

            list.Clear();

            list.Count.Should().Be(0);
            list.ShouldBeDirty();
        }

        [Fact]
        public void Contains_IsCorrect() {
            var list = new DirtiableList<int> {100};

            list.Contains(100).Should().BeTrue();
        }

        [Fact]
        public void CopyTo_IsCorrect() {
            var list = new DirtiableList<int> {100};
            var array = new int[1];

            list.CopyTo(array, 0);

            array[0].Should().Be(100);
        }

        [Fact]
        public void Remove_IsCorrect() {
            var list = new DirtiableList<int> {100};
            list.Clean();

            list.Remove(0).Should().BeFalse();

            list.ShouldBeDirty();

            list.Remove(100).Should().BeTrue();

            list.Count.Should().Be(0);
            list.ShouldBeDirty();
        }

        [Fact]
        public void IndexOf_IsCorrect() {
            var list = new DirtiableList<int> {100};

            list.IndexOf(0).Should().Be(-1);
            list.IndexOf(100).Should().Be(0);
        }

        [Fact]
        public void Insert_IsCorrect() {
            var list = new DirtiableList<int> {100};
            list.Clean();

            list.Insert(1, 101);

            list.Count.Should().Be(2);
            list[1].Should().Be(101);
            list.ShouldBeDirty();
        }

        [Fact]
        public void RemoveAt_IsCorrect() {
            var list = new DirtiableList<int> {100};
            list.Clean();

            list.RemoveAt(0);

            list.Should().BeEmpty();
            list.ShouldBeDirty();
        }
    }
}
