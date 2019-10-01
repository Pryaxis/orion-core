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
using Microsoft.Xna.Framework;
using Orion.Utils;

namespace Orion.Entities {
    /// <summary>
    /// Represents a Terraria entity.
    /// </summary>
    public interface IEntity : IAnnotatable {
        /// <summary>
        /// Gets the entity's index. A value of -1 indicates that the entity is not "real".
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the entity's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null" />.</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity's position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the entity's velocity.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the entity's size.
        /// </summary>
        Vector2 Size { get; set; }
    }
}
