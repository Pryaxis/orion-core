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

using FluentAssertions;
using Orion.Items;
using Xunit;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    public class OrionPlayerInventoryTests {
        [Fact]
        public void Main_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.Main.Count.Should().Be(terrariaPlayer.inventory.Length);
        }

        [Fact]
        public void Main_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.Main[0]).Wrapped.Should().BeSameAs(terrariaPlayer.inventory[0]);
        }

        [Fact]
        public void Equips_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.Equips.Count.Should().Be(terrariaPlayer.armor.Length);
        }

        [Fact]
        public void Equips_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.Equips[0]).Wrapped.Should().BeSameAs(terrariaPlayer.armor[0]);
        }

        [Fact]
        public void Dyes_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.Dyes.Count.Should().Be(terrariaPlayer.dye.Length);
        }

        [Fact]
        public void Dyes_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.Dyes[0]).Wrapped.Should().BeSameAs(terrariaPlayer.dye[0]);
        }

        [Fact]
        public void MiscEquips_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.MiscEquips.Count.Should().Be(terrariaPlayer.miscEquips.Length);
        }

        [Fact]
        public void MiscEquips_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.MiscEquips[0]).Wrapped.Should().BeSameAs(terrariaPlayer.miscEquips[0]);
        }

        [Fact]
        public void MiscDyes_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.MiscDyes.Count.Should().Be(terrariaPlayer.miscDyes.Length);
        }

        [Fact]
        public void MiscDyes_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.MiscDyes[0]).Wrapped.Should().BeSameAs(terrariaPlayer.miscDyes[0]);
        }

        [Fact]
        public void PiggyBank_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.PiggyBank.Count.Should().Be(terrariaPlayer.bank.item.Length);
        }

        [Fact]
        public void PiggyBank_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.PiggyBank[0]).Wrapped.Should().BeSameAs(terrariaPlayer.bank.item[0]);
        }

        [Fact]
        public void Safe_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.Safe.Count.Should().Be(terrariaPlayer.bank2.item.Length);
        }

        [Fact]
        public void Safe_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.Safe[0]).Wrapped.Should().BeSameAs(terrariaPlayer.bank2.item[0]);
        }

        [Fact]
        public void DefendersForge_Count_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            playerInventory.DefendersForge.Count.Should().Be(terrariaPlayer.bank3.item.Length);
        }

        [Fact]
        public void DefendersForge_Item_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.DefendersForge[0]).Wrapped.Should().BeSameAs(terrariaPlayer.bank3.item[0]);
        }

        [Fact]
        public void TrashCan_Get() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            ((OrionItem)playerInventory.TrashCan).Wrapped.Should().BeSameAs(terrariaPlayer.trashItem);
        }

        [Fact]
        public void TrashCan_GetMultipleTimes_ReturnsSameInstance() {
            var terrariaPlayer = new TerrariaPlayer();
            var playerInventory = new OrionPlayerInventory(terrariaPlayer);

            var trashCan = playerInventory.TrashCan;
            var trashCan2 = playerInventory.TrashCan;

            trashCan.Should().BeSameAs(trashCan2);
        }
    }
}
