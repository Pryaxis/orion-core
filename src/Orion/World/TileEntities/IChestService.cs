using System;
using System.Collections.Generic;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a mechanism for managing chests.
    /// </summary>
    public interface IChestService : IReadOnlyList<IChest>, IService {
        /// <summary>
        /// Attempts to add a chest at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting chest, or <c>null</c> if none was added.</returns>
        IChest AddChest(int x, int y);

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
