using OTAPI.Tile;

namespace Orion.World {
    /// <summary>
    /// Orion's optimized implementation of <see cref="Tile"/>.
    /// </summary>
    internal sealed unsafe class OrionTile : Tile, ITile {
        public static int ByteCount => 12;

        private readonly byte* _ptr;

        public override BlockType BlockType {
            get => *(BlockType*)_ptr;
            set => *(BlockType*)_ptr = value;
        }

        public override WallType WallType {
            get => *(WallType*)(_ptr + 2);
            set => *(WallType*)(_ptr + 2) = value;
        }

        public override byte LiquidAmount {
            get => *(_ptr + 3);
            set => *(_ptr + 3) = value;
        }

        public override short TileHeader {
            get => *(short*)(_ptr + 4);
            set => *(short*)(_ptr + 4) = value;
        }

        public override byte TileHeader2 {
            get => *(_ptr + 6);
            set => *(_ptr + 6) = value;
        }

        // TileHeader3 can be ignored in the server.
        public override byte TileHeader3 {
            get => 0;
            set { }
        }

        public override byte TileHeader4 {
            get => *(_ptr + 7);
            set => *(_ptr + 7) = value;
        }

        public override short BlockFrameX {
            get => *(short*)(_ptr + 8);
            set => *(short*)(_ptr + 8) = value;
        }

        public override short BlockFrameY {
            get => *(short*)(_ptr + 10);
            set => *(short*)(_ptr + 10) = value;
        }

        public OrionTile(byte* ptr) {
            _ptr = ptr;
        }

        #region Optimized ITile implementation
        void ITile.ClearEverything() {
            *(int*)_ptr = 0;
            *(int*)(_ptr + 4) = 0;
            *(int*)(_ptr + 8) = 0;
        }

        void ITile.CopyFrom(ITile from) {
            if (from is OrionTile tile) {
                byte* src = tile._ptr;

                *(int*)_ptr = *(int*)src;
                *(int*)(_ptr + 4) = *(int*)(src + 4);
                *(int*)(_ptr + 8) = *(int*)(src + 8);
            } else {
                ((ITile)this).type = from.type;
                ((ITile)this).wall = from.wall;
                ((ITile)this).liquid = from.liquid;
                ((ITile)this).sTileHeader = from.sTileHeader;
                ((ITile)this).bTileHeader = from.bTileHeader;
                ((ITile)this).bTileHeader2 = from.bTileHeader2;
                ((ITile)this).bTileHeader3 = from.bTileHeader3;
                ((ITile)this).frameX = from.frameX;
                ((ITile)this).frameY = from.frameY;
            }
        }
        #endregion
    }
}
