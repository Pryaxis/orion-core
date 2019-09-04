using System;
using FluentAssertions;
using Orion.World.Chests;

namespace Orion.Tests.World.Chests {
    public class OrionChestTests {
        public void SetName_NullValue_ThrowsArgumentNullException() {
            var terrariaChest = new Terraria.Chest();
            var chest = new OrionChest(terrariaChest);
            Action action = () => chest.Name = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
