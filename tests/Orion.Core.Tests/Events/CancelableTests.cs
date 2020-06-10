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
using Moq;
using Xunit;

namespace Orion.Core.Events {
    public class CancelableTests {
        [Fact]
        public void IsCanceled_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.IsCanceled(null!));
        }

        [Fact]
        public void IsCanceled_Yes_ReturnsTrue() {
            var cancelable = Mock.Of<ICancelable>(c => c.CancellationReason == "");

            Assert.True(cancelable.IsCanceled());
        }

        [Fact]
        public void IsCanceled_No_ReturnsFalse() {
            var cancelable = Mock.Of<ICancelable>(c => c.CancellationReason == null);

            Assert.False(cancelable.IsCanceled());
        }

        [Fact]
        public void Cancel_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.Cancel(null!));
        }

        [Fact]
        public void Cancel_NullReason_ThrowsArgumentNullException() {
            var cancelable = Mock.Of<ICancelable>();

            Assert.Throws<ArgumentNullException>(() => cancelable.Cancel(null!));
        }

        [Fact]
        public void Cancel() {
            var cancelable = Mock.Of<ICancelable>();

            cancelable.Cancel("test");

            Assert.Equal("test", cancelable.CancellationReason);
        }

        [Fact]
        public void Uncancel_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.Uncancel(null!));
        }

        [Fact]
        public void Uncancel() {
            var cancelable = Mock.Of<ICancelable>(c => c.CancellationReason == "");

            cancelable.Uncancel();

            Assert.Null(cancelable.CancellationReason);
        }
    }
}
