using Orion.Framework;

namespace Orion.World {
    /// <summary>
    /// Provides access to Terraria's tile mechanism.
    /// </summary>
    public interface ITileService : IService {
        /// <summary>
        /// Gets the width of the tiles.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the tiles.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets or sets the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        /// <remarks>For performance reasons, we do not do bounds checking on the coordinates.</remarks>
        Tile this[int x, int y] { get; set; }
    }
}
