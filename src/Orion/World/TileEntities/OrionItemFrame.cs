using Orion.Items;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionItemFrame : OrionTileEntity<TGCTE.TEItemFrame>, IItemFrame {
        private OrionItem _item;

        public IItem Item {
            get {
                if (_item?.Wrapped != Wrapped.item) {
                    _item = new OrionItem(Wrapped.item);
                }

                return _item;
            }
        }

        public OrionItemFrame(TGCTE.TEItemFrame itemFrame) : base(itemFrame) { }
    }
}
