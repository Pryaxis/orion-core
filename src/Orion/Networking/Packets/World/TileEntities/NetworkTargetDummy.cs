using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a target dummy that is transmitted over the network.
    /// </summary>
    public sealed class NetworkTargetDummy : NetworkTileEntity, ITargetDummy {
        /// <inheritdoc />
        public int NpcIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTargetDummy"/> class with the specified index and
        /// coordinates.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public NetworkTargetDummy(int index, int x, int y) : base(index, x, y) { }
    }
}
