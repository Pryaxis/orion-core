// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

namespace Orion.Projectiles.Events {
    /// <summary>
    /// Provides data for the see <see cref="IProjectileService.SetProjectileDefaults"/> handlers.
    /// </summary>
    public sealed class SetProjectileDefaultsEventArgs : EventArgs {
        /// <summary>
        /// Gets the projectile that had its defaults set.
        /// </summary>
        public IProjectile Projectile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetProjectileDefaultsEventArgs"/> class with the specified
        /// projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is null.</exception>
        public SetProjectileDefaultsEventArgs(IProjectile projectile) {
            Projectile = projectile ?? throw new ArgumentNullException(nameof(projectile));
        }
    }
}
