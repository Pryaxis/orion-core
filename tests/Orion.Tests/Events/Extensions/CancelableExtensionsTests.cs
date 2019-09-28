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
using Moq;
using Xunit;

namespace Orion.Events.Extensions {
    public class CancelableExtensionsTests {
        [Fact]
        public void IsCanceled_True_IsCorrect() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupGet(c => c.CancellationReason).Returns("");

            mockCancelable.Object.IsCanceled().Should().BeTrue();

            mockCancelable.VerifyGet(c => c.CancellationReason);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void IsCanceled_False_IsCorrect() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupGet(c => c.CancellationReason).Returns((string?)null);

            mockCancelable.Object.IsCanceled().Should().BeFalse();

            mockCancelable.VerifyGet(c => c.CancellationReason);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void IsCanceled_NullCancelable_ThrowsArgumentNullException() {
            Func<bool> func = () => CancelableExtensions.IsCanceled(null!);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Cancel_IsCorrect() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupSet(c => c.CancellationReason = "test");

            mockCancelable.Object.Cancel("test");

            mockCancelable.VerifySet(c => c.CancellationReason = "test");
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void Cancel_NullCancelable_ThrowsArgumentNullException() {
            Action action = () => CancelableExtensions.Cancel(null!);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Cancel_NullCancellation_Reason_ThrowsArgumentNullException() {
            var cancelable = new Mock<ICancelable>().Object;
            Action action = () => cancelable.Cancel(null!);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Uncancel_IsCorrect() {
            var mockCancelable = new Mock<ICancelable>();
            mockCancelable.SetupSet(c => c.CancellationReason = null);

            mockCancelable.Object.Uncancel();

            mockCancelable.VerifySet(c => c.CancellationReason = null);
            mockCancelable.VerifyNoOtherCalls();
        }

        [Fact]
        public void Uncancel_NullCancelable_ThrowsArgumentNullException() {
            Action action = () => CancelableExtensions.Uncancel(null!);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
