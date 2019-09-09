using System;

namespace Orion.Players {
    /// <summary>
    /// Represents a buff, which consists of a buff type and a duration.
    /// </summary>
    public struct Buff {
        /// <summary>
        /// Gets the buff type.
        /// </summary>
        public BuffType BuffType { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> struct with the specified buff type and duration.
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
    }
}
