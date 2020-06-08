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
using Destructurama.Attributed;
using Orion.Projectiles;
using Serilog.Events;

namespace Orion.Events.Projectiles {
    /// <summary>
    /// An event that occurs when a projectile's defaults are being set: i.e., when a projectile's stats are being
    /// initialized. This event can be canceled.
    /// </summary>
    [Event("proj-defaults", LoggingLevel = LogEventLevel.Verbose)]
    public sealed class ProjectileDefaultsEvent : ProjectileEvent, ICancelable {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileDefaultsEvent"/> class with the specified
        /// <paramref name="projectile"/>.
        /// </summary>
        /// <param name="projectile">The projectile whose defaults are being set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="projectile"/> is <see langword="null"/>.</exception>
        public ProjectileDefaultsEvent(IProjectile projectile) : base(projectile) { }

        /// <summary>
        /// Gets or sets the projectile ID that the projectile's defaults are being set to.
        /// </summary>
        /// <value>The projectile ID that the projectile's defaults are being set to.</value>
        public ProjectileId Id { get; set; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }
    }
}
