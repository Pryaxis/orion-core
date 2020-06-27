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
using Serilog.Events;
using Xunit;

namespace Orion.Core.Events
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class EventAttributeTests
    {
        [Fact]
        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EventAttribute(null!));
        }

        [Fact]
        public void Name_Get()
        {
            var attribute = new EventAttribute("test");

            Assert.Equal("test", attribute.Name);
        }

        [Fact]
        public void LoggingLevel_Set_Get()
        {
            var attribute = new EventAttribute("test");

            attribute.LoggingLevel = LogEventLevel.Information;

            Assert.Equal(LogEventLevel.Information, attribute.LoggingLevel);
        }

        [Fact]
        public void IsCancelable_Set_Get()
        {
            var attribute = new EventAttribute("test");

            attribute.IsCancelable = false;

            Assert.False(attribute.IsCancelable);
        }
    }
}
