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
using System.Linq;
using FluentAssertions;
using Terraria;
using Xunit;

namespace Orion.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionSignServiceTests : IDisposable {
        private readonly ISignService _signService;

        public OrionSignServiceTests() {
            for (var i = 0; i < Sign.maxSigns; ++i) {
                Main.sign[i] = null;
            }

            _signService = new OrionSignService();
        }

        public void Dispose() {
            _signService.Dispose();
        }

        [Fact]
        public void GetItem_IsCorrect() {
            Main.sign[0] = new Sign();
            var sign = (OrionSign)_signService[0];

            sign.Wrapped.Should().BeSameAs(Main.sign[0]);
            sign.Index.Should().Be(0);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            Main.sign[0] = new Sign();
            var sign = _signService[0];
            var sign2 = _signService[0];

            sign.Should().BeSameAs(sign2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetItem_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<ISign> func = () => _signService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetItem_SignIsNull_IsCorrect() {
            var sign = (OrionSign)_signService[0];

            sign.Should().BeNull();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            for (var i = 0; i < Sign.maxSigns; ++i) {
                Main.sign[i] = new Sign();
            }

            var signs = _signService.ToList();

            for (var i = 0; i < signs.Count; ++i) {
                ((OrionSign)signs[i]).Wrapped.Should().BeSameAs(Main.sign[i]);
            }
        }

        [Fact]
        public void AddSign_IsCorrect() {
            var sign = _signService.AddSign(100, 100);

            sign.Should().NotBeNull();
            sign.X.Should().Be(100);
            sign.Y.Should().Be(100);
        }

        [Fact]
        public void AddSign_Exists_ReturnsNull() {
            _signService.AddSign(100, 100);

            var sign = _signService.AddSign(100, 100);

            sign.Should().BeNull();
        }

        [Fact]
        public void AddSign_TooMany_ReturnsNull() {
            for (var i = 0; i < Sign.maxSigns; ++i) {
                Main.sign[i] = new Sign();
            }

            var sign = _signService.AddSign(100, 100);

            sign.Should().BeNull();
        }

        [Fact]
        public void GetSign_IsCorrect() {
            Main.sign[0] = new Sign {
                x = 100,
                y = 100,
                text = "test"
            };

            var sign = _signService.GetSign(100, 100);

            sign.Should().NotBeNull();
            sign.X.Should().Be(100);
            sign.Y.Should().Be(100);
            sign.Text.Should().Be("test");
        }

        [Fact]
        public void GetSign_NoSign_ReturnsNull() {
            var sign = _signService.GetSign(100, 100);

            sign.Should().BeNull();
        }

        [Fact]
        public void RemoveSign_IsCorrect() {
            Main.sign[0] = new Sign {
                x = 100,
                y = 100
            };
            var sign = _signService.GetSign(100, 100);

            var result = _signService.RemoveSign(sign);

            result.Should().BeTrue();
            Main.sign[0].Should().BeNull();
        }

        [Fact]
        public void RemoveSign_NoSign_ReturnsFalse() {
            Main.sign[0] = new Sign {
                x = 100,
                y = 100
            };
            var sign = _signService.GetSign(100, 100);
            Main.sign[0] = null;

            var result = _signService.RemoveSign(sign);

            result.Should().BeFalse();
        }
    }
}
