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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Orion.Entities {
    /// <summary>
    /// Represents a buff.
    /// </summary>
    // TODO: write WriterToWriter and ReadFromReader methods.
    public readonly struct Buff {
        /// <summary>
        /// Gets the buff type.
        /// </summary>
        public BuffType BuffType { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> structure with the specified buff type and duration.
        /// </summary>
        /// <param name="buffType">The buff type.</param>
        /// <param name="duration">The duration.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> is negative.</exception>
        public Buff(BuffType buffType, TimeSpan duration) {
            if (duration < TimeSpan.Zero) {
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration cannot be negative.");
            }

            BuffType = buffType;
            Duration = duration;
        }

        /// <inheritdoc />
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{BuffType} for {Duration}";
    }
}
