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
using System.Runtime.InteropServices;

namespace Orion.Core.Packets.Modules
{
    /// <summary>
    /// An unknown module.
    /// </summary>
    public sealed class UnknownModule : IModule
    {
        private readonly byte[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownModule"/> class with the specified module
        /// <paramref name="length"/> and <paramref name="id"/>.
        /// </summary>
        /// <param name="length">The module length.</param>
        /// <param name="id">The module ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is too small.</exception>
        public UnknownModule(ushort length, ModuleId id)
        {
            if (length < IModule.HeaderSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length), $"Length is too small (expected: >= {IModule.HeaderSize})");
            }

            _data = new byte[length - IModule.HeaderSize];
            Id = id;
        }

        /// <inheritdoc/>
        public ModuleId Id { get; }

        /// <summary>
        /// Gets the module's data.
        /// </summary>
        /// <value>The module's data.</value>
        public unsafe Span<byte> Data => _data;

        int IModule.ReadBody(Span<byte> span, PacketContext context) =>
            _data.Length == 0 ? 0 : span.Read(ref _data[0], _data.Length);

        int IModule.WriteBody(Span<byte> span, PacketContext context) =>
            _data.Length == 0 ? 0 : span.Write(ref _data[0], _data.Length);
    }
}
