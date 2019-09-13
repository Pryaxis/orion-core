// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using Terraria;
using Xunit;

namespace Orion.World.TileEntities {
    public class OrionSignTests {
        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaSign = new Sign {x = x};
            var sign = new OrionSign(0, terrariaSign);

            sign.X.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaSign = new Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.X = x;

            terrariaSign.x.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaSign = new Sign {y = y};
            var sign = new OrionSign(0, terrariaSign);

            sign.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaSign = new Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.Y = y;

            terrariaSign.y.Should().Be(y);
        }

        [Theory]
        [InlineData("test")]
        public void GetText_IsCorrect(string text) {
            var terrariaSign = new Sign {text = text};
            var sign = new OrionSign(0, terrariaSign);

            sign.Text.Should().Be(text);
        }

        [Theory]
        [InlineData("test")]
        public void SetText_IsCorrect(string text) {
            var terrariaSign = new Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.Text = text;

            terrariaSign.text.Should().Be(text);
        }

        [Fact]
        public void SetText_NullValue_ThrowsArgumentNullException() {
            var terrariaSign = new Sign();
            var sign = new OrionSign(0, terrariaSign);
            Action action = () => sign.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
