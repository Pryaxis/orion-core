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
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures.Modules;

namespace Orion.Core.Packets
{
    /// <summary>
    /// A packet sent in the form of a module.
    /// </summary>
    /// <typeparam name="TModule">The type of module.</typeparam>
    public struct ModulePacket<TModule> : IPacket where TModule : notnull, IModule
    {
        private TModule _module;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
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
            // `UnknownModule` is a special case, since we need to always reconstruct it in case the `ModulePacket<>`
            // instance is being reused.
            if (typeof(TModule) == typeof(UnknownModule))
            {
                Debug.Assert(span.Length >= 2);

                ref var header = ref MemoryMarshal.GetReference(span);

                // Read the module ID with no bounds checking since we need to perform bounds checking later
                // anyways.
                var moduleId = Unsafe.ReadUnaligned<ModuleId>(ref header);

                _module = (TModule)(object)new UnknownModule(span.Length - 2, moduleId);
            }
            else if (_module is null)
            {
                _module = (TModule)Activator.CreateInstance(typeof(TModule));
            }

            return 2 + _module.ReadBody(span[2..], context);
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            if (_module is null)
            {
                throw new InvalidOperationException("Module is null");
            }

            return _module.Write(span, context);
        }
    }
}
