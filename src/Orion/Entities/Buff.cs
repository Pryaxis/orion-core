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
using System.IO;
using Orion.Packets.Extensions;

namespace Orion.Entities {
    /// <summary>
    /// Represents a buff, which consists of a buff type along with a duration.
    /// </summary>
    /// <remarks>
    /// Buffs can be applied to both players and NPCs. They typically have small effects on entities.
    /// </remarks>
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types",
        Justification = "Buffs will not be compared.")]
    public readonly struct Buff {
        private readonly TimeSpan _duration;

        /// <summary>
        /// Gets the buff's type.
        /// </summary>
        /// <value>The buff's type.</value>
        public BuffType BuffType { get; }

        /// <summary>
        /// Gets the buff's duration.
        /// </summary>
        /// <value>The buff's duration.</value>
        public TimeSpan Duration => _duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buff"/> structure with the specified buff type and duration.
        /// </summary>
        /// <param name="buffType">The buff type.</param>
        /// <param name="duration">The duration.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> is negative.</exception>
        public Buff(BuffType buffType, TimeSpan duration) {
            if (duration < TimeSpan.Zero) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(duration), "Value is negative.");
            }

            BuffType = buffType;
            _duration = duration;
        }

        /// <summary>
        /// Reads and returns a buff from a <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="numOfDurationBytes">
        /// The number of bytes to spend on reading the buff's duration. This can be either <c>2</c> or <c>4</c>.
        /// </param>
        /// <returns>The buff.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numOfDurationBytes"/> is neither <c>2</c> nor <c>4</c>.
        /// </exception>
        public static Buff ReadFromReader(BinaryReader reader, int numOfDurationBytes) {
            if (reader is null) {
                throw new ArgumentNullException(nameof(reader));
            }

            if (numOfDurationBytes != 2 && numOfDurationBytes != 4) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(numOfDurationBytes), "Value is neither 2 nor 4.");
            }

            return new Buff((BuffType)reader.ReadByte(), reader.ReadTimeSpan(numOfDurationBytes));
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{BuffType} for {Duration}";

        /// <summary>
        /// Writes the buff to a <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="numOfDurationBytes">
        /// The number of bytes to spend on writing the buff's duration. This can be either <c>2</c> or <c>4</c>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numOfDurationBytes"/> is neither <c>2</c> nor <c>4</c>.
        /// </exception>
        public void WriteToWriter(BinaryWriter writer, int numOfDurationBytes) {
            if (writer is null) {
                throw new ArgumentNullException(nameof(writer));
            }

            if (numOfDurationBytes != 2 && numOfDurationBytes != 4) {
                // Not localized because this string is developer-facing.
                throw new ArgumentOutOfRangeException(nameof(numOfDurationBytes), "Value is neither 2 nor 4.");
            }

            writer.Write((byte)BuffType);
            writer.Write(in _duration, numOfDurationBytes);
        }
    }
}
