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

namespace Orion.Events {
    public class CancelableTests {
        [Fact]
        public void IsCanceled_Yes_ReturnsTrue() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupGet(c => c.CancellationReason).Returns("");

            Assert.True(mockCancelable.Object.IsCanceled());

            mockCancelable.VerifyGet(c => c.CancellationReason);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void IsCanceled_No_ReturnsFalse() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupGet(c => c.CancellationReason).Returns((string)null);

            Assert.False(mockCancelable.Object.IsCanceled());

            mockCancelable.VerifyGet(c => c.CancellationReason);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void IsCanceled_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.IsCanceled(null));
        }

        [Fact]
        public void Cancel() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupSet(c => c.CancellationReason = "test");

            mockCancelable.Object.Cancel("test");

            mockCancelable.VerifySet(c => c.CancellationReason = "test");
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void Cancel_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.Cancel(null));
        }

        [Fact]
        public void Cancel_NullCancellation_Reason_ThrowsArgumentNullException() {
            var cancelable = new Mock<ICancelable>().Object;

            Assert.Throws<ArgumentNullException>(() => cancelable.Cancel(null));
        }

        [Fact]
        public void Uncancel() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupSet(c => c.CancellationReason = null);

            mockCancelable.Object.Uncancel();

            mockCancelable.VerifySet(c => c.CancellationReason = null);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void Uncancel_NullCancelable_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => CancelableExtensions.Uncancel(null));
        }
    }
}
