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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Orion.Events;
using Serilog.Core;
using Xunit;

namespace Orion {
    public class OrionKernelTests {
        // It is difficult to test the plugin loading mechanism programmatically...

        [Fact]
        public void QueuePluginsFromPath_NullAssemblyPath_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.StartLoadingPlugins(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnloadPlugin_NullPlugin_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnloadPlugin(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandler_NullHandler_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandler<TestEvent>(null, Logger.None);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandler_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandler<TestEvent>(e => { }, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandlers_MissingArg_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_MissingArg(), Logger.None);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_TooManyArgs_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_TooManyArgs(), Logger.None);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_InvalidArg_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_InvalidArg(), Logger.None);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_NullHandlerObject_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(null, Logger.None);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandlers_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass(), null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RaiseEvent_AfterRegisterHandler() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(e => e.Value = 100, Logger.None);
            var e = new TestEvent();

            kernel.RaiseEvent(e, Logger.None);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void RaiseEvent_AfterRegisterHandlers() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandlers(new TestClass(), Logger.None);
            var e = new TestEvent();

            kernel.RaiseEvent(e, Logger.None);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void RaiseEvent_AfterRegisterHandlers_Private() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandlers(new TestClass_Private(), Logger.None);
            var e = new TestEvent();

            kernel.RaiseEvent(e, Logger.None);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void RaiseEvent_NullE_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RaiseEvent<TestEvent>(null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RaiseEvent_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RaiseEvent(new TestEvent(), null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SuppressEvent() {
            static void Handler(TestEvent e) => e.Value = 100;

            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(Handler, Logger.None);

            kernel.SuppressEvent<TestEvent>();

            var e = new TestEvent();
            kernel.RaiseEvent(e, Logger.None);
            e.Value.Should().NotBe(100);
            
            kernel.RaiseEvent(e, Logger.None);
            e.Value.Should().Be(100);
        }

        [Fact]
        public void SuppressEvent_MultipleTimes() {
            static void Handler(TestEvent e) => e.Value = 100;

            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(Handler, Logger.None);

            kernel.SuppressEvent<TestEvent>();
            kernel.SuppressEvent<TestEvent>();
            kernel.SuppressEvent<TestEvent>();

            var e = new TestEvent();
            kernel.RaiseEvent(e, Logger.None);
            kernel.RaiseEvent(e, Logger.None);
            kernel.RaiseEvent(e, Logger.None);
            e.Value.Should().NotBe(100);
            
            kernel.RaiseEvent(e, Logger.None);
            e.Value.Should().Be(100);
        }

        [Fact]
        public void UnregisterHandler() {
            static void Handler(TestEvent e) => e.Value = 100;

            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(Handler, Logger.None);
            kernel.UnregisterHandler<TestEvent>(Handler, Logger.None);
            var e = new TestEvent();

            kernel.RaiseEvent(e, Logger.None);

            e.Value.Should().NotBe(100);
        }

        [Fact]
        public void UnregisterHandler_NullHandler_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandler<TestEvent>(null, Logger.None);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandler_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandler<TestEvent>(e => { }, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandlers() {
            using var kernel = new OrionKernel(Logger.None);
            var testClass = new TestClass();
            kernel.RegisterHandlers(testClass, Logger.None);
            kernel.UnregisterHandlers(testClass, Logger.None);
            var e = new TestEvent();

            kernel.RaiseEvent(e, Logger.None);

            e.Value.Should().NotBe(100);
        }

        [Fact]
        public void UnregisterHandlers_NotRegistered() {
            using var kernel = new OrionKernel(Logger.None);

            kernel.UnregisterHandlers(new TestClass(), Logger.None);
        }

        [Fact]
        public void UnregisterHandlers_NullHandlerObject_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandlers(null, Logger.None);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandlers_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandlers(new TestClass(), null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Event("test")]
        private class TestEvent : Event {
            public int Value { get; set; }
        }

        private class TestClass {
            [EventHandler]
            public void OnTest(TestEvent e) => e.Value = 100;
        }

        private class TestClass_Private {
            [EventHandler]
            [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Implicit usage")]
            private void OnTest(TestEvent e) => e.Value = 100;
        }

        private class TestClass_MissingArg {
            [EventHandler]
            public void OnTest() { }
        }

        private class TestClass_TooManyArgs {
            [EventHandler]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest(TestEvent e, int x) { }
        }

        private class TestClass_InvalidArg {
            [EventHandler]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest(int x) { }
        }
    }
}
