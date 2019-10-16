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
    /// Provides data for the <see cref="IProjectileService.ProjectileSetDefaults"/> event. This event can be canceled.
    /// </summary>
    [EventArgs("proj-defaults")]
    public sealed class ProjectileSetDefaultsEventArgs : ProjectileEventArgs, ICancelable {
        /// <inheritdoc/>
        public string? CancellationReason { get; set; }
        
        /// <summary>
        /// Gets or sets the projectile type that the projectile's defaults are being set to.
        /// </summary>
        /// <value>The projectile type that the item's defaults are being set to.</value>
        public ProjectileType ProjectileType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileSetDefaultsEventArgs"/> class with the specified
        /// projectile and projectile type.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <param name="projectileType">The projectile type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is <see langword="null"/>.</exception>
        public ProjectileSetDefaultsEventArgs(
                IProjectile projectile, ProjectileType projectileType) : base(projectile) {
            ProjectileType = projectileType;
        }
    }
}
