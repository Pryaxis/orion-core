using Orion.Items;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionItemFrame : OrionTileEntity<TGCTE.TEItemFrame>, IItemFrame {
        public ItemType ItemType {
            get => (ItemType)Wrapped.item.type;
            set => Wrapped.item.type = (int)value;
        }

        public int ItemStackSize {
            get => Wrapped.item.stack;
            set => Wrapped.item.stack = value;
        }

        public ItemPrefix ItemPrefix {
            get => (ItemPrefix)Wrapped.item.prefix;
            set => Wrapped.item.prefix = (byte)value;
        }

        public OrionItemFrame(TGCTE.TEItemFrame itemFrame) : base(itemFrame) { }
    }
}
