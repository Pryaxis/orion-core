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
using Orion.Projectiles;

namespace Orion.Events.Projectiles {
    /// <summary>
    /// Provides the base class for a projectile-related event.
    /// </summary>
    public abstract class ProjectileEvent : Event {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileEvent"/> class with the specified
        /// <paramref name="projectile"/>.
        /// </summary>
        /// <param name="projectile">The projectile involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is <see langword="null"/>.</exception>
        protected ProjectileEvent(IProjectile projectile) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        }

        /// <summary>
        /// Gets the projectile involved in the event.
        /// </summary>
        /// <value>The projectile involved in the event.</value>
        public IProjectile Projectile { get; }
    }
}
