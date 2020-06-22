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

namespace Orion.Core.Events
{
    public class EventTests
    {
        [Fact]
        public void IsCanceled_Get_ReturnsTrue()
        {
            var evt = new TestEvent();

            evt.Cancel();

            Assert.True(evt.IsCanceled);
        }

        [Fact]
        public void IsCanceled_Get_ReturnsFalse()
        {
            var evt = new TestEvent();

            Assert.False(evt.IsCanceled);
        }

        [Fact]
        public void CancellationReason_Get()
        {
            var evt = new TestEvent();

            evt.Cancel("test");

            Assert.Equal("test", evt.CancellationReason);
        }

        [Fact]
        public void CancellationReason_GetNotCanceled_ThrowsInvalidOperationException()
        {
            var evt = new TestEvent();

            Assert.Throws<InvalidOperationException>(() => evt.CancellationReason);
        }

        [Fact]
        public void Cancel_NullReason_ThrowsArgumentNullException()
        {
            var evt = new TestEvent();

            Assert.Throws<ArgumentNullException>(() => evt.Cancel(null!));
        }

        [Fact]
        public void Uncancel_NotCanceled_ThrowsInvalidOperationException()
        {
            var evt = new TestEvent();

            Assert.Throws<InvalidOperationException>(() => evt.Uncancel());
        }

        [Fact]
        public void Uncancel()
        {
            var evt = new TestEvent();
            evt.Cancel("test");

            evt.Uncancel();

            Assert.False(evt.IsCanceled);
        }

        [Event("test")]
        private class TestEvent : Event
        {
        }
    }
}
