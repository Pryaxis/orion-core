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
using Orion.Core.Packets.DataStructures;

namespace Orion.Core.Packets.Client
{
    /// <summary>
    /// A packet sent from the server to the client to set the client's status.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct ClientStatus : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(4)] private byte _bytes2;  // Used to obtain an interior reference.
        [FieldOffset(4)] private Flags8 _flags;
        [FieldOffset(8)] private NetworkText? _statusText;

        /// <summary>
        /// Gets or sets the number of outstanding packets to be sent to the client.
        /// </summary>
        /// <value>The number of outstanding packets to be sent to the client.</value>
        [field: FieldOffset(0)] public int OutstandingPackets { get; set; }

        /// <summary>
        /// Gets or sets the client's status text.
        /// </summary>
        /// <value>The client's status text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText StatusText
        {
            get => _statusText ??= NetworkText.Empty;
            set => _statusText = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether to hide the percentage in the status text.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the percentage in the status text; otherwise, <see langword="false"/>.
        /// </value>
        public bool HidePercentage
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the status text has shadows.
        /// </summary>
        /// <value><see langword="true"/> if the status text has shadows; otherwise, <see langword="false"/>.</value>
        public bool HasShadows
        {
            get => _flags[1];
            set => _flags[1] = value;
        }

        PacketId IPacket.Id => PacketId.ClientStatus;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 4);
            length += NetworkText.Read(span[length..], out _statusText);
            length += span[length..].Read(ref _bytes2, 1);
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 4);
            length += StatusText.Write(span[length..]);
            length += span[length..].Write(ref _bytes2, 1);
            return length;
        }
    }
}
