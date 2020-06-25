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

namespace Orion.Core.Packets.Modules
{
    /// <summary>
    /// A packet sent in the form of a module.
    /// </summary>
    public sealed class ModulePacket : IPacket
    {
        private IModule _module = EmptyModule.Instance;

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>The module.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public IModule Module
        {
            get => _module;
            set => _module = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.Module;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => IModule.Read(span, context, out _module);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => _module.Write(span, context);

        private sealed class EmptyModule : IModule
        {
            public static EmptyModule Instance { get; } = new EmptyModule();

            private EmptyModule()
            {
            }

            ModuleId IModule.Id => (ModuleId)65535;

            int IModule.ReadBody(Span<byte> span, PacketContext context) => 0;
            int IModule.WriteBody(Span<byte> span, PacketContext context) => 0;
        }
    }
}
