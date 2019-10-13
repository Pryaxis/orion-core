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
using Xunit;

namespace Orion.Events {
    public class EventArgsAttributeTests {
        [Fact]
        public void Ctor_NullName_ThrowsArgumentNullException() {
            Func<EventArgsAttribute> func = () => new EventArgsAttribute(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Name_Get() {
            var attribute = new EventArgsAttribute("test");

            attribute.Name.Should().Be("test");
        }
    }
}
