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
using Serilog.Core;
using Xunit;

namespace Orion.Core.Events
{
    public class EventHandlerCollectionTests
    {
        [Fact]
        public void DeregisterHandler()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2, Logger.None);
            collection.RegisterHandler(TestHandler, Logger.None);
            collection.DeregisterHandler(TestHandler2, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void Raise_RunsHandlers()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void Raise_HandlerPriority()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2, Logger.None);
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void Raise_HandlerIgnoresCanceled()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();
            evt.Cancel();

            collection.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void Raise_HandlerThrowsNotImplementedException()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler3, Logger.None);

            collection.Raise(new TestEvent(), Logger.None);
        }

        [EventHandler("test", Priority = EventPriority.Lowest)]
        private static void TestHandler(TestEvent evt)
        {
            evt.Value = 100;
        }

        [EventHandler("test-2", Priority = EventPriority.Highest)]
        private static void TestHandler2(TestEvent evt)
        {
            evt.Value = 200;
        }

        private static void TestHandler3(TestEvent evt)
        {
            throw new NotImplementedException();
        }

        private class TestEvent : Event
        {
            public int Value { get; set; }
        }
    }
}
