using System.ComponentModel;
using Orion.World.Tiles;

namespace Orion.World.Events {
    /// <summary>
    /// Provides data for the <see cref="IWorldService.UpdatingHardmodeBlock"/> event.
    /// </summary>
    public sealed class UpdatingHardmodeBlockEventArgs : HandledEventArgs {
        /// <summary>
        /// Gets the block's X coordinate.
        /// </summary>
        public int BlockX { get; }

        /// <summary>
        /// Gets the block's Y coordinate.
        /// </summary>
        public int BlockY { get; }

        /// <summary>
        /// Gets or sets the type that the block will become.
        /// </summary>
        public BlockType BlockType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingHardmodeBlockEventArgs"/> class with the specified
        /// coordinates and block type.
        /// </summary>
        /// <param name="blockX">The X coordinate.</param>
        /// <param name="blockY">The Y coordinate.</param>
        /// <param name="type">The new block type.</param>
        public UpdatingHardmodeBlockEventArgs(int blockX, int blockY, BlockType type) {
            BlockX = blockX;
            BlockY = blockY;
            BlockType = type;
        }
    }
}
