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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using TerrariaSign = Terraria.Sign;

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionSignTests {
        [Fact]
        public void Index_Get_IsCorrect() {
            var terrariaSign = new TerrariaSign();
            ISign sign = new OrionSign(1, terrariaSign);

            sign.Index.Should().Be(1);
        }

        [Fact]
        public void X_Get_IsCorrect() {
            var terrariaSign = new TerrariaSign {x = 100};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.X.Should().Be(100);
        }

        [Fact]
        public void X_GetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.X.Should().Be(0);
        }

        [Fact]
        public void X_Set_IsCorrect() {
            var terrariaSign = new TerrariaSign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.X = 100;

            terrariaSign.x.Should().Be(100);
        }

        [Fact]
        public void X_SetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.X = 0;
        }

        [Fact]
        public void Y_Get_IsCorrect() {
            var terrariaSign = new TerrariaSign {y = 100};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Y.Should().Be(100);
        }

        [Fact]
        public void Y_GetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.Y.Should().Be(0);
        }

        [Fact]
        public void Y_Set_IsCorrect() {
            var terrariaSign = new TerrariaSign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Y = 100;

            terrariaSign.y.Should().Be(100);
        }

        [Fact]
        public void Y_SetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.Y = 0;
        }

        [Fact]
        public void Text_Get_IsCorrect() {
            var terrariaSign = new TerrariaSign {text = "test"};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Text.Should().Be("test");
        }

        [Fact]
        public void Text_GetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.Text.Should().Be("");
        }

        [Fact]
        public void Text_Set_IsCorrect() {
            var terrariaSign = new TerrariaSign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Text = "test";

            terrariaSign.text.Should().Be("test");
        }

        [Fact]
        public void Text_SetNotActive_IsCorrect() {
            ISign sign = new OrionSign(0, null);

            sign.Text = "";
        }

        [Fact]
        public void Text_SetNullValue_ThrowsArgumentNullException() {
            var terrariaSign = new TerrariaSign();
            ISign sign = new OrionSign(0, terrariaSign);
            Action action = () => sign.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
