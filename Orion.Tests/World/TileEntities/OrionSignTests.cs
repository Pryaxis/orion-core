using System;
using FluentAssertions;
using Orion.World.TileEntities;

namespace Orion.Tests.World.TileEntities {
    public class OrionSignTests {
        public void SetText_NullValue_ThrowsArgumentNullException() {
            var terrariaSign = new Terraria.Sign();
            var sign = new OrionSign(terrariaSign);
            Action action = () => sign.Text = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
