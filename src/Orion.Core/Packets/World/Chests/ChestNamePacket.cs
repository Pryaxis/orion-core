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
using System.Runtime.InteropServices;
using System.Text;

namespace Orion.Core.Packets.World.Chests
{
    /// <summary>
    /// A packet sent from the client to the server to request a chest's name or from the server to the client to set a
    /// chest's name.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ChestNamePacket : IPacket
    {
        [field: FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the chest index. If <c>-1</c> and read in <see cref="PacketContext.Server"/>, then the chest
        /// index is retrieved using the chest's coordinates.
        /// </summary>
        /// <value>The chest index.</value>
        [field: FieldOffset(0)] public short ChestIndex { get; set; }

        /// <summary>
        /// Gets or sets the chest's X coordinate.
        /// </summary>
        /// <value>The chest's X coordinate.</value>
        [field: FieldOffset(2)] public short X { get; set; }

        /// <summary>
        /// Gets or sets the chest's Y coordinate.
        /// </summary>
        /// <value>The chest's Y coordinate.</value>
        [field: FieldOffset(4)] public short Y { get; set; }

        /// <summary>
        /// Gets or sets the chest's name. This is only applicable if read in <see cref="PacketContext.Client"/> or
        /// written in <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The chest's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name
        {
            get => _name ?? string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ChestName;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            var index = span.Read(ref this.AsRefByte(0), 6);
            return context == PacketContext.Server ? index : index + span[index..].Read(Encoding.UTF8, out _name);
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            var index = span.Write(ref this.AsRefByte(0), 6);
            return context == PacketContext.Client ? index : index + span[index..].Write(Name, Encoding.UTF8);
        }
    }
}
