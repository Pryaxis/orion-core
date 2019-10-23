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
        public void Raise() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var e = new TestEvent();

            collection.Raise(e, Logger.None);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void Raise_Priority() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2, Logger.None);
            collection.RegisterHandler(TestHandler, Logger.None);
            var e = new TestEvent();

            collection.Raise(e, Logger.None);

            e.Value.Should().Be(200);
        }

        [Fact]
        public void Raise_IgnoreCanceled() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var e = new TestEvent();
            e.Cancel();

            collection.Raise(e, Logger.None);

            e.Value.Should().NotBe(100);
        }

        [Fact]
        public void Raise_ThrowsNotImplementedException() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler3, Logger.None);

            collection.Raise(new TestEvent(), Logger.None);
        }

        [Fact]
        public void UnregisterHandler() {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2, Logger.None);
            collection.RegisterHandler(TestHandler, Logger.None);
            collection.UnregisterHandler(TestHandler2, Logger.None);
            var e = new TestEvent();

            collection.Raise(e, Logger.None);

            e.Value.Should().Be(100);
        }

        [EventHandler(EventPriority.Lowest, IgnoreCanceled = true)]
        private static void TestHandler(TestEvent e) => e.Value = 100;

        [EventHandler(EventPriority.Highest)]
        private static void TestHandler2(TestEvent e) => e.Value = 200;

        private static void TestHandler3(TestEvent e) => throw new NotImplementedException();

        private class TestEvent : Event, ICancelable {
            public string CancellationReason { get; set; }
            public int Value { get; set; }
        }
    }
}
