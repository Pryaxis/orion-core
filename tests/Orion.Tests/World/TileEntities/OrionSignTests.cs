using System;
using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    public class OrionSignTests {
        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaSign = new Terraria.Sign {x = x};
            var sign = new OrionSign(0, terrariaSign);

            sign.X.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.X = x;

            terrariaSign.x.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaSign = new Terraria.Sign {y = y};
            var sign = new OrionSign(0, terrariaSign);

            sign.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.Y = y;

            terrariaSign.y.Should().Be(y);
        }

        [Theory]
        [InlineData("test")]
        public void GetText_IsCorrect(string text) {
            var terrariaSign = new Terraria.Sign {text = text};
            var sign = new OrionSign(0, terrariaSign);

            sign.Text.Should().Be(text);
        }

        [Theory]
        [InlineData("test")]
        public void SetText_IsCorrect(string text) {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(0, terrariaSign);

            sign.Text = text;

            terrariaSign.text.Should().Be(text);
        }

        [Fact]
        public void SetText_NullValue_ThrowsArgumentNullException() {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(0, terrariaSign);
            Action action = () => sign.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
