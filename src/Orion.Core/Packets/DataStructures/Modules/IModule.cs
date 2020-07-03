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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// Represents a module, a specific form of communication between the server and its clients.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the module's ID.
        /// </summary>
        /// <value>The module's ID.</value>
        public ModuleId Id { get; }

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
        public int ReadBody(Span<byte> span, PacketContext context);

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
        public int WriteBody(Span<byte> span, PacketContext context);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IModule"/> interface.
    /// </summary>
    public static class IModuleExtensions
    {
        /// <summary>
        /// Writes the module to the given <paramref name="span"/> in the specified <paramref name="context"/>. Returns
        /// the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <typeparam name="TModule">The type of module.</typeparam>
        /// <param name="module">The module.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="module"/> is <see langword="null"/>.</exception>
        public static int Write<TModule>(this TModule module, Span<byte> span, PacketContext context)
            where TModule : IModule
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            Debug.Assert(span.Length >= 2);

            // Write the module ID with no bounds checking since we need to perform bounds checking later anyways.
            Unsafe.WriteUnaligned(ref span.At(0), module.Id);

            return 2 + module.WriteBody(span[2..], context);
        }
    }
}
