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
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Framework {
    public class OrionPluginTests {
        [Fact]
        public void Ctor_NullKernel_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new TestOrionPlugin(null!, Logger.None));
        }

        [Fact]
        public void Ctor_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => new TestOrionPlugin(kernel, null!));
        }

        [Fact]
        public void Kernel_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var plugin = new TestOrionPlugin(kernel, Logger.None);

            Assert.Same(kernel, plugin.Kernel);
        }

        private class TestOrionPlugin : OrionPlugin {
            public new OrionKernel Kernel => base.Kernel;

            public TestOrionPlugin(OrionKernel kernel, ILogger log) : base(kernel, log) { }

            public override void Initialize() => throw new NotImplementedException();
        }
    }
}
