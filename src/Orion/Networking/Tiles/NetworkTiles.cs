// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.Linq;
using Orion.Events;

namespace Orion.Networking.Tiles {
    /// <summary>
    /// Represents a section of tiles that are transmitted over the network.
    /// </summary>
    public class NetworkTiles : IDirtiable {
        private readonly NetworkTile[,] _tiles;
        private bool _isDirty;

        /// <inheritdoc />
        public bool IsDirty => _isDirty || _tiles.Cast<NetworkTile>().Any(t => t?.IsDirty == true);

        /// <summary>
        /// Gets or sets the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public NetworkTile this[int x, int y] {
            get => _tiles[x, y];
            set {
                _tiles[x, y] = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => _tiles.GetLength(0);

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => _tiles.GetLength(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTiles"/> class with the specified dimensions.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> are negative.
        /// </exception>
        public NetworkTiles(int width, int height) {
            if (width < 0) throw new ArgumentOutOfRangeException(nameof(width), "Width is negative.");
            if (height < 0) throw new ArgumentOutOfRangeException(nameof(height), "Height is negative.");

            _tiles = new NetworkTile[width, height];
        }

        /// <inheritdoc />
        public void Clean() {
            _isDirty = false;
            foreach (var tile in _tiles.Cast<NetworkTile>()) {
                tile?.Clean();
            }
        }
    }
}
