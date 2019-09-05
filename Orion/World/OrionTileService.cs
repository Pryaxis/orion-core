using System;
using System.Runtime.InteropServices;
using Orion.Framework;
using OTAPI;
using OTAPI.Tile;

namespace Orion.World {
    /// <summary>
    /// Orion's implementation of <see cref="ITileService"/>.
    /// </summary>
    internal sealed unsafe class OrionTileService : OrionService, ITileService, ITileCollection {
        private byte* _ptr = null;

        public override string Author => "Pryaxis";
        public override string Name => "Orion Tile Service";

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Tile this[int x, int y] {
            get {
                if (_ptr == null) {
                    Width = Terraria.Main.maxTilesX;
                    Height = Terraria.Main.maxTilesY;

                    _ptr = (byte*)Marshal.AllocHGlobal(OrionTile.ByteCount * (Width + 1) * (Height + 1));
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
            Marshal.FreeHGlobal((IntPtr)_ptr);

            if (disposeManaged) {
                Hooks.Tile.CreateCollection = null;
            }
        }
    }
}
