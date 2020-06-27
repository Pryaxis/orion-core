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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures.Modules;

namespace Orion.Core.Packets
{
    /// <summary>
    /// A packet sent in the form of a module.
    /// </summary>
    /// <typeparam name="TModule">The type of module.</typeparam>
    public sealed class ModulePacket<TModule> : IPacket where TModule : notnull, IModule
    {
        private TModule _module = default!;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>The module.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The module is <see langword="null"/>.</exception>
        public TModule Module
        {
            get => _module ?? throw new InvalidOperationException("Module is null");
            set => _module = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.Module;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            // Initialize the `_module` field if it is `null`. If `TModule` is `UnknownModule`, then this is a special
            // case.
            if (_module is null)
            {
                var type = typeof(TModule);
                if (type == typeof(UnknownModule))
                {
                    var id = Unsafe.ReadUnaligned<ModuleId>(ref MemoryMarshal.GetReference(span));
                    _module = (TModule)(object)new UnknownModule(span.Length - 2, id);
                }
                else
                {
                    _module = (TModule)Activator.CreateInstance(type);
                }
            }

            return 2 + _module.ReadBody(span[2..], context);
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module is null");
            }

            ref var header = ref MemoryMarshal.GetReference(span);

            // Write the module ID with no bounds checking since we need to perform bounds checking later anyways.
            Unsafe.WriteUnaligned(ref header, _module.Id);

            return 2 + _module.WriteBody(span[2..], context);
        }
    }
}
