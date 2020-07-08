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
using System.Diagnostics.CodeAnalysis;
using Destructurama.Attributed;

namespace Orion.Core.Entities
{
    /// <summary>
    /// Represents a buff.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A buff is a positive (or negative, in the case of a debuff) status effect which can be applied to players and
    /// NPCs. See <a href="https://terraria.gamepedia.com/Buffs">here</a> for more information on buffs.
    /// </para>
    /// <para>
    /// Each buff consists of a <see cref="BuffId"/>, which specifies the type of buff, along with a duration indicating
    /// the amount of time remaining on the buff.
    /// </para>
    /// </remarks>
    public readonly struct Buff : IEquatable<Buff>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> structure with the specified buff <paramref name="id"/>
        /// and <paramref name="ticks"/>.
        /// </summary>
        /// <param name="id">The buff ID.</param>
        /// <param name="ticks">The buff duration, in ticks.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="ticks"/> is negative.</exception>
        public Buff(BuffId id, int ticks)
        {
            if (ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticks), "Ticks is negative");
            }

            Id = id;
            Ticks = ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> structure with the specified buff <paramref name="id"/>
        /// and <paramref name="duration"/>.
        /// </summary>
        /// <param name="id">The buff ID.</param>
        /// <param name="duration">The buff duration.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> is negative.</exception>
        public Buff(BuffId id, TimeSpan duration)
        {
            if (duration < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration), "Duration is negative");
            }

            Id = id;
            Ticks = (int)(duration.TotalSeconds * 60.0);
        }

        /// <summary>
        /// Gets the buff ID.
        /// </summary>
        /// <value>The buff ID.</value>
        public BuffId Id { get; }

        /// <summary>
        /// Gets the buff duration, in ticks.
        /// </summary>
        /// <value>The buff duration, in ticks.</value>
        public int Ticks { get; }

        /// <summary>
        /// Gets the buff duration.
        /// </summary>
        /// <value>The buff duration.</value>
        [NotLogged]
        public TimeSpan Duration => TimeSpan.FromSeconds(Ticks / 60.0);

        /// <summary>
        /// Gets a value indicating whether the buff is empty.
        /// </summary>
        /// <value><see langword="true"/> if the buff is empty; otherwise, <see langword="false"/>.</value>
        [NotLogged]
        public bool IsEmpty => Id == BuffId.None || Ticks == 0;

        /// <summary>
        /// Gets a value indicating whether the buff is a debuff.
        /// </summary>
        /// <value><see langword="true"/> if the buff is a debuff; otherwise, <see langword="false"/>.</value>
        [NotLogged]
        public bool IsDebuff => Id.IsDebuff();

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Buff other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Buff other) => IsEmpty ? other.IsEmpty : (Id == other.Id && Duration == other.Duration);

        /// <summary>
        /// Returns the hash code of the buff.
        /// </summary>
        /// <returns>The hash code of the buff.</returns>
        public override int GetHashCode() => IsEmpty ? 0 : HashCode.Combine(Id, Duration);

        /// <summary>
        /// Returns a string representation of the buff.
        /// </summary>
        /// <returns>A string representation of the buff.</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString() => IsEmpty ? "<none>" : $"{Id} for {Duration:mm:ss}";

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left buff.</param>
        /// <param name="right">The right buff.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public static bool operator ==(Buff left, Buff right) => left.Equals(right);

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
        /// </summary>
        /// <param name="left">The left buff.</param>
        /// <param name="right">The right buff.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public static bool operator !=(Buff left, Buff right) => !(left == right);
    }
}
