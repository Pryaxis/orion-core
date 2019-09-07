using FluentAssertions;
using Orion.Items;
using Orion.World.TileEntities;
using Xunit;
using TDS = Terraria.DataStructures;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.Tests.World.TileEntities {
    public class OrionItemFrameTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {ID = index};
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {Position = new TDS.Point16(x, 0)};
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.X.Should().Be(x);
        }
        
        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaItemFrame = new TGCTE.TEItemFrame();
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.X = x;

            terrariaItemFrame.Position.X.Should().Be((short)x);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {Position = new TDS.Point16(0, y)};
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.Y.Should().Be(y);
        }
        
        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaItemFrame = new TGCTE.TEItemFrame();
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.Y = y;

            terrariaItemFrame.Position.Y.Should().Be((short)y);
        }
        
        [Theory]
        [InlineData(ItemType.SDMG, 1, ItemPrefix.Unreal)]
        public void GetItem_IsCorrect(ItemType itemType, int itemStackSize, ItemPrefix itemPrefix) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item {
                    type = (int)itemType,
                    stack = itemStackSize,
                    prefix = (byte)itemPrefix,
                }
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.Item.Type.Should().Be(itemType);
            itemFrame.Item.StackSize.Should().Be(itemStackSize);
            itemFrame.Item.Prefix.Should().Be(itemPrefix);
        }
    }
}
