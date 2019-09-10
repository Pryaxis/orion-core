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
        [InlineData(ItemType.SDMG)]
        public void GetItemType_IsCorrect(ItemType itemType) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item {
                    type = (int)itemType,
                },
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemType.Should().Be(itemType);
        }

        [Theory]
        [InlineData(ItemType.SDMG)]
        public void SetItemType_IsCorrect(ItemType itemType) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item(),
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);
            itemFrame.ItemType = itemType;

            terrariaItemFrame.item.type.Should().Be((int)itemType);
        }

        [Theory]
        [InlineData(100)]
        public void GetItemStackSize_IsCorrect(int itemStackSize) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item {
                    stack = itemStackSize,
                },
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemStackSize.Should().Be(itemStackSize);
        }

        [Theory]
        [InlineData(100)]
        public void SetItemStackSize_IsCorrect(int itemStackSize) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item(),
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemStackSize = itemStackSize;

            terrariaItemFrame.item.stack.Should().Be(itemStackSize);
        }

        [Theory]
        [InlineData(ItemPrefix.Unreal)]
        public void GetItemPrefix_IsCorrect(ItemPrefix itemPrefix) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item {
                    prefix = (byte)itemPrefix,
                },
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemPrefix.Should().Be(itemPrefix);
        }

        [Theory]
        [InlineData(ItemPrefix.Unreal)]
        public void SetItemPrefix_IsCorrect(ItemPrefix itemPrefix) {
            var terrariaItemFrame = new TGCTE.TEItemFrame {
                item = new Terraria.Item(),
            };
            var itemFrame = new OrionItemFrame(terrariaItemFrame);

            itemFrame.ItemPrefix = itemPrefix;

            terrariaItemFrame.item.prefix.Should().Be((byte)itemPrefix);
        }
    }
}
