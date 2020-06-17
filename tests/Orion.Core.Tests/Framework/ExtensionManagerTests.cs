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
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Core.Framework
{
    public class ExtensionManagerTests
    {
        [Fact]
        public void Load_NullAssembly_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);
            using var manager = new ExtensionManager(kernel, Logger.None);

            Assert.Throws<ArgumentNullException>(() => manager.Load(null!));
        }

        [Fact]
        public void Load_Initialize()
        {
            using var kernel = new OrionKernel(Logger.None);
            using var manager = new ExtensionManager(kernel, Logger.None);
            manager.Load(Assembly.GetExecutingAssembly());

            manager.Initialize();

            Assert.IsType<TestService>(TestOrionPlugin.SingletonService);
            Assert.IsType<TestService2>(TestOrionPlugin.TransientService);

            Assert.Equal(100, TestOrionPlugin.Value);
            Assert.Collection(manager.Plugins, kvp =>
            {
                Assert.Equal("test-plugin", kvp.Key);
                Assert.IsType<TestOrionPlugin>(kvp.Value);
            });
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
}
