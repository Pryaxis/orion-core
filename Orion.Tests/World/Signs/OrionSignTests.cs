using System;
using FluentAssertions;
using Orion.World.Signs;

namespace Orion.Tests.World.Signs {
    public class OrionSignTests {
        public void SetText_NullValue_ThrowsArgumentNullException() {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(terrariaSign);
            Action action = () => sign.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
