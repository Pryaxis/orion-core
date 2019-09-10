using Orion.Utils;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a generalized tile entity.
    /// </summary>
    public interface ITileEntity : IAnnotatable {
        /// <summary>
        /// Gets the tile entity's index.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets or sets the tile entity's X coordinate.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the tile entity's Y coordinate.
        /// </summary>
        int Y { get; set; }
    }
}
