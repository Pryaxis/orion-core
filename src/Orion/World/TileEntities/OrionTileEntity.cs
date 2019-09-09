using System.Diagnostics;
using Orion.Utils;
using TDS = Terraria.DataStructures;

namespace Orion.World.TileEntities {
    internal abstract class OrionTileEntity<TTileEntity> : AnnotatableObject, ITileEntity
        where TTileEntity : TDS.TileEntity {

        public int Index => Wrapped.ID;

        public int X {
            get => Wrapped.Position.X;
            set => Wrapped.Position = new TDS.Point16(value, Y);
        }

        public int Y {
            get => Wrapped.Position.Y;
            set => Wrapped.Position = new TDS.Point16(X, value);
        }

        internal TTileEntity Wrapped { get; }

        protected OrionTileEntity(TTileEntity terrariaTileEntity) {
            Debug.Assert(terrariaTileEntity != null, $"{nameof(terrariaTileEntity)} should not be null.");

            Wrapped = terrariaTileEntity;
        }
    }
}
