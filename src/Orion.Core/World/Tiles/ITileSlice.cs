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
using System.Diagnostics;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.Players;

namespace Orion.Core.World.Tiles {
    /// <summary>
    /// Represents a two-dimensional slice of tiles.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe.
    /// 
    /// A tile slice can be used to efficiently pass tiles by reference in, e.g., a<see cref="TileSquarePacket"/>
    /// instance or the <see cref="PlayerExtensions.SendTiles(IPlayer, int, int, ITileSlice)"/> extension method.
    /// 
    /// The <see cref="TileSliceExtensions.Slice(ITileSlice, int, int, int, int)"/> extension method allows zero-copy
    /// subslices to be obtained.
    /// </remarks>
    public interface ITileSlice {
        /// <summary>
        /// Gets a reference to the tile at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>A reference to the tile at the given coordinates.</returns>
        /// <remarks>
        /// For optimization purposes, implementations may not be required to perform any range checking. With some
        /// implementations, an <see cref="IndexOutOfRangeException"/> or <see cref="AccessViolationException"/> may be
        /// expected.
        /// </remarks>
        ref Tile this[int x, int y] { get; }

        /// <summary>
        /// Gets the slice's width.
        /// </summary>
        /// <value>The slice's width.</value>
        int Width { get; }

        /// <summary>
        /// Gets the slice's height.
        /// </summary>
        /// <value>The slice's height.</value>
        int Height { get; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="ITileSlice"/> interface.
    /// </summary>
    public static class TileSliceExtensions {
        /// <summary>
        /// Returns a slice of the <paramref name="tiles"/> with the specified starting coordinates and dimensions.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        /// <param name="startX">The slice's starting X coordinate.</param>
        /// <param name="startY">The slice's starting Y coordinate.</param>
        /// <param name="width">The slice's width.</param>
        /// <param name="height">The slice's height.</param>
        /// <returns>
        /// The slice of the <paramref name="tiles"/> with the specified starting coordinates and dimensions.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="tiles"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startX"/>, <paramref name="startY"/>, <paramref name="width"/>, or <paramref name="height"/>
        /// are out of range.
        /// </exception>
        public static ITileSlice Slice(this ITileSlice tiles, int startX, int startY, int width, int height) {
            if (tiles is null) {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (startX < 0 || startX >= tiles.Width) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(
                    nameof(startX), $"Start X out of range (expected: 0 to {tiles.Width - 1})");
            }

            if (startY < 0 || startY >= tiles.Height) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(
                    nameof(startY), $"Start Y out of range (expected: 0 to {tiles.Height - 1})");
            }

            if (width <= 0 || startX + width > tiles.Width) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(
                    nameof(width), $"Width out of range (expected: 1 to {tiles.Width - startX})");
            }

            if (height <= 0 || startY + height > tiles.Height) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(
                    nameof(height), $"Height out of range (expected: 1 to {tiles.Height - startY})");
            }

            return new NestedTileSlice(tiles, startX, startY, width, height);
        }

        private sealed class NestedTileSlice : ITileSlice {
            private readonly ITileSlice _tiles;
            private readonly int _startX;
            private readonly int _startY;

            public NestedTileSlice(ITileSlice tiles, int startX, int startY, int width, int height) {
                Debug.Assert(tiles != null);
                Debug.Assert(startX >= 0 && startX + width <= tiles.Width);
                Debug.Assert(startY >= 0 && startY + height <= tiles.Height);
                Debug.Assert(width > 0);
                Debug.Assert(height > 0);

                _tiles = tiles;
                _startX = startX;
                _startY = startY;
                Width = width;
                Height = height;
            }

            public ref Tile this[int x, int y] => ref _tiles[_startX + x, _startY + y];
            public int Width { get; }
            public int Height { get; }
        }
    }
}
