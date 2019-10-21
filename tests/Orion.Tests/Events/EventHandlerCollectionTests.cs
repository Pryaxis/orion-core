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
using Serilog.Core;
using Xunit;

namespace Orion.Events {
    public class EventHandlerCollectionTests {
        [Fact]
        public void Invoke_ThrowsNotImplementedException() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler3);
            var args = new TestEvent();

            collection.Invoke(this, args);
        }

        [Fact]
        public void Invoke_NullArgs_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler);
            Action action = () => collection.Invoke(this, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandler() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler);
            var args = new TestEvent();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void RegisterHandler_Priority() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2);
            collection.RegisterHandler(TestHandler);
            var args = new TestEvent();

            collection.Invoke(this, args);

            args.Value.Should().Be(200);
        }

        [Fact]
        public void RegisterHandler_Log() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var args = new TestEvent();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void RegisterHandler_NullHandler_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEvent>();
            Action action = () => collection.RegisterHandler(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandler() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2);
            collection.RegisterHandler(TestHandler);
            collection.UnregisterHandler(TestHandler2).Should().BeTrue();
            var args = new TestEvent();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void UnregisterHandler_ReturnsFalse() {
            var collection = new EventHandlerCollection<TestEvent>();

            collection.UnregisterHandler(TestHandler).Should().BeFalse();
        }

        [Fact]
        public void UnregisterHandler_NullHandler_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEvent>();
            Action action = () => collection.UnregisterHandler(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [EventHandler(EventPriority.Lowest)]
        private static void TestHandler(object sender, TestEvent e) => e.Value = 100;

        [EventHandler(EventPriority.Highest)]
        private static void TestHandler2(object sender, TestEvent e) => e.Value = 200;

        private static void TestHandler3(object sender, TestEvent e) => throw new NotImplementedException();

        private class TestEvent : Event {
            public int Value { get; set; }
        }
    }
}
