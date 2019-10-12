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
        public void Invoke_NullArgs_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            collection.RegisterHandler(TestHandler);
            Action action = () => collection.Invoke(this, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandler() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            collection.RegisterHandler(TestHandler);
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void RegisterHandler_Priority() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            collection.RegisterHandler(TestHandler2);
            collection.RegisterHandler(TestHandler);
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(200);
        }

        [Fact]
        public void RegisterHandler_NullHandler_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            Action action = () => collection.RegisterHandler(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandler() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            collection.RegisterHandler(TestHandler2);
            collection.RegisterHandler(TestHandler);
            collection.UnregisterHandler(TestHandler2);
            var args = new TestEventArgs();

            collection.Invoke(this, args);

            args.Value.Should().Be(100);
        }

        [Fact]
        public void UnregisterHandler_NullHandler_ThrowsArgumentNullException() {
            var collection = new EventHandlerCollection<TestEventArgs>(Logger.None);
            Action action = () => collection.UnregisterHandler(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [EventHandler(EventPriority.Lowest)]
        private static void TestHandler(object sender, TestEventArgs args) => args.Value = 100;

        [EventHandler(EventPriority.Highest)]
        private static void TestHandler2(object sender, TestEventArgs args) => args.Value = 200;

        private class TestEventArgs : EventArgs {
            public int Value { get; set; }
        }
    }
}
