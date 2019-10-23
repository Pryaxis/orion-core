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
using Destructurama.Attributed;
using Orion.Projectiles;
using Orion.Utils;

namespace Orion.Events.Projectiles {
    /// <summary>
    /// An event that occurs when an projectile's defaults are being set. This event can be canceled and modified.
    /// </summary>
    /// <remarks>
    /// This event occurs when an projectile is being created, which can happen:
    /// <list type="bullet">
    /// <item>
    /// <description>When the server starts up, where all projectiles are initialized.</description>
    /// </item>
    /// <item>
    /// <description>When a projectile spawns in the world.</description>
    /// </item>
    /// </list>
    /// </remarks>
    [Event("proj-defaults", IsVerbose = true)]
    public sealed class ProjectileDefaultsEvent : ProjectileEvent, ICancelable, IDirtiable {
        private ProjectileType _projectileType;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the projectile type that the projectile's defaults are being set to.
        /// </summary>
        /// <value>The projectile type that the item's defaults are being set to.</value>
        public ProjectileType ProjectileType {
            get => _projectileType;
            set {
                _projectileType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileDefaultsEvent"/> class with the specified projectile
        /// and projectile type.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        /// <param name="projectileType">The projectile type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is <see langword="null"/>.</exception>
        public ProjectileDefaultsEvent(IProjectile projectile, ProjectileType projectileType) : base(projectile) {
            _projectileType = projectileType;
        }

        /// <inheritdoc/>
        public void Clean() => IsDirty = false;
    }
}
