using System;
using System.Collections.Generic;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a mechanism for managing signs.
    /// </summary>
    public interface ISignService : IReadOnlyList<ISign>, IService {
        /// <summary>
        /// Attempts to add a sign at the given coordinates with the type and style.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting chest, or <c>null</c> if none was added.</returns>
        ISign AddSign(int x, int y);

        /// <summary>
        /// Gets the sign with the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The sign, or <c>null</c> if there is none.</returns>
        ISign GetSign(int x, int y);

        /// <summary>
        /// Removes the given sign.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sign"/> is <c>null</c>.</exception>
        /// <returns>A value indicating whether or not the sign was successfully removed.</returns>
        bool RemoveSign(ISign sign);
    }
}
