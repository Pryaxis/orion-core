using System.ComponentModel;
using Orion.World.Tiles;

namespace Orion.World.Events {
    /// <summary>
    /// Provides data for the <see cref="IWorldService.UpdatingHardmodeTile"/> event.
    /// </summary>
    public sealed class UpdatingHardmodeTileEventArgs : HandledEventArgs
    {
        /// <summary>
        /// Gets the X coordinate of the tile.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y coordinate of the tile.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets or sets the <see cref="Tiles.BlockType"/> that the block will become.
        /// </summary>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingHardmodeTileEventArgs"/> class with the specified
        /// coordinates and block type.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="type">The new block type.</param>
        public UpdatingHardmodeTileEventArgs(int x, int y, BlockType type)
        {
            X = x;
            Y = y;
            BlockType = type;
        }
    }
}
