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
using System.Linq;
using System.Reflection;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion {
    public class OrionKernelTests {
        [Fact]
        public void LoadPlugins_NullAssembly_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.LoadPlugins(null));
        }

        [Fact]
        public void UnloadPlugin_NullPlugin_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => kernel.UnloadPlugin(null));
        }

        [Fact]
        public void UnloadPlugin_NotLoaded_ReturnsFalse() {
            using var kernel = new OrionKernel(Logger.None);

            Assert.False(kernel.UnloadPlugin(new TestOrionPlugin(kernel, Logger.None)));
        }

        [Fact]
        public void LoadPlugins_InitializePlugins_UnloadPlugin() {
            using var kernel = new OrionKernel(Logger.None);
            kernel.LoadPlugins(Assembly.GetExecutingAssembly());

            // Testing `InitializePlugins`
            kernel.InitializePlugins();

            Assert.Equal(100, TestOrionPlugin.Value);
            Assert.Collection(kernel.Plugins, kvp => {
                Assert.Equal("test-plugin", kvp.Key);
                Assert.IsType<TestOrionPlugin>(kvp.Value);
            });

            // Testing `UnloadPlugin`
            var plugin = kernel.Plugins.Values.First();
            Assert.True(kernel.UnloadPlugin(plugin));

            Assert.Equal(-100, TestOrionPlugin.Value);
        }
    }

    [Service("test-plugin", Author = "Test Author")]
    public class TestOrionPlugin : OrionPlugin {
        public static int Value { get; set; }

        public TestOrionPlugin(OrionKernel kernel, ILogger log) : base(kernel, log) { }

        public override void Initialize() => Value = 100;

        public override void Dispose() => Value = -100;
    }
}
