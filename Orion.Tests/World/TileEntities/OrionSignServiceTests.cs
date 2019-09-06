using System;
using System.Linq;
using FluentAssertions;
using Orion.World.TileEntities;
using Orion.World.Tiles;
using Xunit;

namespace Orion.Tests.World.TileEntities {
    [Collection("TerrariaTestsCollection")]
    public class OrionSignServiceTests : IDisposable {
        private readonly ISignService _signService;

        public OrionSignServiceTests() {
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
            Terraria.Main.sign[0] = null;

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
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            Terraria.Main.tile[100, 100] = new Terraria.Tile();
            Terraria.Main.tile[100, 100].active(true);
            Terraria.Main.tile[101, 100] = new Terraria.Tile();
            Terraria.Main.tile[101, 100].active(true);

            var sign = _signService.PlaceSign(100, 99);

            sign.Should().NotBeNull();
            sign.X.Should().Be(100);
            sign.Y.Should().Be(98);
        }

        [Fact]
        public void PlaceSign_InvalidPlacement_ReturnsNull() {
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            Terraria.Main.tile[100, 100] = new Terraria.Tile();
            Terraria.Main.tile[101, 100] = new Terraria.Tile();

            var sign = _signService.PlaceSign(100, 99);

            sign.Should().BeNull();
        }

        [Fact]
        public void GetSign_IsCorrect() {
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            Terraria.Main.tile[100, 100] = new Terraria.Tile();
            Terraria.Main.tile[100, 100].active(true);
            Terraria.Main.tile[101, 100] = new Terraria.Tile();
            Terraria.Main.tile[101, 100].active(true);
            Terraria.WorldGen.PlaceSign(100, 99, (ushort)BlockType.Signs);
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 98,
                text = "test",
            };

            var sign = _signService.GetSign(100, 98);

            sign.Should().NotBeNull();
            sign.X.Should().Be(100);
            sign.Y.Should().Be(98);
            sign.Text.Should().Be("test");
        }

        [Fact]
        public void GetSign_NoSign_ReturnsNull() {
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            for (var i = 0; i < Terraria.Sign.maxSigns; ++i) {
                Terraria.Main.sign[i] = null;
            }

            var sign = _signService.GetSign(100, 98);

            sign.Should().BeNull();
        }

        [Fact]
        public void RemoveSign_IsCorrect() {
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            Terraria.Main.tile[100, 100] = new Terraria.Tile();
            Terraria.Main.tile[100, 100].active(true);
            Terraria.Main.tile[101, 100] = new Terraria.Tile();
            Terraria.Main.tile[101, 100].active(true);
            Terraria.WorldGen.PlaceSign(100, 99, (ushort)BlockType.Signs);
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 98,
            };
            var sign = _signService.GetSign(100, 98);

            var result = _signService.RemoveSign(sign);

            result.Should().BeTrue();
            Terraria.Main.sign[0].Should().BeNull();
        }

        [Fact]
        public void RemoveSign_NoSign_ReturnsFalse() {
            Terraria.Main.tile[100, 98] = new Terraria.Tile();
            Terraria.Main.tile[101, 98] = new Terraria.Tile();
            Terraria.Main.tile[100, 99] = new Terraria.Tile();
            Terraria.Main.tile[101, 99] = new Terraria.Tile();
            Terraria.Main.tile[100, 100] = new Terraria.Tile();
            Terraria.Main.tile[100, 100].active(true);
            Terraria.Main.tile[101, 100] = new Terraria.Tile();
            Terraria.Main.tile[101, 100].active(true);
            Terraria.WorldGen.PlaceSign(100, 99, (ushort)BlockType.Signs);
            Terraria.Main.sign[0] = new Terraria.Sign {
                x = 100,
                y = 98,
            };
            var sign = _signService.GetSign(100, 98);
            for (var i = 0; i < Terraria.Sign.maxSigns; ++i) {
                Terraria.Main.sign[i] = null;
            }

            var result = _signService.RemoveSign(sign);

            result.Should().BeFalse();
        }
    }
}
