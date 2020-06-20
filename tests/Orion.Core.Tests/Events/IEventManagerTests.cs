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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
            var mockEventManager = new Mock<IEventManager>();
            var log = Mock.Of<ILogger>();
            mockEventManager
                .Setup(em => em.RegisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnTest"), log));
            mockEventManager
                .Setup(em => em.RegisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnPrivateTest"), log));
            mockEventManager
                .Setup(em => em.RegisterAsyncHandler(
                    It.Is<Func<TestEvent, Task>>(h => h.Method.Name == "OnTestAsync"), log));

            mockEventManager.Object.RegisterHandlers(new TestClass(), log);

            mockEventManager.VerifyAll();
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
            var mockEventManager = new Mock<IEventManager>();
            var log = Mock.Of<ILogger>();
            mockEventManager
                .Setup(em => em.DeregisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnTest"), log));
            mockEventManager
                .Setup(em => em.DeregisterHandler(
                    It.Is<Action<TestEvent>>(h => h.Method.Name == "OnPrivateTest"), log));
            mockEventManager
                .Setup(em => em.DeregisterAsyncHandler(
                    It.Is<Func<TestEvent, Task>>(h => h.Method.Name == "OnTestAsync"), log));

            mockEventManager.Object.DeregisterHandlers(new TestClass(), log);

            mockEventManager.VerifyAll();
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
            public void OnTest_MissingParam() { }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest_TooManyParams(TestEvent evt, int x) { }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest_InvalidParamType(int x) { }

            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public int OnTest_NotVoid(TestEvent evt) => 0;
        }
    }
}
