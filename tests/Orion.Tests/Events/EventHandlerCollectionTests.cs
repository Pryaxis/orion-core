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
using Xunit;

namespace Orion.Events {
    public class EventHandlerCollectionTests {
        [Fact]
        public void Invoke_NullArgs_ThrowsArgumentNullException() {
            EventHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler;
            Action action = () => collection.Invoke(this, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Plus() {
            EventHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler;
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void Plus_Priority() {
            EventHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler2;
            collection += TestHandler;
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(200);
        }

        [Fact]
        public void Plus_NullHandler_ThrowsArgumentNullException() {
            EventHandlerCollection<TestEventArgs> collection = null;
            Func<EventHandlerCollection<TestEventArgs>> func = () => collection += null;

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Minus() {
            EventHandlerCollection<TestEventArgs> collection = null;
            collection += TestHandler2;
            collection += TestHandler;
            collection -= TestHandler2;
            var args = new TestEventArgs();

            collection!.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void Minus_NullHandler_ThrowsArgumentNullException() {
            EventHandlerCollection<TestEventArgs> collection = null;
            Func<EventHandlerCollection<TestEventArgs>> func = () => collection -= null;

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Minus_InvalidHandler_ThrowsArgumentException() {
            EventHandlerCollection<TestEventArgs> collection = null;
            Func<EventHandlerCollection<TestEventArgs>> func = () => collection -= TestHandler;

            func.Should().Throw<ArgumentException>();
        }

        [EventHandler(EventPriority.Lowest)]
        private static void TestHandler(object sender, TestEventArgs args) {
            args.Value = 100;
        }

        [EventHandler(EventPriority.Highest)]
        private static void TestHandler2(object sender, TestEventArgs args) {
            args.Value = 200;
        }

        private class TestEventArgs : EventArgs {
            public int Value { get; set; }
        }
    }
}
