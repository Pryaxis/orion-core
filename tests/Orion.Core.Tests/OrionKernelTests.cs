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
using System.Reflection;
using Orion.Core.Events;
using Orion.Core.Framework;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Core
{
    [Collection("TerrariaTestsCollection")]
    public class OrionKernelTests
    {
        [Fact]
        public void Ctor_NullLog_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OrionKernel(null!));
        }

        [Fact]
        public void LoadFrom_NullAssembly_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.LoadFrom(null!));
        }

        [Fact]
        public void UnloadPlugin_NullPluginName_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.UnloadPlugin(null!));
        }

        [Fact]
        public void UnloadPlugin_NotLoaded_ReturnsFalse()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.False(kernel.UnloadPlugin("test"));
        }

        [Fact]
        public void LoadFrom_Initialize()
        {
            using var kernel = new OrionKernel(Logger.None);
            kernel.LoadFrom(Assembly.GetExecutingAssembly());

            kernel.Initialize();

            Assert.IsType<TestService>(TestOrionPlugin.SingletonService);
            Assert.IsType<TestService2>(TestOrionPlugin.TransientService);

            Assert.Equal(100, TestOrionPlugin.Value);
            Assert.Collection(kernel.Plugins, kvp =>
            {
                Assert.Equal("test-plugin", kvp.Key);
                Assert.IsType<TestOrionPlugin>(kvp.Value);
            });

            Assert.True(kernel.UnloadPlugin("test-plugin"));

            Assert.Equal(-100, TestOrionPlugin.Value);
        }

        [Fact]
        public void RegisterHandler_NullHandler_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.RegisterHandler<TestEvent>(null!, Logger.None));
        }

        [Fact]
        public void RegisterHandler_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.RegisterHandler<TestEvent>(evt => { }, null!));
        }

        [Fact]
        public void RegisterHandlers_NullHandlerObject_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.RegisterHandlers(null!, Logger.None));
        }

        [Fact]
        public void RegisterHandlers_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.RegisterHandlers(new TestClass(), null!));
        }

        [Fact]
        public void RegisterHandlers_MissingArg_ThrowsArgumentException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentException>(() => kernel.RegisterHandlers(new TestClass_MissingArg(), Logger.None));
        }

        [Fact]
        public void RegisterHandlers_TooManyArgs_ThrowsArgumentException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentException>(() => kernel.RegisterHandlers(new TestClass_TooManyArgs(), Logger.None));
        }

        [Fact]
        public void RegisterHandlers_InvalidArg_ThrowsArgumentException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentException>(() => kernel.RegisterHandlers(new TestClass_InvalidArg(), Logger.None));
        }

        [Fact]
        public void Raise_NullEvt_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.Raise<TestEvent>(null!, Logger.None));
        }

        [Fact]
        public void Raise_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.Raise(new TestEvent(), null!));
        }

        [Fact]
        public void RegisterHandler_Raise()
        {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(evt => evt.Value = 100, Logger.None);
            var evt = new TestEvent();

            kernel.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void RegisterHandlers_Raise()
        {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandlers(new TestClass(), Logger.None);
            var evt = new TestEvent();

            kernel.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void RegisterHandlers_Raise_Private()
        {
            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandlers(new TestClass_Private(), Logger.None);
            var evt = new TestEvent();

            kernel.Raise(evt, Logger.None);

            Assert.Equal(100, evt.Value);
        }

        [Fact]
        public void DeregisterHandler_NullHandler_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.DeregisterHandler<TestEvent>(null!, Logger.None));
        }

        [Fact]
        public void DeregisterHandler_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.DeregisterHandler<TestEvent>(evt => { }, null!));
        }

        [Fact]
        public void DeregisterHandler()
        {
            static void Handler(TestEvent evt) => evt.Value = 100;

            using var kernel = new OrionKernel(Logger.None);
            kernel.RegisterHandler<TestEvent>(Handler, Logger.None);
            Assert.True(kernel.DeregisterHandler<TestEvent>(Handler, Logger.None));
            var evt = new TestEvent();

            kernel.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void DeregisterHandler_NotRegistered()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.False(kernel.DeregisterHandler<TestEvent>(evt => { }, Logger.None));
        }

        [Fact]
        public void DeregisterHandlers_NullHandlerObject_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.DeregisterHandlers(null!, Logger.None));
        }

        [Fact]
        public void DeregisterHandlers_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.DeregisterHandlers(new TestClass(), null!));
        }

        [Fact]
        public void DeregisterHandlers()
        {
            using var kernel = new OrionKernel(Logger.None);
            var testClass = new TestClass();
            kernel.RegisterHandlers(testClass, Logger.None);
            Assert.True(kernel.DeregisterHandlers(testClass, Logger.None));
            var evt = new TestEvent();

            kernel.Raise(evt, Logger.None);

            Assert.NotEqual(100, evt.Value);
        }

        [Fact]
        public void DeregisterHandlers_NotRegistered()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.False(kernel.DeregisterHandlers(new TestClass(), Logger.None));
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
        }

        private class TestClass_Private
        {
            [EventHandler("test")]
            [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Implicit usage")]
            private void OnTest(TestEvent evt) => evt.Value = 100;
        }

        private class TestClass_MissingArg
        {
            [EventHandler("test")]
            public void OnTest() { }
        }

        private class TestClass_TooManyArgs
        {
            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest(TestEvent evt, int x) { }
        }

        private class TestClass_InvalidArg
        {
            [EventHandler("test")]
            [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Testing")]
            public void OnTest(int x) { }
        }
    }

    [Service(ServiceScope.Singleton)]
    public interface ITestSingletonService { }

    [Service(ServiceScope.Transient)]
    public interface ITestTransientService { }

    [Binding("test-service")]
    internal class TestService : ITestSingletonService, ITestTransientService { }

    [Binding("test-service-2", Priority = BindingPriority.Highest)]
    internal class TestService2 : ITestTransientService { }

    [Plugin("test-plugin")]
    public class TestOrionPlugin : OrionPlugin
    {
        public TestOrionPlugin(
                OrionKernel kernel, ILogger log,
                ITestSingletonService singletonService,
                ITestTransientService transientService) : base(kernel, log)
        {
            SingletonService = singletonService;
            TransientService = transientService;
        }

        public static ITestSingletonService SingletonService { get; private set; } = null!;
        public static ITestTransientService TransientService { get; private set; } = null!;
        public static int Value { get; set; }

        public override void Initialize() => Value = 100;
        public override void Dispose() => Value = -100;
    }
}
