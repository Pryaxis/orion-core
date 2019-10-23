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
            Action action = () => kernel.RegisterHandler<TestEvent>(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterHandlers_MissingArg_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_MissingArg());

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_TooManyArgs_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_TooManyArgs());

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_InvalidArg_ThrowsArgumentException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(new TestClass_InvalidArg());

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterHandlers_NullHandlerObject_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RegisterHandlers(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RaiseEvent_AfterRegisterHandler() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(e => e.Value = 100);
            var e = new TestEvent();

            kernel.RaiseEvent(e, null);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void RaiseEvent_AfterRegisterHandlers() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandlers(new TestClass());
            var e = new TestEvent();

            kernel.RaiseEvent(e, null);

            e.Value.Should().Be(100);
        }

        [Fact]
        public void RaiseEvent_NullE_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.RaiseEvent<TestEvent>(null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandler() {
            static void Handler(TestEvent e) => e.Value = 100;

            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(Handler);
            kernel.UnregisterHandler<TestEvent>(Handler);
            var e = new TestEvent();

            kernel.RaiseEvent(e, null);

            e.Value.Should().NotBe(100);
        }

        [Fact]
        public void UnregisterHandler_NullHandler_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandler<TestEvent>(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UnregisterHandlers() {
            using var kernel = new OrionKernel(Logger.None);
            var testClass = new TestClass();
            kernel.RegisterHandlers(testClass);
            kernel.UnregisterHandlers(testClass);
            var e = new TestEvent();

            kernel.RaiseEvent(e, null);

            e.Value.Should().NotBe(100);
        }

        [Fact]
        public void UnregisterHandlers_NotRegistered() {
            using var kernel = new OrionKernel(Logger.None);

            kernel.UnregisterHandlers(new TestClass());
        }

        [Fact]
        public void UnregisterHandlers_NullHandlerObject_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Action action = () => kernel.UnregisterHandlers(null);

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
