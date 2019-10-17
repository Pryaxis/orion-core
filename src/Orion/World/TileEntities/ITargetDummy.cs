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

using Orion.Utils;
using TerrariaTargetDummy = Terraria.GameContent.Tile_Entities.TETrainingDummy;

namespace Orion.World.TileEntities {
    /// <summary>
    /// Represents a Terraria target dummy.
    /// </summary>
    /// <remarks>Target dummies are tile entities that provide a target dummy NPC.</remarks>
    public interface ITargetDummy : ITileEntity, IWrapping<TerrariaTargetDummy> {
        /// <summary>
        /// Gets or sets the target dummy's NPC index.
        /// </summary>
        /// <value>The target dummy's NPC index.</value>
        int NpcIndex { get; set; }
    }
}
