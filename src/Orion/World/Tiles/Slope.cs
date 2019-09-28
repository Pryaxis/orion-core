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

using System.Diagnostics.CodeAnalysis;

namespace Orion.World.Tiles {
    /// <summary>
    /// Specifies a slope.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum Slope : byte {
        /// <summary>
        /// Indicates no slope.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a top-right slope.
        /// </summary>
        TopRight = 1,

        /// <summary>
        /// Indicates a top-left slope.
        /// </summary>
        TopLeft = 2,

        /// <summary>
        /// Indicates a bottom-right slope.
        /// </summary>
        BottomRight = 3,

        /// <summary>
        /// Indicates a bottom-left slope.
        /// </summary>
        BottomLeft = 4
    }
}
