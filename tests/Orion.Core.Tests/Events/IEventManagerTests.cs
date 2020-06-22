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
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Moq;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Core.Events
{
    public class IEventManagerTests
    {
        [Fact]
        public void RegisterHandlers_NullEventManager_ThrowsArgumentNullException()
        {
            var log = Mock.Of<ILogger>();

            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.RegisterHandlers(null!, new object(), log));
        }

        [Fact]
        public void RegisterHandlers_NullObj_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();
            var log = Mock.Of<ILogger>();

            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.RegisterHandlers(eventManager, null!, log));
        }

        [Fact]
        public void RegisterHandlers_NullLog_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();

            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.RegisterHandlers(eventManager, new object(), null!));
        }

        [Fact]
        public void RegisterHandlers()
        {
            var eventManager = Mock.Of<IEventManager>();
            var log = Mock.Of<ILogger>();
            Mock.Get(eventManager)
                .Setup(em => em.RegisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnTest"), log));
            Mock.Get(eventManager)
                .Setup(em => em.RegisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnPrivateTest"), log));
            Mock.Get(eventManager)
                .Setup(em => em.RegisterAsyncHandler(
                    It.Is<Func<TestEvent, Task>>(h => h.Method.Name == "OnTestAsync"), log));

            eventManager.RegisterHandlers(new TestClass(), log);

            Mock.Get(eventManager).VerifyAll();
        }

        [Fact]
        public void DeregisterHandlers_NullEventManager_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.DeregisterHandlers(null!, new object(), Logger.None));
        }

        [Fact]
        public void DeregisterHandlers_NullObj_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();

            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.DeregisterHandlers(eventManager, null!, Logger.None));
        }

        [Fact]
        public void DeregisterHandlers_NullLog_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();

            Assert.Throws<ArgumentNullException>(
                () => IEventManagerExtensions.DeregisterHandlers(eventManager, new object(), null!));
        }

        [Fact]
        public void DeregisterHandlers()
        {
            var eventManager = Mock.Of<IEventManager>();
            var log = Mock.Of<ILogger>();
            Mock.Get(eventManager)
                .Setup(em => em.DeregisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnTest"), log));
            Mock.Get(eventManager)
                .Setup(em => em.DeregisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnPrivateTest"), log));
            Mock.Get(eventManager)
                .Setup(em => em.DeregisterAsyncHandler(
                    It.Is<Func<TestEvent, Task>>(h => h.Method.Name == "OnTestAsync"), log));

            eventManager.DeregisterHandlers(new TestClass(), log);

            Mock.Get(eventManager).VerifyAll();
        }

        [Fact]
        public void Forward_NullEventManager_ThrowsArgumentNullException()
        {
            var evt = new TestEvent();
            var newEvt = new TestEvent();
            var log = Mock.Of<ILogger>();

            Assert.Throws<ArgumentNullException>(() => IEventManagerExtensions.Forward(null!, evt, newEvt, log));
        }

        [Fact]
        public void Forward_NullEvt_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();
            var newEvt = new TestEvent();
            var log = Mock.Of<ILogger>();

            Assert.Throws<ArgumentNullException>(() => eventManager.Forward(null!, newEvt, log));
        }

        [Fact]
        public void Forward_NullNewEvt_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();
            var evt = new TestEvent();
            var log = Mock.Of<ILogger>();

            Assert.Throws<ArgumentNullException>(() => eventManager.Forward<TestEvent>(evt, null!, log));
        }

        [Fact]
        public void Forward_NullLog_ThrowsArgumentNullException()
        {
            var eventManager = Mock.Of<IEventManager>();
            var evt = new TestEvent();
            var newEvt = new TestEvent();

            Assert.Throws<ArgumentNullException>(() => eventManager.Forward(evt, newEvt, null!));
        }

        [Fact]
        public void Forward_NotCanceled()
        {
            var eventManager = Mock.Of<IEventManager>();
            var evt = new TestEvent();
            var newEvt = new TestEvent();
            var log = Mock.Of<ILogger>();

            eventManager.Forward(evt, newEvt, log);

            Mock.Get(eventManager)
                .Verify(em => em.Raise(newEvt, log));
        }

        [Fact]
        public void Forward_Canceled()
        {
            var eventManager = Mock.Of<IEventManager>();
            var evt = new TestEvent();
            var newEvt = new TestEvent();
            var log = Mock.Of<ILogger>();
            Mock.Get(eventManager)
                .Setup(em => em.Raise(newEvt, log))
                .Callback<TestEvent, ILogger>((evt, log) => evt.Cancel());

            eventManager.Forward(evt, newEvt, log);

            Assert.True(evt.IsCanceled);

            Mock.Get(eventManager).VerifyAll();
        }

        [Event("test")]
        private class TestEvent : Event
        {
            public int Value { get; set; }
        }

        private class TestClass
        {
            [EventHandler("test")]
            public void OnTest(TestEvent evt) => evt.Value = 100;

            [EventHandler("test")]
            [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Implicit usage")]
            private void OnPrivateTest(TestEvent evt) => evt.Value = 100;

            [EventHandler("test")]
            public async Task OnTestAsync(TestEvent evt)
            {
                await Task.Delay(100);

                evt.Value = 100;
            }

            [EventHandler("test")]
            public void OnTest_MissingParam()
            {
            }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest_TooManyParams(TestEvent evt, int x)
            {
            }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest_InvalidParamType(int x)
            {
            }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public int OnTest_NotVoid(TestEvent evt) => 0;
        }
    }
}
