using Orion.World.Tiles;

namespace Orion.Networking.Packets.Modules {
    /// <summary>
    /// Represents a liquid change in a <see cref="LiquidChangesModule"/>.
    /// </summary>
    public struct LiquidChange {
        /// <summary>
        /// Gets the tile's X coordinate.
        /// </summary>
        public short TileX { get; }

        /// <summary>
        /// Gets the tile's Y coordinate.
        /// </summary>
        public short TileY { get; }

        /// <summary>
        /// Gets the liquid amount.
        /// </summary>
        public byte LiquidAmount { get; }

        /// <summary>
        /// Gets the liquid type.
        /// </summary>
        public LiquidType LiquidType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidChange"/> structure with the given coordinates, liquid
        /// amount, and liquid type.
        /// </summary>
        /// <param name="tileX">The tile's X coordinate.</param>
        /// <param name="tileY">The tile's Y coordinate.</param>
        /// <param name="liquidAmount">The liquid amount.</param>
        /// <param name="liquidType">The liquid type.</param>
        public LiquidChange(short tileX, short tileY, byte liquidAmount, LiquidType liquidType) {
            TileX = tileX;
            TileY = tileY;
            LiquidAmount = liquidAmount;
            LiquidType = liquidType;
        }
    }
}
