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

namespace Orion.Framework {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class BindingAttributeTests {
        [Fact]
        public void Ctor_NullName_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(() => new BindingAttribute(null!));
        }

        [Fact]
        public void Name_Get() {
            var attribute = new BindingAttribute("test");

            Assert.Equal("test", attribute.Name);
        }

        [Fact]
        public void Author_SetNullValue_ThrowsArgumentNullException() {
            var attribute = new BindingAttribute("");

            Assert.Throws<ArgumentNullException>(() => attribute.Author = null!);
        }

        [Fact]
        public void Author_Set_Get() {
            var attribute = new BindingAttribute("");

            attribute.Author = "test";

            Assert.Equal("test", attribute.Author);
        }

        [Fact]
        public void Priority_Set_Get() {
            var attribute = new BindingAttribute("");

            attribute.Priority = BindingPriority.High;

            Assert.Equal(BindingPriority.High, attribute.Priority);
        }
    }
}
