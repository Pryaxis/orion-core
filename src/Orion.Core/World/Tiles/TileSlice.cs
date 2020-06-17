// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;

namespace Orion.Core.World.Tiles
{
    /// <summary>
    /// Represents a slice of tiles backed by a two-dimensional tile array.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public sealed class TileSlice : ITileSlice
    {
        private readonly Tile[,] _tiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileSlice"/> class with the specified dimensions.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> are negative.
        /// </exception>
        public TileSlice(int width, int height)
        {
            if (width < 0)
            {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(width), "Width is negative");
            }

            if (height < 0)
            {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(height), "Height is negative");
            }

            _tiles = new Tile[width, height];
        }

        /// <inheritdoc/>
        public ref Tile this[int x, int y] => ref _tiles[x, y];

        /// <inheritdoc/>
        public int Width => _tiles.GetLength(0);

        /// <inheritdoc/>
        public int Height => _tiles.GetLength(1);
    }
}
