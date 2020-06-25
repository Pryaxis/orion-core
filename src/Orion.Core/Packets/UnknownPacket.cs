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

namespace Orion.Core.Packets
{
    /// <summary>
    /// An unknown packet.
    /// </summary>
    public class UnknownPacket : IPacket
    {
        private readonly byte[] _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPacket"/> class with the specified packet
        /// <paramref name="length"/> and <paramref name="id"/>.
        /// </summary>
        /// <param name="length">The packet length.</param>
        /// <param name="id">The packet ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is too small.</exception>
        public UnknownPacket(ushort length, PacketId id)
        {
            if (length < IPacket.HeaderSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length), $"Length is too small (expected: >= {IPacket.HeaderSize})");
            }

            _data = new byte[length - IPacket.HeaderSize];
            Id = id;
        }

        /// <inheritdoc/>
        public PacketId Id { get; }

        /// <summary>
        /// Gets the packet's data.
        /// </summary>
        /// <value>The packet's data.</value>
        public Span<byte> Data => _data;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) =>
            _data.Length == 0 ? 0 : span.Read(ref _data[0], _data.Length);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) =>
            _data.Length == 0 ? 0 : span.Write(ref _data[0], _data.Length);
    }
}
