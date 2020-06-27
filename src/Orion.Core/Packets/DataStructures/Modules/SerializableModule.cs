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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// Provides the base class for a serializable module, a specific form of communication between the server and its
    /// clients.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public abstract class SerializableModule
    {
        private static readonly IDictionary<ModuleId, Func<SerializableModule>> _ctors =
            new Dictionary<ModuleId, Func<SerializableModule>>
            {
                [ModuleId.Chat] = () => new Chat()
            };

        /// <summary>
        /// Gets the module's ID.
        /// </summary>
        /// <value>The module's ID.</value>
        public abstract ModuleId Id { get; }

        /// <summary>
        /// Writes the module to the given <paramref name="span"/> in the specified <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public int Write(Span<byte> span, PacketContext context)
        {
            Debug.Assert(span.Length >= 2);

            // Write the module ID with no bounds checking since we need to perform bounds checking later anyways.
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), Id);

            return 2 + WriteBody(span[2..], context);
        }

        /// <summary>
        /// Reads the module's body from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read from the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations might not perform bounds checking on the <paramref name="span"/>.
        /// </remarks>
        protected abstract int ReadBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the module's body to the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>. Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations might not perform bounds checking on the <paramref name="span"/>.
        /// </remarks>
        protected abstract int WriteBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Reads a serializable module from the given <paramref name="span"/> in the specified
        /// <paramref name="context"/>. Returns the number of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <param name="module">The resulting module.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, PacketContext context, out SerializableModule module)
        {
            Debug.Assert(span.Length >= 2);

            // Read the module ID with no bounds checking since we need to perform bounds checking later anyways.
            var id = Unsafe.ReadUnaligned<ModuleId>(ref MemoryMarshal.GetReference(span));

            module = _ctors.TryGetValue(id, out var ctor) ? ctor() : new UnknownModule(span.Length - 2, id);
            return 2 + module.ReadBody(span[2..], context);
        }
    }
}
