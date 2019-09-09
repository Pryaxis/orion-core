using Orion.World.Tiles;

namespace Orion.Networking {
    /// <summary>
    /// Represents a tile that is transmitted over the network.
    /// </summary>
    public sealed class NetworkTile : Tile {
        /// <inheritdoc />
        public override BlockType BlockType { get; set; }

        /// <inheritdoc />
        public override WallType WallType { get; set; }

        /// <inheritdoc />
        public override byte LiquidAmount { get; set; }

        /// <inheritdoc />
        public override short TileHeader { get; set; }

        /// <inheritdoc />
        public override byte TileHeader2 { get; set; }

        /// <inheritdoc />
        public override byte TileHeader3 { get; set; }

        /// <inheritdoc />
        public override byte TileHeader4 { get; set; }

        /// <inheritdoc />
        public override short BlockFrameX { get; set; }

        /// <inheritdoc />
        public override short BlockFrameY { get; set; }
    }
}
