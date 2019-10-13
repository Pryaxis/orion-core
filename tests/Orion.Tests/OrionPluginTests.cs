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
using FluentAssertions;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion {
    public class OrionPluginTests {
        [Fact]
        public void Ctor_NullKernel_ThrowsArgumentNullException() {
            Func<OrionPlugin> func = () => new TestOrionPlugin(null, Logger.None);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Kernel_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var plugin = new TestOrionPlugin(kernel, Logger.None);

            plugin.Kernel.Should().BeSameAs(kernel);
        }

        private class TestOrionPlugin : OrionPlugin {
            public new OrionKernel Kernel => base.Kernel;

            public TestOrionPlugin(OrionKernel kernel, ILogger log) : base(kernel, log) { }

            public override void Initialize() => throw new NotImplementedException();
        }
    }
}
