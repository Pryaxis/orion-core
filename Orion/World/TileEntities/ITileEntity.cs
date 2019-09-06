namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a generalized tile entity.
    /// </summary>
    public interface ITileEntity {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        int Y { get; set; }
    }
}
