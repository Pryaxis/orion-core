using Orion.Items;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents an item frame that is transmitted over the network.
    /// </summary>
    public sealed class NetworkItemFrame : NetworkTileEntity, IItemFrame {
        /// <inheritdoc />
        public ItemType ItemType { get; set; }

        /// <inheritdoc />
        public int ItemStackSize { get; set; }

        /// <inheritdoc />
        public ItemPrefix ItemPrefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTargetDummy"/> class with the specified index and
        /// coordinates.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public NetworkItemFrame(int index, int x, int y) : base(index, x, y) { }
    }
}
