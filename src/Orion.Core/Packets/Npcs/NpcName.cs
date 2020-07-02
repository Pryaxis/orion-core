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
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent from the client to the server to request an NPC's name or from the server to the client to set an
    /// NPC's name.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct NpcName : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(2)] private byte _bytes2;  // Used to obtain an interior reference.
        [FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's name. <i>This is only applicable if read in <see cref="PacketContext.Client"/> or
        /// written in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The NPC's name.</value>
        public string Name
        {
            get => _name ??= string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the NPC's variant. <i>This is only applicable if read in <see cref="PacketContext.Client"/> or
        /// written in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The NPC's variant.</value>
        [field: FieldOffset(2)] public int Variant { get; set; }

        PacketId IPacket.Id => PacketId.NpcName;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 2);
            if (context == PacketContext.Server)
            {
                return length;
            }

            length += span[length..].Read(out _name);
            length += span[length..].Read(ref _bytes2, 4);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 2);
            if (context == PacketContext.Client)
            {
                return length;
            }

            length += span[length..].Write(Name);
            length += span[length..].Write(ref _bytes2, 4);
            return length;
        }
    }
}
