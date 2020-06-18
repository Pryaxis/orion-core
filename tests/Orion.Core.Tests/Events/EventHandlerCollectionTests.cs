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
using System.Threading.Tasks;
using Serilog.Core;
using Xunit;

namespace Orion.Core.Events
{
    public class EventHandlerCollectionTests
    {
        [Fact]
        public void RegisterHandler_Raise()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void RegisterHandler_Raise_HandlerPriorities()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler2, Logger.None);
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void RegisterHandler_Raise_HandlerIgnoresCanceled()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);
            var evt = new TestEvent();
            evt.Cancel();

            collection.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void RegisterHandler_Raise_HandlerThrowsException()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler3, Logger.None);

            collection.Raise(new TestEvent(), Logger.None);
        }

        [Fact]
        public void RegisterAsyncHandler_Raise()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterAsyncHandler(TestHandlerAsync, Logger.None);
            collection.RegisterAsyncHandler(TestHandler2Async, Logger.None);
            collection.RegisterAsyncHandler(TestHandler3Async, Logger.None);
            var evt = new TestEvent();

            collection.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void RegisterAsyncHandler_Raise_HandlerIgnoresCanceled()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterAsyncHandler(TestHandlerAsync, Logger.None);
            var evt = new TestEvent();
            evt.Cancel();

            collection.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void RegisterAsyncHandler_Raise_HandlerThrowsException()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterAsyncHandler(TestHandler3Async, Logger.None);

            collection.Raise(new TestEvent(), Logger.None);
        }

        [Fact]
        public void RegisterAsyncHandler_Raise_HandlerNonBlocking()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterAsyncHandler(TestHandler4Async, Logger.None);

            collection.Raise(new TestEvent(), Logger.None);
        }

        [Fact]
        public void DeregisterHandler()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterHandler(TestHandler, Logger.None);

            collection.DeregisterHandler(TestHandler, Logger.None);

            var evt = new TestEvent();
            collection.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void DeregisterHandler_NotRegistered()
        {
            var collection = new EventHandlerCollection<TestEvent>();

            collection.DeregisterHandler(TestHandler, Logger.None);
        }

        [Fact]
        public void DeregisterAsyncHandler()
        {
            var collection = new EventHandlerCollection<TestEvent>();
            collection.RegisterAsyncHandler(TestHandlerAsync, Logger.None);

            collection.DeregisterAsyncHandler(TestHandlerAsync, Logger.None);

            var evt = new TestEvent();
            collection.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void DeregisterAsyncHandler_NotRegistered()
        {
            var collection = new EventHandlerCollection<TestEvent>();

            collection.DeregisterAsyncHandler(TestHandlerAsync, Logger.None);
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

        [EventHandler("test-async")]
        private static async Task TestHandlerAsync(TestEvent evt)
        {
            await Task.Delay(100);

            evt.Value = 100;
        }

        [EventHandler("test-async-2")]
        private static async Task TestHandler2Async(TestEvent evt)
        {
            await Task.Delay(100);
        }

        [EventHandler("test-async-3")]
        private static async Task TestHandler3Async(TestEvent evt)
        {
            await Task.Delay(100);

            throw new NotImplementedException();
        }

        [EventHandler("test-async-4", IsBlocking = false)]
        private static async Task TestHandler4Async(TestEvent evt)
        {
            await Task.Delay(1000);
        }

        private class TestEvent : Event
        {
            public int Value { get; set; }
        }
    }
}
