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

namespace Orion.World.TileEntities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionSignTests {
        [Fact]
        public void GetX_IsCorrect() {
            var terrariaSign = new Terraria.Sign {x = 100};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.X.Should().Be(100);
        }

        [Fact]
        public void SetX_IsCorrect() {
            var terrariaSign = new Terraria.Sign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.X = 100;

            terrariaSign.x.Should().Be(100);
        }

        [Fact]
        public void GetY_IsCorrect() {
            var terrariaSign = new Terraria.Sign {y = 100};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Y.Should().Be(100);
        }

        [Fact]
        public void SetY_IsCorrect() {
            var terrariaSign = new Terraria.Sign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Y = 100;

            terrariaSign.y.Should().Be(100);
        }

        [Fact]
        public void GetText_IsCorrect() {
            var terrariaSign = new Terraria.Sign {text = "test"};
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Text.Should().Be("test");
        }

        [Fact]
        public void SetText_IsCorrect() {
            var terrariaSign = new Terraria.Sign();
            ISign sign = new OrionSign(0, terrariaSign);

            sign.Text = "test";

            terrariaSign.text.Should().Be("test");
        }

        [Fact]
        public void SetText_NullValue_ThrowsArgumentNullException() {
            var terrariaSign = new Terraria.Sign();
            ISign sign = new OrionSign(0, terrariaSign);
            Action action = () => sign.Text = null!;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
