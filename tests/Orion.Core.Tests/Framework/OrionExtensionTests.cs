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
using System.Text;
using Moq;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Core.Framework
{
    public class OrionExtensionTests
    {
        [Fact]
        public void Ctor_NullKernel_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TestExtension(null!, Logger.None));
        }

        [Fact]
        public void Ctor_NullLog_ThrowsArgumentNullException()
        {
            using var kernel = new OrionKernel(Logger.None);

            Assert.Throws<ArgumentNullException>(() => new TestExtension(kernel, null!));
        }

        [Fact]
        public void Kernel_Get()
        {
            using var kernel = new OrionKernel(Logger.None);
            var log = Mock.Of<ILogger>();
            using var service = new TestExtension(kernel, log);

            Assert.Same(kernel, service.Kernel);
        }

        [Fact]
        public void Log_Get()
        {
            using var kernel = new OrionKernel(Logger.None);
            var log = Mock.Of<ILogger>();
            using var service = new TestExtension(kernel, log);

            Assert.Same(log, service.Log);
        }

        public class TestExtension : OrionExtension
        {
            public TestExtension(OrionKernel kernel, ILogger log) : base(kernel, log) { }
        }
    }
}
