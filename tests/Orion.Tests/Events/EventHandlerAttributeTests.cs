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

namespace Orion.Events {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class EventHandlerAttributeTests {
        [Fact]
        public void Priority_Get() {
            var attribute = new EventHandlerAttribute(EventPriority.Normal);

            Assert.Equal(EventPriority.Normal, attribute.Priority);
        }

        [Fact]
        public void Name_SetNullValue_ThrowsArgumentNullException() {
            var attribute = new EventHandlerAttribute();

            Assert.Throws<ArgumentNullException>(() => attribute.Name = null!);
        }

        [Fact]
        public void Name_Set_Get() {
            var attribute = new EventHandlerAttribute();

            attribute.Name = "test";

            Assert.Equal("test", attribute.Name);
        }

        [Fact]
        public void IgnoreCanceled_Set_Get() {
            var attribute = new EventHandlerAttribute();

            attribute.IgnoreCanceled = false;

            Assert.False(attribute.IgnoreCanceled);
        }
    }
}
