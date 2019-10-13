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
using Moq;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Xunit;

namespace Orion {
    public class OrionServiceTests {
        [Fact]
        public void Ctor_NullLog_ThrowsArgumentNullException() {
            using var kernel = new OrionKernel(Logger.None);
            Func<OrionService> func = () => new TestService(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Log_Get() {
            var mockLog = new Mock<ILogger>();
            using var service = new TestService(mockLog.Object);

            service.Log.Information("TEST");

            mockLog.Verify(l => l.Write(It.IsAny<LogEvent>()));
        }

        [Fact]
        public void SetLogLevel() {
            var mockLog = new Mock<ILogger>();
            using var service = new TestService(mockLog.Object);

            service.SetLogLevel(LogEventLevel.Error);

            service.Log.IsEnabled(LogEventLevel.Warning).Should().BeFalse();

            service.SetLogLevel(LogEventLevel.Verbose);

            service.Log.IsEnabled(LogEventLevel.Verbose).Should().BeTrue();
        }

        public class TestService : OrionService {
            public new ILogger Log => base.Log;

            public TestService(ILogger log) : base(log) { }
        }
    }
}
