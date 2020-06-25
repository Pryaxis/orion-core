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

namespace Orion.Core.Packets.Modules
{
    /// <summary>
    /// Represents a module, a specific form of communication between the server and its clients.
    /// </summary>
    public interface IModule
    {
        private static readonly IDictionary<ModuleId, Func<IModule>> _constructors =
            new Dictionary<ModuleId, Func<IModule>>
            {
                [ModuleId.Chat] = () => new ChatModule(),
            };

        /// <summary>
        /// The module's header size.
        /// </summary>
        public const int HeaderSize = sizeof(ModuleId);

        /// <summary>
        /// Gets the module's ID.
        /// </summary>
        /// <value>The module's ID.</value>
        ModuleId Id { get; }

        /// <summary>
        /// Reads the module's body from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read from the
        /// <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        int ReadBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the module's body to the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>. Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        /// <remarks>
        /// Implementations may not perform any bounds checking on <paramref name="span"/>.
        /// </remarks>
        int WriteBody(Span<byte> span, PacketContext context);

        /// <summary>
        /// Reads a packet from the given <paramref name="span"/> in the specified <paramref name="context"/>. Returns
        /// the number of bytes read from the <paramref name="span"/>.
        /// </summary>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <param name="module">The resulting packet.</param>
        /// <returns>The number of bytes read from the <paramref name="span"/>.</returns>
        public static int Read(Span<byte> span, PacketContext context, out IModule module)
        {
            // Since we know that the span must have space for the module header, we read it with zero bounds checking.
            var moduleLength = (ushort)span.Length;
            var id = Unsafe.ReadUnaligned<ModuleId>(ref MemoryMarshal.GetReference(span));

            module = _constructors.TryGetValue(id, out var ctor) ? ctor() : new UnknownModule(moduleLength, id);
            var moduleBodyLength = module.ReadBody(span[HeaderSize..], context);
            Debug.Assert(moduleBodyLength + HeaderSize == moduleLength);

            return moduleLength;
        }
    }

    /// <summary>
    /// Provides extensions for the <see cref="IModule"/> interface.
    /// </summary>
    public static class IModuleExtensions
    {
        /// <summary>
        /// Writes the module to the given <paramref name="span"/> in the specified <paramref name="context"/>.
        /// Returns the number of bytes written to the <paramref name="span"/>.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int Write(this IModule module, Span<byte> span, PacketContext context)
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            var moduleLength = IModule.HeaderSize + module.WriteBody(span[IModule.HeaderSize..], context);

            // Since we know that the span must have space for the module header, we write it with zero bounds checking.
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), module.Id);

            return moduleLength;
        }

        /// <summary>
        /// Writes the <paramref name="module"/> reference to the given <paramref name="span"/> with the specified
        /// packet <paramref name="context"/>, including the module header. Returns the number of bytes written to the
        /// <paramref name="span"/>.
        /// </summary>
        /// <typeparam name="TModule">The type of module.</typeparam>
        /// <param name="module">The module reference.</param>
        /// <param name="span">The span to write to.</param>
        /// <param name="context">The packet context to use when writing.</param>
        /// <returns>The number of bytes written to the <paramref name="span"/>.</returns>
        public static int WriteWithHeader<TModule>(ref this TModule module, Span<byte> span, PacketContext context)
            where TModule : struct, IModule
        {
            Unsafe.WriteUnaligned(ref span[0], module.Id);
            return IModule.HeaderSize + module.WriteBody(span[IModule.HeaderSize..], context);
        }
    }
}
