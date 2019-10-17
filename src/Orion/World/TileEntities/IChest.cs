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
using TerrariaChest = Terraria.Chest;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a Terraria chest.
    /// </summary>
    /// <remarks>
    /// Chests are the main form of permanent storage for items. They may only be accessed by a single player at a time
    /// and cannot be broken while still storing items.
    /// </remarks>
    public interface IChest : ITileEntity, IWrapping<TerrariaChest?> {
        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets the chest's items.
        /// </summary>
        /// <value>The chest's items.</value>
        IReadOnlyArray<IItem> Items { get; }
    }
}
