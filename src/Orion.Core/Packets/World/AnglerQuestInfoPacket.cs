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

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent from the server to the client to set the angler quest.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public sealed class AnglerQuestInfoPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;

        // TODO: potentially enum-ify this.

        /// <summary>
        /// Gets or sets the angler quest.
        /// </summary>
        /// <value>The angler quest.</value>
        [field: FieldOffset(0)] public byte Quest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the angler quest is completed.
        /// </summary>
        /// <value><see langword="true"/> if the angler quest is completed; otherwise <see langword="false"/>.</value>
        [field: FieldOffset(1)] public bool IsCompleted { get; set; }

        PacketId IPacket.Id => PacketId.AnglerQuestInfo;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 2);
        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 2);
    }
}
