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
using Orion.Items;
using Orion.Utils;
using TerrariaItemFrame = Terraria.GameContent.Tile_Entities.TEItemFrame;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a Terraria item frame.
    /// </summary>
    public interface IItemFrame : ITileEntity, IWrapping<TerrariaItemFrame> {
        /// <summary>
        /// Gets or sets the item's type.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the item's stack size.
        /// </summary>
        int ItemStackSize { get; set; }

        /// <summary>
        /// Gets or sets the item's prefix.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        ItemPrefix ItemPrefix { get; set; }
    }
}
