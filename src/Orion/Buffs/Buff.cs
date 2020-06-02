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

namespace Orion.Buffs {
    /// <summary>
    /// Represents a buff, which consists of a buff ID along with a duration.
    /// </summary>
    public readonly struct Buff {
        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> structure with the specified buff <paramref name="id"/>
        /// and <paramref name="duration"/>.
        /// </summary>
        /// <param name="id">The buff ID.</param>
        /// <param name="duration">The buff duration.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> is non-positive.</exception>
        public Buff(BuffId id, TimeSpan duration) {
            if (duration <= TimeSpan.Zero) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration is non-positive");
            }

            Id = id;
            Duration = duration;
        }

        /// <summary>
        /// Gets the buff ID.
        /// </summary>
        /// <value>The buff ID.</value>
        public BuffId Id { get; }

        /// <summary>
        /// Gets the buff duration.
        /// </summary>
        /// <value>The buff duration.</value>
        public TimeSpan Duration { get; }
    }
}
