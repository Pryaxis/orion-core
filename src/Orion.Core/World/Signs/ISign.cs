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
using Orion.Core.World.TileEntities;

namespace Orion.Core.World.Signs
{
    /// <summary>
    /// Represents a Terraria sign.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe: i.e., each operation on the sign should be atomic.
    /// </remarks>
    public interface ISign : ITileEntity
    {
        /// <summary>
        /// Gets or sets the sign's text.
        /// </summary>
        /// <value>The sign's text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Text { get; set; }
    }
}
