using System;
using System.Linq;
using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionSignServiceTests : IDisposable {
        private readonly ISignService _signService;

        public OrionSignServiceTests() {
            for (var i = 0; i < Terraria.Sign.maxSigns; ++i) {
                Terraria.Main.sign[i] = null;
            }

            _signService = new OrionSignService();
        }

        public void Dispose() {
            _signService.Dispose();
        }
        
        [Fact]
        public void GetItem_IsCorrect() {
            Terraria.Main.sign[0] = new Terraria.Sign();
            var sign = (OrionSign)_signService[0];

            sign.Wrapped.Should().BeSameAs(Terraria.Main.sign[0]);
            sign.Index.Should().Be(0);
        }

        [Fact]
        public void GetItem_MultipleTimes_ReturnsSameInstance() {
            Terraria.Main.sign[0] = new Terraria.Sign();
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
            for (var i = 0; i < Terraria.Sign.maxSigns; ++i) {
                Terraria.Main.sign[i] = new Terraria.Sign();
            }
            var signs = _signService.ToList();

            for (var i = 0; i < signs.Count; ++i) {
                ((OrionSign)signs[i]).Wrapped.Should().BeSameAs(Terraria.Main.sign[i]);
            }
        }

        [Fact]
        public void PlaceSign_IsCorrect() {
            var sign = _signService.AddSign(100, 100);

            sign.Should().NotBeNull();
            sign.X.Should().Be(100);
            sign.Y.Should().Be(100);
        }

        [Fact]
        public void GetSign_IsCorrect() {
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 100,
                text = "test",
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
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 100,
            };
            var sign = _signService.GetSign(100, 100);

            var result = _signService.RemoveSign(sign);

            result.Should().BeTrue();
            Terraria.Main.sign[0].Should().BeNull();
        }

        [Fact]
        public void RemoveSign_NoSign_ReturnsFalse() {
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 100,
            };
            var sign = _signService.GetSign(100, 100);
            Terraria.Main.sign[0] = null;

            var result = _signService.RemoveSign(sign);

            result.Should().BeFalse();
        }
    }
}
