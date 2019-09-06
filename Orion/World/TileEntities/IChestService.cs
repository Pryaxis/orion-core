using System;
using System.Collections.Generic;
using Orion.World.Tiles;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a mechanism for managing chests.
    /// </summary>
    public interface IChestService : IReadOnlyList<IChest>, IService {
        /// <summary>
        /// Attempts to place a chest at the given coordinates with the type and style.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="type">
        /// The type. Must be <see cref="BlockType.Containers"/>, <see cref="BlockType.Dressers"/>, or
        /// <see cref="BlockType.Containers2"/>.
        /// </param>
        /// <param name="style">The style.</param>
        /// <returns>The resulting chest, or <c>null</c> if none was added.</returns>
        IChest PlaceChest(int x, int y, BlockType type = BlockType.Containers, int style = 0);

        /// <summary>
        /// Gets the chest at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The chest, or <c>null</c> if there is none.</returns>
        IChest GetChest(int x, int y);

        /// <summary>
        /// Removes the given chest.
        /// </summary>
        /// <param name="chest">The chest.</param>
        /// <exception cref="ArgumentNullException"><paramref name="chest"/> is <c>null</c>.</exception>
        /// <returns>A value indicating whether or not the chest was successfully removed.</returns>
        bool RemoveChest(IChest chest);
    }
}
