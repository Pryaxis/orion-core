// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.Runtime.InteropServices;
using Destructurama.Attributed;
using Orion.Utils;
using Orion.World.Tiles;

namespace Orion.Packets.World.Tiles {
    /// <summary>
    /// Represents a section of tiles that are transmitted over the network.
    /// </summary>
    public sealed class NetworkTiles : IDirtiable {
        private readonly Tile[] _tiles;
        private readonly Tile[] _cleanTiles;

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty {
            get {
                // Convert the tiles to a span of bytes, and the clean tiles to a span of bytes. This allows us to use
                // SequenceEqual, which is highly optimized for the purpose of comparing the two. This is significantly
                // faster than just comparing the two naively.
                var span = MemoryMarshal.AsBytes(_tiles.AsSpan());
                var cleanSpan = MemoryMarshal.AsBytes(_cleanTiles.AsSpan());
                return !span.SequenceEqual(cleanSpan);
            }
        }

        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="x"/> or <paramref name="y"/> are out of range.
        /// </exception>
        public ref Tile this[int x, int y] {
            get {
                if (x < 0 || x >= Width) {
                    throw new ArgumentOutOfRangeException(nameof(x));
                }

                if (y < 0 || y >= Height) {
                    throw new ArgumentOutOfRangeException(nameof(y));
                }

                return ref _tiles[y * Width + x];
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTiles"/> class with the specified dimensions.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> or <paramref name="height"/> are negative.
        /// </exception>
        public NetworkTiles(int width, int height) {
            if (width < 0) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(width), "Value is negative.");
            }

            if (height < 0) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(height), "Value is negative.");
            }

            _tiles = new Tile[width * height];
            _cleanTiles = new Tile[width * height];
            Width = width;
            Height = height;
        }

        /// <inheritdoc/>
        public void Clean() {
            // Convert the tiles to a span of bytes, and the clean tiles to a span of bytes. This allows us to use
            // CopyTo, which is highly optimized for the purpose of copying. This is significantly faster than just
            // copying naively.
            var span = MemoryMarshal.AsBytes(_tiles.AsSpan());
            var cleanSpan = MemoryMarshal.AsBytes(_cleanTiles.AsSpan());
            span.CopyTo(cleanSpan);
        }
    }
}
