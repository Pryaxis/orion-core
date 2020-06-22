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
using Moq;
using Serilog;
using Serilog.Core;
using Xunit;

namespace Orion.Core
{
    public class OrionPluginTests
    {
        [Fact]
        public void Ctor_NullServer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TestOrionPlugin(null!, Logger.None));
        }

        [Fact]
        public void Ctor_NullLog_ThrowsArgumentNullException()
        {
            var server = Mock.Of<IServer>();

            Assert.Throws<ArgumentNullException>(() => new TestOrionPlugin(server, null!));
        }

        [Fact]
        public void Server_Get()
        {
            var server = Mock.Of<IServer>();
            var log = Mock.Of<ILogger>();
            using var extension = new TestOrionPlugin(server, log);

            Assert.Same(server, extension.Server);
        }

        [Fact]
        public void Log_Get()
        {
            var server = Mock.Of<IServer>();
            var log = Mock.Of<ILogger>();
            using var extension = new TestOrionPlugin(server, log);

            Assert.Same(log, extension.Log);
        }

        public class TestOrionPlugin : OrionPlugin
        {
            public TestOrionPlugin(IServer server, ILogger log) : base(server, log)
            {
            }
        }
    }
}
