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
using System.Runtime.CompilerServices;

namespace Orion.Packets.Modules {
    /// <summary>
    /// Represents a module, a specific form of communication between the server and its clients.
    /// </summary>
    public interface IModule {
        /// <summary>
        /// The module's header size.
        /// </summary>
        public const int HeaderSize = sizeof(ModuleId);

        /// <summary>
        /// Gets the module's ID.
        /// </summary>
        ModuleId Id { get; }

        /// <summary>
        /// Reads the module from the given <paramref name="span"/> with the specified packet
        /// <paramref name="context"/>, mutating this instance. Returns the number of bytes read.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="context">The packet context.</param>
        /// <returns>The number of bytes read.</returns>
        int Read(Span<byte> span, PacketContext context);

        /// <summary>
        /// Writes the module to the given <paramref name="span"/> with the specified packet <paramref name="context"/>.
        /// Returns the number of bytes written.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="context">The packet context.</param>
        /// <returns>The number of bytes written.</returns>
        int Write(Span<byte> span, PacketContext context);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IModule"/> interface.
    /// </summary>
    public static class ModuleExtensions {
        /// <summary>
        /// Writes the <paramref name="module"/> reference to the given <paramref name="span"/> with the specified
        /// packet <paramref name="context"/>, including the module header. Returns the number of bytes written.
        /// </summary>
        /// <typeparam name="TModule">The type of module.</typeparam>
        /// <param name="module">The module reference.</param>
        /// <param name="span">The span.</param>
        /// <param name="context">The packet context.</param>
        /// <returns>The number of bytes written.</returns>
        public static int WriteWithHeader<TModule>(ref this TModule module, Span<byte> span, PacketContext context)
                where TModule : struct, IModule {
            Unsafe.WriteUnaligned(ref span[0], module.Id);
            return IModule.HeaderSize + module.Write(span[IModule.HeaderSize..], context);
        }
    }
}
