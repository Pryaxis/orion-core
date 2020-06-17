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

namespace Orion.Core.Packets.Modules
{
    /// <summary>
    /// A packet sent in the form of a module.
    /// </summary>
    /// <typeparam name="TModule">The type of module.</typeparam>
    public struct ModulePacket<TModule> : IPacket where TModule : struct, IModule
    {
        /// <summary>
        /// The module.
        /// </summary>
        public TModule Module;

        PacketId IPacket.Id => PacketId.Module;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            // If `TModule` is `UnknownModule`, then we need to set the `Id` property appropriately.
            if (typeof(TModule) == typeof(UnknownModule))
            {
                Unsafe.As<TModule, UnknownModule>(ref Module).Id = Unsafe.ReadUnaligned<ModuleId>(ref span[0]);
            }

            return IModule.HeaderSize + Module.Read(span[IModule.HeaderSize..], context);
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) => Module.WriteWithHeader(span, context);
    }
}
