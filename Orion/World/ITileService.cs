using Orion.Framework;

namespace Orion.World {
    /// <summary>
    /// Provides access to Terraria's tiles.
    /// </summary>
    public interface ITileService : IService {
        /// <summary>
        /// Gets the width of the tiles.
        /// </summary>
        /// <remarks>This corresponds to Terraria.Main.maxTilesX.</remarks>
        int Width { get; }

        /// <summary>
        /// Gets the height of the tiles.
        /// </summary>
        /// <remarks>This corresponds to Terraria.Main.maxTilesY.</remarks>
        int Height { get; }

        /// <summary>
        /// Gets or sets the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        /// <remarks>For performance reasons, we don't bother bounds checking the coordinates.</remarks>
        Tile this[int x, int y] { get; set; }
    }
}
