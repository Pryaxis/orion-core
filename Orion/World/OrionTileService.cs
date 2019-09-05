using System.Runtime.InteropServices;
using Orion.Framework;
using OTAPI;
using OTAPI.Tile;

namespace Orion.World {
    /// <summary>
    /// Orion's implementation of <see cref="ITileService"/>.
    /// </summary>
    internal sealed unsafe class OrionTileService : OrionService, ITileService, ITileCollection {
        private GCHandle _handle;
        private byte* _ptr;

        public override string Author => "Pryaxis";
        public override string Name => "Orion Tile Service";

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Tile this[int x, int y] {
            get {
                if (!_handle.IsAllocated) {
                    Width = Terraria.Main.maxTilesX;
                    Height = Terraria.Main.maxTilesY;

                    var bytes = new byte[OrionTile.ByteCount * (Width + 1) * (Height + 1)];
                    _handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    _ptr = (byte*)_handle.AddrOfPinnedObject();
                }

                return new OrionTile(_ptr + OrionTile.ByteCount * ((Width + 1) * y + x));
            }
            set => ((ITileCollection)this)[x, y] = value;
        }

        ITile ITileCollection.this[int x, int y] {
            get => this[x, y];
            set {
                var location = new OrionTile(_ptr + OrionTile.ByteCount * ((Width + 1) * y + x));
                ((ITile)location).CopyFrom(value);
            }
        }

        public OrionTileService() {
            Hooks.Tile.CreateCollection = () => this;
        }

        protected override void Dispose(bool disposeManaged) {
            _handle.Free();

            if (disposeManaged) {
                Hooks.Tile.CreateCollection = null;
            }
        }
    }
}
