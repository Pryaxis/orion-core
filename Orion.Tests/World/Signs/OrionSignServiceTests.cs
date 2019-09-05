using System;
using System.Linq;
using FluentAssertions;
using Orion.World.Signs;
using Xunit;

namespace Orion.Tests.World.Signs {
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
            for (var i = 0; i < 1000; ++i) {
                Terraria.Main.sign[i] = new Terraria.Sign();
            }
            var signs = _signService.ToList();

            for (var i = 0; i < signs.Count; ++i) {
                ((OrionSign)signs[i]).Wrapped.Should().BeSameAs(Terraria.Main.sign[i]);
            }
        }
    }
}
