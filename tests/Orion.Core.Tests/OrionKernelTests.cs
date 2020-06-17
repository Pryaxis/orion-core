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
using System.Reflection;
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
        public void Events_Get()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.NotNull(kernel.Events);
        }

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
