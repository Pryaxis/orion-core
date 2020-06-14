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
using Xunit;

namespace Orion.Core.Events {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class EventHandlerAttributeTests {
        [Fact]
        public void Ctor_NullName_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new EventHandlerAttribute(null!));
        }

        [Fact]
        public void Priority_Set_Get() {
            var attribute = new EventHandlerAttribute("test");

            attribute.Priority = EventPriority.Highest;

            Assert.Equal(EventPriority.Highest, attribute.Priority);
        }

        [Fact]
        public void Name_Get() {
            var attribute = new EventHandlerAttribute("test");

            Assert.Equal("test", attribute.Name);
        }

        [Fact]
        public void IgnoreCanceled_Set_Get() {
            var attribute = new EventHandlerAttribute("test");

            attribute.IgnoreCanceled = false;

            Assert.False(attribute.IgnoreCanceled);
        }
    }
}
