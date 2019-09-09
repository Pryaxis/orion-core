using Orion.Utils;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a tile entity that is transmitted over the network.
    /// </summary>
    public abstract class NetworkTileEntity : AnnotatableObject, ITileEntity {
        /// <inheritdoc />
        public int Index { get; set; }

        /// <inheritdoc />
        public int X { get; set; }

        /// <inheritdoc />
        public int Y { get; set; }

        private protected NetworkTileEntity(int index, int x, int y) {
            Index = index;
            X = x;
            Y = y;
        }
    }
}
