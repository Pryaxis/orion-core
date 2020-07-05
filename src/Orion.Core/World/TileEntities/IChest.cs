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
using Orion.Core.Items;
using Orion.Core.Utils;

namespace Orion.Core.World.TileEntities
{
    /// <summary>
    /// Represents a Terraria chest.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe: i.e., each operation on the chest should be atomic.
    /// </remarks>
    public interface IChest : ITileEntity
    {
        /// <summary>
        /// Gets or sets the chest's name.
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name { get; set; }

        /// <summary>
        /// Gets the chest's items.
        /// </summary>
        /// <value>The chest's items.</value>
        public IArray<ItemStack> Items { get; }
    }
}
