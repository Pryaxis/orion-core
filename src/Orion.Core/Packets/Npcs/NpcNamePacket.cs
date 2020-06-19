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

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent from the client to the server to request an NPC's name or from the server to the client to set an
    /// NPC's name.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct NpcNamePacket : IPacket
    {
        [field: FieldOffset(8)] private string? _name;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        [field: FieldOffset(0)] public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's name.
        /// </summary>
        /// <value>The NPC's name.</value>
        public string Name
        {
            get => _name ?? string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.NpcName;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            var index = span.Read(ref this.AsRefByte(0), 2);
            return context == PacketContext.Server ? index : index + span[index..].Read(Encoding.UTF8, out _name);
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            var index = span.Write(ref this.AsRefByte(0), 2);
            return context == PacketContext.Client ? index : index + span[index..].Write(Name, Encoding.UTF8);
        }
    }
}
