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

namespace Orion.Packets {
    /// <summary>
    /// The exception thrown when a packet fails to be processed properly.
    /// </summary>
    [Serializable, ExcludeFromCodeCoverage]
    public sealed class PacketException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketException"/> class.
        /// </summary>
        public PacketException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public PacketException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketException"/> class with the specified message
        /// and inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PacketException(string message, Exception innerException) : base(message, innerException) { }
    }
}
